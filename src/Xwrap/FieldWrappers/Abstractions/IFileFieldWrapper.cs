namespace Xwrap.FieldWrappers.Abstractions
{
	/// <summary>
	/// Field wrapper abstraction for 'file' Sitecore field types. Implements <see cref="IFieldWrapper{string}"/>
	/// </summary>
	public interface IFileFieldWrapper : IFieldWrapper<string>
	{
		/// <summary>
		/// Gets the file download URL.
		/// </summary>
		string DownloadUrl { get; }

		/// <summary>
		/// Gets the file extension.
		/// </summary>
		string Extension { get; }

		/// <summary>
		/// Gets the file size.
		/// </summary>
		string Size { get; }
	}
}