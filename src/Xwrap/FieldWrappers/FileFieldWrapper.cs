namespace Xwrap.FieldWrappers
{
	using System.Web;
	using Abstractions;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using Sitecore.Resources.Media;
	using System;

	/// <summary>
	/// Default field wrapper type for 'file' Sitecore fields. Implements <see cref="IFileFieldWrapper"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IFileFieldWrapper" />
	public class FileFieldWrapper : FieldWrapper, IFileFieldWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FileFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public FileFieldWrapper(Field originalField) : base(originalField)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public FileFieldWrapper(BaseItem item, string fieldName) : base(item, fieldName)
		{
		}

		/// <summary>
		/// Renders the field using default Sitecore field renderer.
		/// </summary>
		/// <param name="parameters">The parameters</param>
		/// <param name="editing">Specify if the field should be editable when rendered.</param>
		/// <returns></returns>
		public override IHtmlString Render(string parameters = null, bool editing = true)
		{
			var url = "/" + MediaManager.GetMediaUrl(this.MediaItem).TrimStart('/');

			return new HtmlString(url);
		}

		/// <summary>
		/// Gets the file download URL.
		/// </summary>
		/// <value>
		/// The download URL.
		/// </value>
		public string DownloadUrl
		{
			get
			{
				var returnValue = string.Empty;
				if (this.MediaItem != null)
				{
					returnValue = MediaManager.GetMediaUrl(this.MediaItem, new MediaUrlOptions
					{
						AbsolutePath = true,
						AlwaysIncludeServerUrl = true,
						IncludeExtension = false
					});
				}
				return returnValue;
			}
		}

		/// <summary>
		/// Gets the file extension.
		/// </summary>
		/// <value>
		/// The file extension.
		/// </value>
		public string Extension
		{
			get
			{
				var returnValue = string.Empty;
				if (this.MediaItem != null)
				{
					returnValue = this.MediaItem.Fields["Extension"].ToString();
				}
				return returnValue;
			}
		}

		/// <summary>
		/// Gets the file size.
		/// </summary>
		/// <value>
		/// The file size.
		/// </value>
		public string Size
		{
			get
			{
				var returnValue = string.Empty;
				if (this.MediaItem != null)
				{
					var bytes = long.Parse(this.MediaItem.Fields["Size"].ToString());
					returnValue = Math.Round((bytes / 1024f) / 1024f, 2).ToString();
				}
				return returnValue;
			}
		}

		/// <summary>
		/// Gets the rendered image string value.
		/// </summary>
		public string Value => this.Render().ToHtmlString();

		/// <summary>
		/// Gets the media item.
		/// </summary>
		protected Item MediaItem => ((FileField)this.OriginalField).MediaItem;
	}
}