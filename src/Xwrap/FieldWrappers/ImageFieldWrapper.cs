namespace Xwrap.FieldWrappers
{
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;

    public class ImageFieldWrapper : FieldWrapper, IImageFieldWrapper
    {
        public ImageFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

        public ImageFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

        protected ImageField ImageField => this.OriginalField;
        public string AltText => this.ImageField.Alt;
        public string Value => this.GetSourceUri();

        public string GetSourceUri()
        {
            return this.GetSourceUri(false);
        }

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
        
        public Item GetTarget()
        {
            if (!this.HasValue)
            {
                return null;
            }

            return this.ImageField.MediaItem;
        }

        public static implicit operator string(ImageFieldWrapper field)
        {
            return field.GetSourceUri();
        }
    }
}
