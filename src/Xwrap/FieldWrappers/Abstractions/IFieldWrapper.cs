namespace Xwrap.FieldWrappers.Abstractions
{
    using System.Web;

	/// <summary>
	/// Field wrapper abstraction
	/// </summary>
	public interface IFieldWrapper : IHtmlString
    {
		/// <summary>
		/// Gets the original wrapped object.
		/// </summary>
		object Original { get; }

		/// <summary>
		/// Gets the field name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the raw field value.
		/// </summary>
		string RawValue { get; }

		/// <summary>
		/// Gets a value indicating whether this field has a valid value.
		/// </summary>
		bool HasValue { get; }

		/// <summary>
		/// Renders the field using default Sitecore field renderer.
		/// </summary>
		/// <param name="parameters">The parameters</param>
		/// <param name="editing">Specify if the field should be editable when rendered.</param>
		IHtmlString Render(string parameters = null, bool editing = true);

	    /// <summary>
	    /// Renders the field using default Sitecore field renderer.
	    /// </summary>
	    /// <param name="parameters">The parameters</param>
	    /// <param name="editing">Specify if the field should be editable when rendered.</param>
		IHtmlString Render(object parameters, bool editing = true);

		/// <summary>
		/// Renders the field using default Sitecore field renderer. Leaves the option to insert html inside the field until RenderEndField method is being called.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="editing">if set to <c>true</c> [editing].</param>
		/// <returns></returns>
		IHtmlString RenderBeginField(object parameters, bool editing = true);

	    /// <summary>
	    /// Renders the field using default Sitecore field renderer. Leaves the option to insert html inside the field until RenderEndField method is being called.
	    /// </summary>
	    /// <param name="parameters">The parameters.</param>
	    /// <param name="editing">if set to <c>true</c> [editing].</param>
	    /// <returns></returns>
		IHtmlString RenderBeginField(string parameters = null, bool editing = true);

		/// <summary>
		/// Renders the end of field. The RenderBeginField method must be called before.
		/// </summary>
		/// <returns></returns>
		IHtmlString RenderEndField();
    }

	/// <summary>
	/// Strongly typed field wrapper abstraction
	/// </summary>
	public interface IFieldWrapper<out TReturnType> : IFieldWrapper
    {
		/// <summary>
		/// Gets the strongly typed field value.
		/// </summary>
		TReturnType Value { get; }
    }
}
