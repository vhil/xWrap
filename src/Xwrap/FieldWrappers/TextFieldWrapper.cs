namespace Xwrap.FieldWrappers
{
	using Abstractions;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;

	/// <summary>
	/// Default field wrapper type for textual Sitecore field types such as 'single line text'. Implements <see cref="IFieldWrapper{string}"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.ITextFieldWrapper" />
	public class TextFieldWrapper : FieldWrapper, ITextFieldWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TextFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public TextFieldWrapper(Field originalField)
			: base(originalField)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public TextFieldWrapper(BaseItem item, string fieldName)
			: base(item, fieldName)
		{
		}

		/// <summary>
		/// Gets the strongly typed value.
		/// </summary>
		public string Value => this.RawValue;
	}
}
