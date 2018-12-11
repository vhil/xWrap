namespace Xwrap.FieldWrappers.Abstractions
{
    using System;
    using System.Web;

	/// <summary>
	/// Field wrapper abstraction for 'date time' based Sitecore field types. Implements <see cref="IFieldWrapper{DateTime}"/>
	/// </summary>
	public interface IDateTimeFieldWrapper : IFieldWrapper<DateTime>
    {
		/// <summary>
		/// Renders date time field
		/// </summary>
		/// <param name="includeTime">Specify if time part should be included</param>
		/// <param name="editing">Specify if the field should be editable</param>
		IHtmlString Render(bool includeTime, bool editing = true);
    }
}
