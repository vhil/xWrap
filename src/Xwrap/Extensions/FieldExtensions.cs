namespace Xwrap.Extensions
{
    using Sitecore.Data.Fields;
    using FieldWrappers.Abstractions;

	public static class FieldExtensions
    {
        private static IFieldWrapperFactory FieldWrapperFactory => Xwrap.FieldWrapperFactory.Instance;

        public static TField AsStronglyTypedField<TField>(this Field field) where TField : IFieldWrapper
        {
            return FieldWrapperFactory.GetStronglyTypedField<TField>(field);
        }

        public static IRichTextFieldWrapper AsRichTextField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IRichTextFieldWrapper>(field);
        }

        public static IIntegerFieldWrapper AsIntegerField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IIntegerFieldWrapper>(field);
        }

        public static INumberFieldWrapper AsNumberField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<INumberFieldWrapper>(field);
        }

        public static ICheckboxFieldWrapper AsCheckboxField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<ICheckboxFieldWrapper>(field);
        }

        public static IFileFieldWrapper AsFileField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IFileFieldWrapper>(field);
        }

        public static IDateTimeFieldWrapper AsDateTimeField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IDateTimeFieldWrapper>(field);
        }

        public static IGeneralLinkFieldWrapper AsGeneralLinkField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IGeneralLinkFieldWrapper>(field);
        }

        public static IImageFieldWrapper AsImageField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IImageFieldWrapper>(field);
        }

        public static ILinkFieldWrapper AsLinkField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<ILinkFieldWrapper>(field);
        }

        public static IListFieldWrapper AsListField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<IListFieldWrapper>(field);
        }

        public static INameValueListFieldWrapper AsNameValueListField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<INameValueListFieldWrapper>(field);
        }

        public static INameLookupValueListFieldWrapper AsNameLookupValueField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<INameLookupValueListFieldWrapper>(field);
        }

        public static ITextFieldWrapper TextField(this Field field)
        {
            return FieldWrapperFactory.GetStronglyTypedField<ITextFieldWrapper>(field);
        }
    }
}