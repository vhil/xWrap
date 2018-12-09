namespace Xwrap.FieldWrappers
{
	using System;
	using Sitecore.Data;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using Sitecore.Links;
	using Sitecore.StringExtensions;
	using Abstractions;

	public class InternalLinkFieldWrapper : FieldWrapper, IInternalLinkFieldWrapper
	{
		public InternalLinkFieldWrapper(Field originalField)
			: base(originalField)
		{
		}

		public InternalLinkFieldWrapper(BaseItem item, string fieldName)
			: base(item, fieldName)
		{
		}

		public string Value => this.Path;

		public Guid TargetId
		{
			get
			{
				var target = this.GetTarget();

				return target?.ID.Guid ?? ID.Null.Guid;
			}
		}

		public string Path => this.RawValue;

		public string Url
		{
			get
			{
				var target = this.GetTarget();

				return target != null ? LinkManager.GetItemUrl(target) : string.Empty;
			}
		}

		public Item GetTarget()
		{
			return this.Path.IsNullOrEmpty()
				? null 
				: this.OriginalField.Database.GetItem(this.Path);
		}

		public TItemWrapper WrapTarget<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			var target = this.GetTarget();

			if (target == null) return null;

			return this.Factory.WrapItem<TItemWrapper>(target);
		}
	}
}
