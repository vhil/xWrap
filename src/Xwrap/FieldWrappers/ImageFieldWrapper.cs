namespace Xwrap.FieldWrappers
{
	using Abstractions;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using Sitecore.Resources.Media;
	using System.Web;
	using Sitecore;
	using Sitecore.Pipelines.WebDAV.Processors;
	using Sitecore.SecurityModel;

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
		/// Gets media url for the image
		/// </summary>
		/// <param name="absolute"></param>
		/// <param name="mw"></param>
		/// <param name="mh"></param>
		/// <returns></returns>
		public string GetSourceUri(bool absolute = false, int mw = 0, int mh = 0)
		{
			var options = new MediaUrlOptions {AbsolutePath = absolute};

			if (mw > 0)
			{
				options.MaxWidth = mw;
			}

			if (mh > 0)
			{
				options.MaxHeight = mh;
			}

			return this.GetSourceUri(options);
		}

		/// <summary>
		/// Gets media url for the image
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public string GetSourceUri(MediaUrlOptions options)
		{
			var disabler = Settings.DisableSecurityOnLinkGeneration ? new SecurityDisabler() : null;

			if (!this.HasValue)
			{
				disabler?.Dispose();
				return string.Empty;
			}

			var mediaItem = this.ImageField.MediaItem;

			if (mediaItem == null) return string.Empty;

			if (options == null)
			{
				options = MediaUrlOptions.Empty;
			}

			var mediaUrl = MediaManager.GetMediaUrl(mediaItem, options);
			var url = StringUtil.EnsurePrefix('/', mediaUrl);
			var hashedUrl = HashingUtils.ProtectAssetUrl(url);

			disabler?.Dispose();
			return hashedUrl;
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

		public override IHtmlString RenderBeginField(object parameters, bool editing = true)
		{
			var disabler = Settings.DisableSecurityOnLinkGeneration ? new SecurityDisabler() : null;
			var url = base.RenderBeginField(parameters, editing);
			disabler?.Dispose();
			return url;
		}

		public override IHtmlString RenderBeginField(string parameters = null, bool editing = true)
		{
			var disabler = Settings.DisableSecurityOnLinkGeneration ? new SecurityDisabler() : null;
			var url = base.RenderBeginField(parameters, editing);
			disabler?.Dispose();
			return url;
		}
	}
}
