namespace Xwrap.Mvc.RenderingParameters
{
	using FieldWrappers.Abstractions;

	/// <summary>
	/// Rendering parameters field wrapper for 'integer' Sitecore field types. Implements <see cref="IFieldWrapper{int}"/>
	/// </summary>
	/// <seealso cref="Xwrap.Mvc.RenderingParameters.RenderingParametersFieldWrapper{System.Int32}" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IIntegerFieldWrapper" />
	public class IntegerFieldWrapper : RenderingParametersFieldWrapper<int>, IIntegerFieldWrapper
	{
		private int? value;

		/// <summary>
		/// Initializes a new instance of the <see cref="IntegerFieldWrapper"/> class.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="value">The value.</param>
		public IntegerFieldWrapper(string fieldName, string value)
			: base(fieldName, value)
		{
		}

		/// <summary>
		/// Gets the strongly typed value.
		/// </summary>
		public override int Value
		{
			get
			{
				this.InitializeValue();
				return this.value ?? 0;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has value.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has value; otherwise, <c>false</c>.
		/// </value>
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
				int parsedValue;

				if (int.TryParse(this.RawValue, out parsedValue))
				{
					this.value = parsedValue;
				}
			}
		}
	}
}