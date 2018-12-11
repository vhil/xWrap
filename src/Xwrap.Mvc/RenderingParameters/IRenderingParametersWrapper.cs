namespace Xwrap.Mvc.RenderingParameters
{
    using FieldWrappers.Abstractions;

	/// <summary>
	/// An abstraction for wrapping rendering parameters templates
	/// </summary>
	public interface IRenderingParametersWrapper
    {
		/// <summary>
		/// Wraps the rendering parameters field into xWrap strongly typed rendering parameter field wrapper of <see cref="IRenderingParametersFieldWrapper"/>.
		/// </summary>
		/// <typeparam name="TField">The type of the field.</typeparam>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		TField WrapParameter<TField>(string fieldName) where TField : class, IRenderingParametersFieldWrapper;

		/// <summary>
		///  Wraps the field into rendering parameters <see cref="ICheckboxFieldWrapper"/> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		ICheckboxFieldWrapper CheckboxField(string fieldName);

		/// <summary>
		///  Wraps the field into rendering parameters <see cref="IIntegerFieldWrapper"/> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		IIntegerFieldWrapper IntegerField(string fieldName);

		/// <summary>
		///  Wraps the field into rendering parameters <see cref="ILinkFieldWrapper"/> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		ILinkFieldWrapper LinkField(string fieldName);

		/// <summary>
		///  Wraps the field into rendering parameters <see cref="IListFieldWrapper"/> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		IListFieldWrapper ListField(string fieldName);

		/// <summary>
		///  Wraps the field into rendering parameters <see cref="INumberFieldWrapper"/> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		INumberFieldWrapper NumberField(string fieldName);

		/// <summary>
		///  Wraps the field into rendering parameters <see cref="ITextFieldWrapper"/> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		ITextFieldWrapper TextField(string fieldName);
    }
}