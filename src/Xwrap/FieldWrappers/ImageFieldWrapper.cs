namespace Xwrap.FieldWrappers
{
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;

	/// <summary>
	/// Default field wrapper type for 'image' Sitecore fields. Implements <see cref="IImageFieldWrapper"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IImageFieldWrapper" />
	public class ImageFieldWrapper : FieldWrapper, IImageFieldWrapper
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ImageFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public ImageFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public ImageFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

		/// <summary>
		/// Gets the image field.
		/// </summary>
		protected ImageField ImageField => this.OriginalField;

		/// <summary>
		/// Gets the image alt text.
		/// </summary>
		public string AltText => this.ImageField.Alt;

		/// <summary>
		/// Gets the strongly typed field value.
		/// </summary>
		public string Value => this.GetSourceUri();

		/// <summary>
		/// Gets the image source URI.
		/// </summary>
		/// <returns></returns>
		public string GetSourceUri()
        {
            return this.GetSourceUri(false);
        }

		/// <summary>
		/// Gets the image source URI.
		/// </summary>
		/// <param name="absolute">if set to <c>true</c> includes hostname.</param>
		/// <returns></returns>
		public string GetSourceUri(bool absolute)
        {
            if (!this.HasValue)
            {
                return string.Empty;
            }

            var mediaItem = this.ImageField.MediaItem;

            var url = mediaItem == null 
                ? string.Empty 
                : MediaManager.GetMediaUrl(mediaItem, new MediaUrlOptions { AbsolutePath = absolute });

	        if (!absolute && !string.IsNullOrWhiteSpace(url))
	        {
		        url = "/" + url.TrimStart('/');
	        }

	        return url;
        }

		/// <summary>
		/// Gets the target media item as <see cref="Item" />.
		/// </summary>
		/// <returns></returns>
		public Item GetTarget()
        {
            if (!this.HasValue)
            {
                return null;
            }

            return this.ImageField.MediaItem;
        }

		/// <summary>
		/// Performs an implicit conversion from <see cref="ImageFieldWrapper"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator string(ImageFieldWrapper field)
        {
            return field.GetSourceUri();
        }
    }
}
