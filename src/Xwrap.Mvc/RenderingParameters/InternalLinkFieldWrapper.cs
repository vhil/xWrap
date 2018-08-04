namespace Xwrap.Mvc.RenderingParameters
{
	using System;
	using Sitecore.Data;
	using Sitecore.Data.Items;
	using Sitecore.Links;
	using FieldWrappers.Abstractions;

	public class InternalLinkFieldWrapper : RenderingParametersFieldWrapper<string>, IInternalLinkFieldWrapper
	{
		public InternalLinkFieldWrapper(string fieldName, string value) 
			: base(fieldName, value)
		{
		}

		public override string Value => this.Path;

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

		public virtual Item GetTarget()
		{
			return this.HasValue 
				? Sitecore.Context.Database.GetItem(this.RawValue) 
				: null;
		}

		public virtual TItemWrapper GetTarget<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			var item = this.GetTarget();

			if (item == null) return null;

			return this.Factory.WrapItem<TItemWrapper>(item);
		}
	}
}