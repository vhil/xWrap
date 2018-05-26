namespace Xwrap.FieldWrappers
{
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

    public class TextFieldWrapper : FieldWrapper, ITextFieldWrapper
    {
        public TextFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

        public TextFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

        public string Value => this.RawValue;
    }
}
