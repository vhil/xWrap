namespace Xwrap.Extensions
{
    using Sitecore.Data.Fields;
    using FieldWrappers.Abstractions;

	public static class FieldExtensions
    {
        private static IFieldWrapperFactory FieldWrapperFactory => Xwrap.FieldWrapperFactory.Instance;

        public static TField AsStronglyTypedField<TField>(this Field field) where TField : IFieldWrapper
        {
            return FieldWrapperFactory.WrapField<TField>(field);
        }

        public static IRichTextFieldWrapper AsRichTextField(this Field field)
        {
            return FieldWrapperFactory.WrapField<IRichTextFieldWrapper>(field);
        }

        public static IIntegerFieldWrapper AsIntegerField(this Field field)
        {
            return FieldWrapperFactory.WrapField<IIntegerFieldWrapper>(field);
        }

        public static INumberFieldWrapper AsNumberField(this Field field)
        {
            return FieldWrapperFactory.WrapField<INumberFieldWrapper>(field);
        }

        public static ICheckboxFieldWrapper AsCheckboxField(this Field field)
        {
            return FieldWrapperFactory.WrapField<ICheckboxFieldWrapper>(field);
        }

        public static IFileFieldWrapper AsFileField(this Field field)
        {
            return FieldWrapperFactory.WrapField<IFileFieldWrapper>(field);
        }

        public static IDateTimeFieldWrapper AsDateTimeField(this Field field)
        {
            return FieldWrapperFactory.WrapField<IDateTimeFieldWrapper>(field);
        }

        public static IGeneralLinkFieldWrapper AsGeneralLinkField(this Field field)
        {
            return FieldWrapperFactory.WrapField<IGeneralLinkFieldWrapper>(field);
        }

        public static IImageFieldWrapper AsImageField(this Field field)
        {
            return FieldWrapperFactory.WrapField<IImageFieldWrapper>(field);
        }

        public static ILinkFieldWrapper AsLinkField(this Field field)
        {
            return FieldWrapperFactory.WrapField<ILinkFieldWrapper>(field);
        }

        public static IListFieldWrapper AsListField(this Field field)
        {
            return FieldWrapperFactory.WrapField<IListFieldWrapper>(field);
        }

        public static INameValueListFieldWrapper AsNameValueListField(this Field field)
        {
            return FieldWrapperFactory.WrapField<INameValueListFieldWrapper>(field);
        }

        public static INameLookupValueListFieldWrapper AsNameLookupValueField(this Field field)
        {
            return FieldWrapperFactory.WrapField<INameLookupValueListFieldWrapper>(field);
        }

        public static ITextFieldWrapper TextField(this Field field)
        {
            return FieldWrapperFactory.WrapField<ITextFieldWrapper>(field);
        }
    }
}