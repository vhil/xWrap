using Xwrap.Exceptions;

namespace Xwrap.Extensions
{
	using Sitecore.Data;
	using Sitecore.Data.Items;
	using FieldWrappers.Abstractions;
	using System;
	using Sitecore.Data.Managers;
	using System.Collections.Generic;
	using Sitecore.Collections;

	public static class ItemExtensions
	{
        private static IFieldWrapperFactory FieldWrapperFactory => Xwrap.FieldWrapperFactory.Instance;

		#region field name

		public static TField StronglyTypedField<TField>(this Item item, string fieldName) where TField : IFieldWrapper
        {
            return FieldWrapperFactory.WrapField<TField>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="IRichTextFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IRichTextFieldWrapper"/></returns>
		public static IRichTextFieldWrapper RichTextField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<IRichTextFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="IIntegerFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IIntegerFieldWrapper"/></returns>
		public static IIntegerFieldWrapper IntegerField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<IIntegerFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="INumberFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="INumberFieldWrapper"/></returns>
		public static INumberFieldWrapper NumberField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<INumberFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="ICheckboxFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="ICheckboxFieldWrapper"/></returns>
		public static ICheckboxFieldWrapper CheckboxField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<ICheckboxFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="IFileFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IFileFieldWrapper"/></returns>
		public static IFileFieldWrapper FileField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<IFileFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="IDateTimeFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IDateTimeFieldWrapper"/></returns>
		public static IDateTimeFieldWrapper DateTimeField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<IDateTimeFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="IGeneralLinkFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IGeneralLinkFieldWrapper"/></returns>
		public static IGeneralLinkFieldWrapper GeneralLinkField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<IGeneralLinkFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="IImageFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IImageFieldWrapper"/></returns>
		public static IImageFieldWrapper ImageField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<IImageFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="ILinkFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="ILinkFieldWrapper"/></returns>
		public static ILinkFieldWrapper LinkField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<ILinkFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="IListFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IListFieldWrapper"/></returns>
		public static IListFieldWrapper ListField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<IListFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="INameValueListFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="INameValueListFieldWrapper"/></returns>
		public static INameValueListFieldWrapper NameValueListField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<INameValueListFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="INameLookupValueListFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="INameLookupValueListFieldWrapper"/></returns>
		public static INameLookupValueListFieldWrapper NameLookupValueField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<INameLookupValueListFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="ITextFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="ITextFieldWrapper"/></returns>
		public static ITextFieldWrapper TextField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<ITextFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field into <see cref="IInternalLinkFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IInternalLinkFieldWrapper"/></returns>
		public static IInternalLinkFieldWrapper InternalLinkField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.WrapField<IInternalLinkFieldWrapper>(item, fieldName);
        }

		#endregion

		#region field ID

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <typeparam name="TField">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		public static TField StronglyTypedField<TField>(this Item item, ID fieldId) where TField : IFieldWrapper
		{
			return FieldWrapperFactory.WrapField<TField>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="IRichTextFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IRichTextFieldWrapper"/></returns>
		public static IRichTextFieldWrapper RichTextField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<IRichTextFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="IIntegerFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IIntegerFieldWrapper"/></returns>
		public static IIntegerFieldWrapper IntegerField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<IIntegerFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="INumberFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="INumberFieldWrapper"/></returns>
		public static INumberFieldWrapper NumberField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<INumberFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="ICheckboxFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="ICheckboxFieldWrapper"/></returns>
		public static ICheckboxFieldWrapper CheckboxField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<ICheckboxFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="IFileFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IFileFieldWrapper"/></returns>
		public static IFileFieldWrapper FileField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<IFileFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="IDateTimeFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IDateTimeFieldWrapper"/></returns>
		public static IDateTimeFieldWrapper DateTimeField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<IDateTimeFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="IGeneralLinkFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IGeneralLinkFieldWrapper"/></returns>
		public static IGeneralLinkFieldWrapper GeneralLinkField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<IGeneralLinkFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="IImageFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IImageFieldWrapper"/></returns>
		public static IImageFieldWrapper ImageField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<IImageFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="ILinkFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="ILinkFieldWrapper"/></returns>
		public static ILinkFieldWrapper LinkField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<ILinkFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="IListFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IListFieldWrapper"/></returns>
		public static IListFieldWrapper ListField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<IListFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="INameValueListFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="INameValueListFieldWrapper"/></returns>
		public static INameValueListFieldWrapper NameValueListField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<INameValueListFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="INameLookupValueListFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="INameLookupValueListFieldWrapper"/></returns>
		public static INameLookupValueListFieldWrapper NameLookupValueField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<INameLookupValueListFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="ITextFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="ITextFieldWrapper"/></returns>
		public static ITextFieldWrapper TextField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<ITextFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field into <see cref="IInternalLinkFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		/// <returns>Instance of <see cref="IInternalLinkFieldWrapper"/></returns>
		public static IInternalLinkFieldWrapper InternalLinkField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.WrapField<IInternalLinkFieldWrapper>(item, fieldId);
		}

		#endregion

		internal static bool IsDerived(this Item item, Guid templateId)
		{
			return item.IsDerived(new ID(templateId));
		}

		internal static bool IsDerived(this Item item, ID templateId)
		{
			if (item == null)
			{
				return false;
			}

			return !templateId.IsNull && item.IsDerived(item.Database.Templates[templateId]);
		}

		internal static bool IsDerived(this Item item, Item templateItem)
		{
			if (item == null)
			{
				return false;
			}

			if (templateItem == null)
			{
				return false;
			}

			var itemTemplate = TemplateManager.GetTemplate(item);
			return itemTemplate != null && (itemTemplate.ID == templateItem.ID || itemTemplate.DescendsFrom(templateItem.ID));
		}

		internal static IEnumerable<Item> GetChildrenReccursively(this Item item, Guid? templateId = null)
		{
			var result = new List<Item>();

			foreach (Item child in item.GetChildren(ChildListOptions.IgnoreSecurity))
			{
				if (!templateId.HasValue || child.IsDerived(templateId.Value))
				{
					result.Add(child);
				}

				result.AddRange(child.GetChildrenReccursively(templateId));
			}

			return result;
		}
	}
}