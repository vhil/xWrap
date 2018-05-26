namespace Xwrap.Extensions
{
	using Sitecore.Data;
	using Sitecore.Data.Items;
	using FieldWrappers.Abstractions;
	using System;
	using Sitecore.Data.Managers;

	public static class ItemExtensions
	{
        private static IFieldWrapperFactory FieldWrapperFactory => Xwrap.FieldWrapperFactory.Instance;

		#region field name

		public static TField StronglyTypedField<TField>(this Item item, string fieldName) where TField : IFieldWrapper
        {
            return FieldWrapperFactory.GetStronglyTypedField<TField>(item, fieldName);
        }

        public static IRichTextFieldWrapper RichTextField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IRichTextFieldWrapper>(item, fieldName);
        }

        public static IIntegerFieldWrapper IntegerField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IIntegerFieldWrapper>(item, fieldName);
        }

        public static INumberFieldWrapper NumberField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<INumberFieldWrapper>(item, fieldName);
        }

        public static ICheckboxFieldWrapper CheckboxField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<ICheckboxFieldWrapper>(item, fieldName);
        }

        public static IFileFieldWrapper FileField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IFileFieldWrapper>(item, fieldName);
        }

        public static IDateTimeFieldWrapper DateTimeField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IDateTimeFieldWrapper>(item, fieldName);
        }

        public static IGeneralLinkFieldWrapper GeneralLinkField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IGeneralLinkFieldWrapper>(item, fieldName);
        }

        public static IImageFieldWrapper ImageField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IImageFieldWrapper>(item, fieldName);
        }

        public static ILinkFieldWrapper LinkField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<ILinkFieldWrapper>(item, fieldName);
        }

        public static IListFieldWrapper ListField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IListFieldWrapper>(item, fieldName);
        }

        public static INameValueListFieldWrapper NameValueListField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<INameValueListFieldWrapper>(item, fieldName);
        }

        public static INameLookupValueListFieldWrapper NameLookupValueField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<INameLookupValueListFieldWrapper>(item, fieldName);
        }

        public static ITextFieldWrapper TextField(this Item item, string fieldName)
        {
            return FieldWrapperFactory.GetStronglyTypedField<ITextFieldWrapper>(item, fieldName);
        }

		#endregion

		#region field ID

		public static TField StronglyTypedField<TField>(this Item item, ID fieldId) where TField : IFieldWrapper
		{
			return FieldWrapperFactory.GetStronglyTypedField<TField>(item, fieldId);
		}

		public static IRichTextFieldWrapper RichTextField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<IRichTextFieldWrapper>(item, fieldId);
		}

		public static IIntegerFieldWrapper IntegerField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<IIntegerFieldWrapper>(item, fieldId);
		}

		public static INumberFieldWrapper NumberField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<INumberFieldWrapper>(item, fieldId);
		}

		public static ICheckboxFieldWrapper CheckboxField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<ICheckboxFieldWrapper>(item, fieldId);
		}

		public static IFileFieldWrapper FileField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<IFileFieldWrapper>(item, fieldId);
		}

		public static IDateTimeFieldWrapper DateTimeField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<IDateTimeFieldWrapper>(item, fieldId);
		}

		public static IGeneralLinkFieldWrapper GeneralLinkField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<IGeneralLinkFieldWrapper>(item, fieldId);
		}

		public static IImageFieldWrapper ImageField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<IImageFieldWrapper>(item, fieldId);
		}

		public static ILinkFieldWrapper LinkField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<ILinkFieldWrapper>(item, fieldId);
		}

		public static IListFieldWrapper ListField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<IListFieldWrapper>(item, fieldId);
		}

		public static INameValueListFieldWrapper NameValueListField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<INameValueListFieldWrapper>(item, fieldId);
		}

		public static INameLookupValueListFieldWrapper NameLookupValueField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<INameLookupValueListFieldWrapper>(item, fieldId);
		}

		public static ITextFieldWrapper TextField(this Item item, ID fieldId)
		{
			return FieldWrapperFactory.GetStronglyTypedField<ITextFieldWrapper>(item, fieldId);
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
	}
}