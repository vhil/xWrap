namespace Xwrap.FieldWrappers
{
    using System;
    using Sitecore;
    using System.Collections.Generic;
    using Abstractions;
    using Sitecore.Data.Fields;
	using System.Web;
    using Sitecore.Data.Items;

	public class NameLookupValueListFieldWrapper : FieldWrapper, INameLookupValueListFieldWrapper
    {
        public NameLookupValueListFieldWrapper(Field originalField) 
            : base(originalField)
        {
            this.InitializeValue();
        }

        public NameLookupValueListFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
            this.InitializeValue();
        }

        public IDictionary<string, Guid> Value { get; private set; }

        private void InitializeValue()
        {
            this.Value = new Dictionary<string, Guid>();
            var nvc = StringUtil.ParseNameValueCollection(HttpUtility.UrlDecode(this.RawValue), '&', '=');

            foreach (string key in nvc.Keys)
            {
	            if (Guid.TryParse(nvc[key], out var value))
                {
                    this.Value.Add(key, value);
                }
            }
        }
    }
}
