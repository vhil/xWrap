namespace Xwrap.FieldWrappers
{
    using System;
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

    public class CheckboxFieldWrapper : FieldWrapper, ICheckboxFieldWrapper
    {
        public CheckboxFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

        public CheckboxFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

        public bool Value => this.OriginalField.Value == "1";

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
