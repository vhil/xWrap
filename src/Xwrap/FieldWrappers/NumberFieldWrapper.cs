namespace Xwrap.FieldWrappers
{
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using System.Globalization;

	public class NumberFieldWrapper : FieldWrapper, INumberFieldWrapper
    {
        private decimal? value;

        public NumberFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

        public NumberFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

        public override bool HasValue
        {
            get
            {
                this.InitializeValue();
                return this.value.HasValue;
            }
        }

        public decimal Value
        {
            get
            {
                this.InitializeValue();
                return this.value ?? 0;
            }
        }

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