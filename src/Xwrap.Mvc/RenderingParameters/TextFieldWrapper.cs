namespace Xwrap.Mvc.RenderingParameters
{
	using FieldWrappers.Abstractions;

	public class TextFieldWrapper : RenderingParametersFieldWrapper, ITextFieldWrapper
    {
        public TextFieldWrapper(string fieldName, string value)
            : base(fieldName, value)
        {
        }

        public string Value => this.RawValue;
    }
}