namespace Xwrap.Mvc.RenderingParameters
{
	using FieldWrappers.Abstractions;

	public class IntegerFieldWrapper : RenderingParametersFieldWrapper<int>, IIntegerFieldWrapper
    {
        private int? value;

        public IntegerFieldWrapper(string fieldName, string value)
            : base(fieldName, value)
        {
        }

        public override int Value
        {
            get
            {
                this.InitializeValue();
                return this.value ?? 0;
            }
        }

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