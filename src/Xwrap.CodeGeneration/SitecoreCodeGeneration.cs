namespace Xwrap.CodeGeneration
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Sitecore.Abstractions.Serialization;
	using Sitecore.Data.Serialization.Yaml;
	using Sitecore.Data.Serialization.Yaml.Formatting;

	public class SitecoreCodeGenerator
	{
		private readonly IEnumerable<string> pathsToTemplates;
		private readonly IEnumerable<BaseFieldFormatter> fieldFormatters;

		public SitecoreCodeGenerator(IEnumerable<string> pathsToTemplates, IEnumerable<BaseFieldFormatter> fieldFormatters = null)
		{
			if (fieldFormatters == null)
			{
				// fallback to the default formatters set from Sitecore 10 initial release
				fieldFormatters = new BaseFieldFormatter[]
				{
					new XmlFieldFormatter(),
					new MultilistFormatter(),
					new CheckboxFieldFormatter()
				};
			}

			this.pathsToTemplates = pathsToTemplates;
			this.fieldFormatters = fieldFormatters;
		}

		public virtual string GetClassName(IItemData template)
		{
			return this.AsValidWord(template.Name);
		}

		public virtual string GetFieldName(IItemData field)
		{
			return this.AsValidWord(field.Name);
		}

		public virtual string TitleCase(string word)
		{
			word = Regex.Replace(word, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1+");
			word = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(word);
			word = word.Replace("+", "");
			return word;
		}

		public virtual bool IsRenderingParameters(IItemData template)
		{
			return template.SharedFields.First(
					f => f.FieldId == new Guid("{12C33F3F-86C5-43A5-AEB4-5598CEC45116}")).Value
				.Contains("{8CA06D6A-B353-44E8-BC31-B528C7306971}");
		}

		public virtual string GetFieldType(IItemData field)
		{
			return field.SharedFields.First(f => f.FieldId == new Guid("{AB162CC0-DC80-4ABF-8871-998EE5D7BA32}")).Value.ToLowerInvariant();
		}

		public virtual string AsValidWord(string part)
		{
			part = this.TitleCase(part);
			part = part.Replace(" ", "");
			part = part.Replace("-", "");
			while (Regex.IsMatch(part, "^\\d"))
			{
				part = Regex.Replace(part, "^1", "One");
				part = Regex.Replace(part, "^2", "Two");
				part = Regex.Replace(part, "^3", "Three");
				part = Regex.Replace(part, "^4", "Four");
				part = Regex.Replace(part, "^5", "Five");
				part = Regex.Replace(part, "^6", "Six");
				part = Regex.Replace(part, "^7", "Seven");
				part = Regex.Replace(part, "^8", "Eight");
				part = Regex.Replace(part, "^9", "Nine");
			}
			return part;
		}

		public virtual string GetParameterMethodName(IItemData field)
		{
			var fieldWrapperName = this.GetParameterWrapperType(field);
			fieldWrapperName = fieldWrapperName.Substring(1, fieldWrapperName.Length - 1);
			return fieldWrapperName.Replace("Wrapper", "");
		}

		public virtual string GetParameterWrapperType(IItemData field)
		{
			string returnType;
			var typeName = this.GetFieldType(field);

			switch (typeName)
			{
				case "checkbox":
					returnType = "ICheckboxFieldWrapper";
					break;
				case "checklist":
				case "treelist":
				case "treelist with search":
				case "treelistex":
				case "multilist":
				case "multilist with search":
				case "multi-root treelist":
				case "accounts multilist":
				case "tags":
					returnType = "IListFieldWrapper";
					break;
				case "droplink":
				case "droptree":
				case "lui component theme":
					returnType = "ILinkFieldWrapper";
					break;
				case "internal link":
					returnType = "IInternalLinkFieldWrapper";
					break;
				case "text":
				case "single-line text":
				case "multi-line text":
					returnType = "ITextFieldWrapper";
					break;
				case "number":
					returnType = "INumberFieldWrapper";
					break;
				case "integer":
					returnType = "IIntegerFieldWrapper";
					break;
				default:
					returnType = "ITextFieldWrapper";
					break;
			}

			return returnType;
		}

		public virtual string GetFieldWrapperType(IItemData field)
		{
			var typeName = this.GetFieldType(field);
			string returnType;

			switch (typeName)
			{
				case "checkbox":
					returnType = "ICheckboxFieldWrapper";
					break;
				case "image":
					returnType = "IImageFieldWrapper";
					break;
				case "file":
					returnType = "IFileFieldWrapper";
					break;
				case "date":
				case "datetime":
					returnType = "IDateTimeFieldWrapper";
					break;
				case "checklist":
				case "treelist":
				case "treelist with search":
				case "treelistex":
				case "multilist":
				case "multilist with search":
				case "multi-root treelist":
				case "accounts multilist":
				case "tags":
					returnType = "IListFieldWrapper";
					break;
				case "droplink":
				case "lui component theme":
				case "droptree":
					returnType = "ILinkFieldWrapper";
					break;
				case "internal link":
					returnType = "IInternalLinkFieldWrapper";
					break;
				case "general link":
				case "general link with search":
					returnType = "IGeneralLinkFieldWrapper";
					break;
				case "text":
				case "single-line text":
				case "multi-line text":
					returnType = "ITextFieldWrapper";
					break;
				case "rich text":
					returnType = "IRichTextFieldWrapper";
					break;
				case "number":
					returnType = "INumberFieldWrapper";
					break;
				case "integer":
					returnType = "IIntegerFieldWrapper";
					break;
				case "name lookup value list":
					returnType = "INameLookupValueListFieldWrapper";
					break;
				case "name value list":
					returnType = "INameValueListFieldWrapper";
					break;
				default:
					returnType = "ITextFieldWrapper";
					break;
			}

			return returnType;
		}

		public virtual IReadOnlyCollection<TemplateData> GetTemplateData()
		{
			var files = this.pathsToTemplates.SelectMany(x => Directory.EnumerateFiles(x, "*.yml", SearchOption.AllDirectories));

			var items = new List<IItemData>();

			var factory = new StaticFieldFormattersFactory(this.fieldFormatters);
			var serializer = new YamlItemSerializer(factory);
			foreach (var file in files)
			{
				using (TextReader reader = new StreamReader(file))
				{
					var item = serializer.Read(reader);
					items.Add(item);
				}
			}

			var itemsLookup = items.ToLookup(x => x.ParentId, x => x);

			var templates = items
				.Where(x => x.TemplateId == Sitecore.TemplateIDs.Template.Guid)
				.Where(x => x.Name != "$name");

			return templates.Select(template => new TemplateData
			{
				Template = template,
				Fields = this.GetFields(template.Id, itemsLookup)
			}).ToArray();
		}

		public virtual IList<IItemData> GetSections(Guid templateId, ILookup<Guid, IItemData> lookup)
		{
			return lookup[templateId].Where(x => x.TemplateId == Sitecore.TemplateIDs.TemplateSection.Guid).ToList();
		}

		public virtual IList<IItemData> GetFields(Guid templateId, ILookup<Guid, IItemData> lookup)
		{
			var sectionIds = this.GetSections(templateId, lookup).Select(x => x.Id);
			return sectionIds.SelectMany(x => lookup[x].Where(item => item.TemplateId == Sitecore.TemplateIDs.TemplateField.Guid).ToList()).ToList();
		}
	}

	public class TemplateData
	{
		public IItemData Template { get; set; }
		public IEnumerable<IItemData> Fields { get; set; }
	}
}
