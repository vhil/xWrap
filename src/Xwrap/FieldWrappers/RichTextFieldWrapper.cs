namespace Xwrap.FieldWrappers
{
    using System.Web;
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

    public class RichTextFieldWrapper : TextFieldWrapper, IRichTextFieldWrapper
    {
        public RichTextFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

        public RichTextFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

        public new IHtmlString Value => this.Render(editing: false);
    }
}