namespace Xwrap.Mvc.RenderingParameters
{
    using System;
    using FieldWrappers.Abstractions;

	/// <summary>
	/// Rendering parameters field wrapper for 'checkbox' Sitecore field type. Implements <see cref="IFieldWrapper{bool}"/>
	/// </summary>
	/// <seealso cref="Xwrap.Mvc.RenderingParameters.RenderingParametersFieldWrapper{System.Boolean}" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.ICheckboxFieldWrapper" />
	public class CheckboxFieldWrapper : RenderingParametersFieldWrapper<bool>, ICheckboxFieldWrapper
    {
		/// <summary>
		/// Gets a value indicating whether checkbox is checked
		/// </summary>
		public override bool Value => !string.IsNullOrWhiteSpace(this.RawValue) && this.RawValue.Equals("1");

		/// <summary>
		/// Initializes a new instance of the <see cref="CheckboxFieldWrapper"/> class.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="value">The value.</param>
		public CheckboxFieldWrapper(string fieldName, string value)
            : base(fieldName, value)
        {
        }

		/// <summary>
		/// Performs an implicit conversion from <see cref="CheckboxFieldWrapper"/> to <see cref="System.Boolean"/>.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator bool(CheckboxFieldWrapper field)
        {
            return field.Value;
        }

		/// <summary>
		/// Performs an implicit conversion from <see cref="CheckboxFieldWrapper"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator string(CheckboxFieldWrapper field)
        {
            return field.Value.ToString();
        }

		/// <summary>
		/// Performs an implicit conversion from <see cref="CheckboxFieldWrapper"/> to <see cref="System.Int32"/>.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator int(CheckboxFieldWrapper field)
        {
            return Convert.ToInt32(field.Value);
        }
    }
}