namespace Xwrap.FieldWrappers
{
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

    public class IntegerFieldWrapper : FieldWrapper, IIntegerFieldWrapper
    {
        private int? value;

        public IntegerFieldWrapper(Field originalField) 
            : base(originalField)
        {
            this.value = null;
        }

        public IntegerFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
            this.value = null;
        }

        public int Value
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