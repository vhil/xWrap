namespace Xwrap.Mvc.RenderingParameters
{
	using FieldWrappers.Abstractions;
	using System.Globalization;

	/// <summary>
	/// Rendering parameters field wrapper for 'number' Sitecore field types. Implements <see cref="IFieldWrapper{decimal}"/>
	/// </summary>
	/// <seealso cref="Xwrap.Mvc.RenderingParameters.RenderingParametersFieldWrapper{System.Decimal}" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.INumberFieldWrapper" />
	public class NumberFieldWrapper : RenderingParametersFieldWrapper<decimal>, INumberFieldWrapper
    {
        private decimal? value;

		/// <summary>
		/// Initializes a new instance of the <see cref="NumberFieldWrapper"/> class.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="value">The value.</param>
		public NumberFieldWrapper(string fieldName, string value)
            : base(fieldName, value)
        {
        }

		/// <summary>
		/// Gets the strongly typed value.
		/// </summary>
		public override decimal Value
        {
            get
            {
                this.InitializeValue();
                return this.value ?? 0;
            }
        }

		/// <summary>
		/// Gets a value indicating whether this field has a valid value.
		/// </summary>
		public override bool HasValue
        {
            get
            {
                this.InitializeValue();
                return this.value.HasValue;
            }
        }

        private void InitializeValue()
        {
            if (!this.value.HasValue)
			{ 
                if (decimal.TryParse(this.RawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedValue))
                {
                    this.value = parsedValue;
                }
            }
        }
    }
}