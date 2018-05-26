namespace Xwrap.Mvc.RenderingParameters
{
    using FieldWrappers.Abstractions;

	public interface IRenderingParametersWrapper
    {
        TField WrapParameter<TField>(string fieldName) where TField : class, IRenderingParametersFieldWrapper;
        ICheckboxFieldWrapper CheckboxField(string fieldName);
        IIntegerFieldWrapper IntegerField(string fieldName);
        ILinkFieldWrapper LinkField(string fieldName);
        IListFieldWrapper ListField(string fieldName);
        INumberFieldWrapper NumberField(string fieldName);
        ITextFieldWrapper TextField(string fieldName);
    }
}