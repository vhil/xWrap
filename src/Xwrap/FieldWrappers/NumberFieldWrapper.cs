namespace Xwrap.FieldWrappers
{
	using Abstractions;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using System.Globalization;

	/// <summary>
	/// Default field wrapper type for 'number' Sitecore field types. Implements <see cref="IFieldWrapper{decimal}"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.INumberFieldWrapper" />
	public class NumberFieldWrapper : FieldWrapper, INumberFieldWrapper
	{
		private decimal? value;

		/// <summary>
		/// Initializes a new instance of the <see cref="NumberFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public NumberFieldWrapper(Field originalField)
			: base(originalField)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NumberFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public NumberFieldWrapper(BaseItem item, string fieldName)
			: base(item, fieldName)
		{
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

		/// <summary>
		/// Gets the strongly typed value.
		/// </summary>
		public decimal Value
		{
			get
			{
				this.InitializeValue();
				return this.value ?? 0;
			}
		}

		/// <summary>
		/// Initializes the value.
		/// </summary>
		protected void InitializeValue()
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