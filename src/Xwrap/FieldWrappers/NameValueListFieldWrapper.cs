namespace Xwrap.FieldWrappers
{
    using System.Collections.Specialized;
    using Abstractions;
    using Sitecore.Data.Fields;
	using System.Web;
	using Sitecore;
	using Sitecore.Data.Items;

	/// <summary>
	/// Default field wrapper type for 'name value list' Sitecore field types. Implements <see cref="IFieldWrapper{NameValueCollection}"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.INameValueListFieldWrapper" />
	public class NameValueListFieldWrapper : FieldWrapper, INameValueListFieldWrapper
    {
        private NameValueCollection value;

		/// <summary>
		/// Initializes a new instance of the <see cref="NameValueListFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public NameValueListFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="NameValueListFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public NameValueListFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

		/// <summary>
		/// Gets the strongly typed value.
		/// </summary>
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

		/// <summary>
		/// Gets a value indicating whether this field has a valid value.
		/// </summary>
		public override bool HasValue
        {
            get
            {
                this.InitializeValue();
                return this.value != null && this.value.Count > 0;
            }
        }

		/// <summary>
		/// Initializes the value.
		/// </summary>
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
