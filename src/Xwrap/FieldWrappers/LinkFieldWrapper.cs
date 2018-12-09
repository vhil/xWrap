namespace Xwrap.FieldWrappers
{
    using System;
    using Abstractions;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Links;

    public class LinkFieldWrapper : FieldWrapper, ILinkFieldWrapper
    {
        public LinkFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

        public LinkFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

        public virtual Guid ItemId => this.GetItemId(this.RawValue);
        public virtual Guid Value => this.GetItemId(this.RawValue);

        public virtual string Url
        {
            get
            {
                var target = this.GetTarget();
                return target == null ? string.Empty : LinkManager.GetItemUrl(target);
            }
        }

        public virtual Item GetTarget()
        {
            if (string.IsNullOrWhiteSpace(this.RawValue))
            {
                return null;
            }
            if (ShortID.IsShortID(this.RawValue))
            {
                return this.GetTarget(ShortID.Parse(this.RawValue).ToID());
            }
            if (ID.IsID(this.RawValue))
            {
                return this.GetTarget(ID.Parse(this.RawValue));
            }

            return null;
        }

		public virtual TItemWrapper WrapTarget<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			var target = this.GetTarget();

			if (target == null) return null;

			return this.Factory.WrapItem<TItemWrapper>(target);
		}

		public static implicit operator string(LinkFieldWrapper field)
        {
            return field.Url;
        }

        protected Item GetTarget(ID id)
        {
            return ID.IsNullOrEmpty(id) ? null : this.OriginalField.Database.GetItem(id);
        }

        protected Guid GetItemId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Guid.Empty;
            }

            if (ShortID.IsShortID(value))
            {
                return ShortID.Parse(value).ToID().Guid;
            }

            if (ID.IsID(value))
            {
                return ID.Parse(value).Guid;
            }

            return Guid.Parse(value);
        }
    }
}
