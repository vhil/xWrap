namespace Xwrap.FieldWrappers
{
	using System.Web;
	using Abstractions;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using Sitecore.Resources.Media;
	using System;

	public class FileFieldWrapper : FieldWrapper, IFileFieldWrapper
	{
		public FileFieldWrapper(Field originalField) : base(originalField)
		{
		}

		public FileFieldWrapper(BaseItem item, string fieldName) : base(item, fieldName)
		{
		}

		public override IHtmlString Render(string parameters = null, bool editing = true)
		{
			var url = "/" + MediaManager.GetMediaUrl(this.MediaItem).TrimStart('/');

			return new HtmlString(url);
		}

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

		public string Size
		{
			get
			{
				var returnValue = string.Empty;
				if (this.MediaItem != null)
				{
					long bytes = long.Parse(this.MediaItem.Fields["Size"].ToString());
					returnValue = Math.Round((bytes / 1024f) / 1024f, 2).ToString();
				}
				return returnValue;
			}
		}


		public string Value => this.Render().ToHtmlString();
		protected Item MediaItem => ((FileField)this.OriginalField).MediaItem;
	}
}