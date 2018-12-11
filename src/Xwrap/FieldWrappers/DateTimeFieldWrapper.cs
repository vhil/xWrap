namespace Xwrap.FieldWrappers
{
    using System;
    using System.Web;
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using System.Globalization;

	/// <summary>
	/// Default field wrapper type for datetime based Sitecore fields. Implements <see cref="IDateTimeFieldWrapper"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IDateTimeFieldWrapper" />
	public class DateTimeFieldWrapper : FieldWrapper, IDateTimeFieldWrapper
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public DateTimeFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public DateTimeFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

		/// <summary>
		/// Gets the field value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public DateTime Value => ((DateField) this.OriginalField).DateTime.ToLocalTime();

		/// <summary>
		/// Renders date time field
		/// </summary>
		/// <param name="includeTime">Specify if time part should be included</param>
		/// <param name="editing">Specify if the field should be editable</param>
		/// <returns></returns>
		public IHtmlString Render(bool includeTime, bool editing = true)
        {
            return this.Render(includeTime 
                ? this.OriginalField.Language?.CultureInfo?.DateTimeFormat.FullDateTimePattern ?? CultureInfo.InvariantCulture.DateTimeFormat.FullDateTimePattern
                : this.OriginalField.Language?.CultureInfo?.DateTimeFormat.ShortDatePattern ?? CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern,
	            editing);
        }

		/// <summary>
		/// Renders the date time in specified format.
		/// </summary>
		/// <param name="dateTimeFormat">The date time format.</param>
		/// <param name="editing">Specify if the field should be editable</param>
		/// <returns></returns>
		public override IHtmlString Render(string dateTimeFormat = null, bool editing = true)
        {
            return base.Render("format=" + dateTimeFormat);
        }

		/// <summary>
		/// Performs an implicit conversion from <see cref="DateTimeFieldWrapper"/> to <see cref="DateTime"/>.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator DateTime(DateTimeFieldWrapper field)
        {
            return field.Value;
        }
    }
}
