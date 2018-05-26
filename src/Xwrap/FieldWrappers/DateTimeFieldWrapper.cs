namespace Xwrap.FieldWrappers
{
    using System;
    using System.Web;
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using System.Globalization;

    public class DateTimeFieldWrapper : FieldWrapper, IDateTimeFieldWrapper
    {
        public DateTimeFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

        public DateTimeFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

        public DateTime Value => ((DateField) this.OriginalField).DateTime.ToLocalTime();

        public IHtmlString Render(bool includeTime)
        {
            return this.Render(includeTime 
                ? this.OriginalField.Language?.CultureInfo?.DateTimeFormat.FullDateTimePattern ?? CultureInfo.InvariantCulture.DateTimeFormat.FullDateTimePattern
                : this.OriginalField.Language?.CultureInfo?.DateTimeFormat.ShortDatePattern ?? CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern);
        }

        public override IHtmlString Render(string dateTimeFormat = null, bool editing = true)
        {
            return base.Render("format=" + dateTimeFormat);
        }

        public static implicit operator DateTime(DateTimeFieldWrapper field)
        {
            return field.Value;
        }
    }
}
