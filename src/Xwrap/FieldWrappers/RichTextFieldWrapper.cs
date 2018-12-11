namespace Xwrap.FieldWrappers
{
    using System.Web;
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

	/// <summary>
	/// Default field wrapper type for 'rich text' Sitecore field types.Implements <see cref="ITextFieldWrapper"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.TextFieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IRichTextFieldWrapper" />
	public class RichTextFieldWrapper : TextFieldWrapper, IRichTextFieldWrapper
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="RichTextFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public RichTextFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="RichTextFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public RichTextFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

		/// <summary>
		/// Gets the HTML value.
		/// </summary>
		public new IHtmlString Value => this.Render(editing: false);
    }
}