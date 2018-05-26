namespace Xwrap.FieldWrappers.Abstractions
{
    using Sitecore.Data.Items;

    public interface IImageFieldWrapper : IFieldWrapper<string>
    {
        string AltText { get; }

        string GetSourceUri();

        string GetSourceUri(bool absolute);

        Item GetTarget();
    }
}