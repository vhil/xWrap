namespace Xwrap.FieldWrappers.Abstractions
{
    using System.Web;

    public interface IFieldWrapper : IHtmlString
    {
        object Original { get; }
        string Name { get; }
        string RawValue { get; }
        bool HasValue { get; }
        IHtmlString Render(string parameters = null, bool editing = true);
        IHtmlString Render(object parameters, bool editing = true);
        IHtmlString RenderBeginField(object parameters, bool editing = true);
        IHtmlString RenderBeginField(string parameters = null, bool editing = true);
        IHtmlString RenderEndField();
    }

    public interface IFieldWrapper<out TReturnType> : IFieldWrapper
    {
        TReturnType Value { get; }
    }
}
