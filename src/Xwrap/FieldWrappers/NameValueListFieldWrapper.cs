namespace Xwrap.FieldWrappers
{
    using System.Collections.Specialized;
    using Abstractions;
    using Sitecore.Data.Fields;
	using System.Web;
	using Sitecore;
	using Sitecore.Data.Items;

	public class NameValueListFieldWrapper : FieldWrapper, INameValueListFieldWrapper
    {
        private NameValueCollection value;

        public NameValueListFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

        public NameValueListFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

        public NameValueCollection Value
        {
            get
            {
                this.InitializeValue();

                if (this.value == null)
                {
                    return new NameValueCollection();
                }

                return this.value;
            }
        }

        public override bool HasValue
        {
            get
            {
                this.InitializeValue();
                return this.value != null && this.value.Count > 0;
            }
        }

        protected void InitializeValue()
        {
            if (this.value == null)
            {
                if (!string.IsNullOrWhiteSpace(this.RawValue))
                {
                    this.value = StringUtil.ParseNameValueCollection(HttpUtility.UrlDecode(this.RawValue), '&', '=');
                }
            }
        }
    }
}
