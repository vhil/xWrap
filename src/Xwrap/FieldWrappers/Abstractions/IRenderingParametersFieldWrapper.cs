namespace Xwrap.FieldWrappers.Abstractions
{
	/// <summary>
	/// Rendering parameters template wrapper abstraction.
	/// </summary>
	public interface IRenderingParametersFieldWrapper : IFieldWrapper
	{
	}

	/// <summary>
	/// Generic rendering parameters template wrapper abstraction.
	/// </summary>
	public interface IRenderingParametersFieldWrapper<out TReturnType> : IFieldWrapper<TReturnType>
	{
	}
}
