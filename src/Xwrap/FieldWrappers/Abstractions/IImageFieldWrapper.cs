namespace Xwrap.FieldWrappers.Abstractions
{
	using Sitecore.Data.Items;
	using Sitecore.Resources.Media;

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
		/// Gets media url for the image
		/// </summary>
		/// <param name="mw"></param>
		/// <param name="mh"></param>
		/// <returns></returns>
		string GetSourceUri(int mw = 0, int mh = 0);

		/// <summary>
		/// Gets media url for the image
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		string GetSourceUri(MediaUrlOptions options);

		/// <summary>
		/// Gets the target media item as <see cref="Item"/>.
		/// </summary>
		/// <returns></returns>
		Item GetTarget();
	}
}