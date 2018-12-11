namespace Xwrap.FieldWrappers.Abstractions
{
    using Sitecore.Data.Items;

	/// <summary>
	/// Field wrapper abstraction for 'image' Sitecore field types. Implements <see cref="IFieldWrapper{string}"/>
	/// </summary>
	public interface IImageFieldWrapper : IFieldWrapper<string>
    {
		/// <summary>
		/// Gets the image alt text.
		/// </summary>
		string AltText { get; }

		/// <summary>
		/// Gets the image source URI.
		/// </summary>
		/// <returns></returns>
		string GetSourceUri();

		/// <summary>
		/// Gets the image source URI.
		/// </summary>
		/// <param name="absolute">if set to <c>true</c> includes hostname.</param>
		/// <returns></returns>
		string GetSourceUri(bool absolute);

		/// <summary>
		/// Gets the target media item as <see cref="Item"/>.
		/// </summary>
		/// <returns></returns>
		Item GetTarget();
    }
}