namespace Xwrap.FieldWrappers
{
	using System;
	using Sitecore;
	using System.Collections.Generic;
	using Abstractions;
	using Sitecore.Data.Fields;
	using System.Web;
	using Sitecore.Data.Items;

	/// <summary>
	/// Default field wrapper type for 'name look value list' Sitecore field types. Implements <see cref="IFieldWrapper{IDictionary{string, Guid}}"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.INameLookupValueListFieldWrapper" />
	public class NameLookupValueListFieldWrapper : FieldWrapper, INameLookupValueListFieldWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NameLookupValueListFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public NameLookupValueListFieldWrapper(Field originalField)
			: base(originalField)
		{
			this.InitializeValue();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NameLookupValueListFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public NameLookupValueListFieldWrapper(BaseItem item, string fieldName)
			: base(item, fieldName)
		{
			this.InitializeValue();
		}

		/// <summary>
		/// Gets the strongly typed value.
		/// </summary>
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
