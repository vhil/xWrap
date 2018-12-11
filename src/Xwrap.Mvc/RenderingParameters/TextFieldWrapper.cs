namespace Xwrap.Mvc.RenderingParameters
{
	using FieldWrappers.Abstractions;

	/// <summary>
	/// Rendering parameters field wrapper for textual Sitecore field types such as 'single line text'. Implements <see cref="IFieldWrapper{string}"/>
	/// </summary>
	/// <seealso cref="Xwrap.Mvc.RenderingParameters.RenderingParametersFieldWrapper{System.String}" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.ITextFieldWrapper" />
	public class TextFieldWrapper : RenderingParametersFieldWrapper<string>, ITextFieldWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TextFieldWrapper"/> class.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="value">The value.</param>
		public TextFieldWrapper(string fieldName, string value)
			: base(fieldName, value)
		{
		}

		/// <summary>
		/// Gets the strongly typed value.
		/// </summary>
		public override string Value => this.RawValue;
	}
}