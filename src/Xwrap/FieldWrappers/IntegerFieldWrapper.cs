namespace Xwrap.FieldWrappers
{
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

	/// <summary>
	/// Default field wrapper type for 'integer' Sitecore fields. Implements <see cref="IIntegerFieldWrapper"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IIntegerFieldWrapper" />
	public class IntegerFieldWrapper : FieldWrapper, IIntegerFieldWrapper
    {
        private int? value;

		/// <summary>
		/// Initializes a new instance of the <see cref="IntegerFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public IntegerFieldWrapper(Field originalField) 
            : base(originalField)
        {
            this.value = null;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="IntegerFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public IntegerFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
            this.value = null;
        }

		/// <summary>
		/// Gets the strongly typed value.
		/// </summary>
		public int Value
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

		/// <summary>
		/// Initializes the value.
		/// </summary>
		protected void InitializeValue()
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