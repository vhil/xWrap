namespace Xwrap.FieldWrappers.Abstractions
{
    using System;
    using System.Web;

    public interface IDateTimeFieldWrapper : IFieldWrapper<DateTime>
    {
        IHtmlString Render(bool includeTime, bool editing = true);
    }
}
