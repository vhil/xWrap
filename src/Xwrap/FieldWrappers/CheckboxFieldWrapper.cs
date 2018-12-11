namespace Xwrap.FieldWrappers
{
    using System;
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

	/// <summary>
	/// Default field wrapper type for 'checkbox' Sitecore fields. Implements <see cref="ICheckboxFieldWrapper"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.ICheckboxFieldWrapper" />
	public class CheckboxFieldWrapper : FieldWrapper, ICheckboxFieldWrapper
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="CheckboxFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public CheckboxFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="CheckboxFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public CheckboxFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

		/// <summary>
		/// Gets a value indicating whether this field is checked.
		/// </summary>
		/// <value>
		///   <c>true</c> if checked; otherwise, <c>false</c>.
		/// </value>
		public bool Value => this.OriginalField.Value == "1";

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
