namespace Xwrap.Mvc.RenderingParameters
{
	using FieldWrappers.Abstractions;

	public class TextFieldWrapper : RenderingParametersFieldWrapper<string>, ITextFieldWrapper
    {
        public TextFieldWrapper(string fieldName, string value)
            : base(fieldName, value)
        {
        }

        public override string Value => this.RawValue;
    }
}