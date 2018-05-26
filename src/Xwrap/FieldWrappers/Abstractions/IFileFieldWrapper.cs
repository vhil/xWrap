namespace Xwrap.FieldWrappers.Abstractions
{
    public interface IFileFieldWrapper : IFieldWrapper<string>
    {
	    string DownloadUrl { get; }
	    string Extension { get; }
	    string Size { get; }
	}
}