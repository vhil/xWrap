namespace Xwrap.Mvc.RenderingParameters
{
    using System;
    using FieldWrappers.Abstractions;

	public class CheckboxFieldWrapper : RenderingParametersFieldWrapper<bool>, ICheckboxFieldWrapper
    {
        public override bool Value => !string.IsNullOrWhiteSpace(this.RawValue) && this.RawValue.Equals("1");

        public CheckboxFieldWrapper(string fieldName, string value)
            : base(fieldName, value)
        {
        }

        public static implicit operator bool(CheckboxFieldWrapper field)
        {
            return field.Value;
        }

        public static implicit operator string(CheckboxFieldWrapper field)
        {
            return field.Value.ToString();
        }

        public static implicit operator int(CheckboxFieldWrapper field)
        {
            return Convert.ToInt32(field.Value);
        }
    }
}