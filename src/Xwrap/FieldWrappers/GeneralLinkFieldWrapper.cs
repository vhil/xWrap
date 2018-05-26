namespace Xwrap.FieldWrappers
{
    using System;
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Links;
    using Sitecore.Resources.Media;

    public class GeneralLinkFieldWrapper : FieldWrapper, IGeneralLinkFieldWrapper
    {
        public GeneralLinkFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

        public GeneralLinkFieldWrapper(BaseItem item, string fieldName) 
            : base(item, fieldName)
        {
        }

        protected LinkField LinkField => this.OriginalField;
        public virtual string AlternateText => this.LinkField.Title;
        public virtual string Description => this.LinkField.Text;
        public virtual bool IsInternal => this.LinkField.IsInternal;
        public virtual bool IsMediaLink => this.LinkField.IsMediaLink;
        public virtual string Styles => this.LinkField.Class;
        public virtual string Target => this.LinkField.Target;
        public virtual string Value => this.Url;
        public virtual Guid ItemId => this.LinkField.TargetID.ToGuid();

        public virtual string Url
        {
            get
            {
                if (this.IsMediaLink)
                {
                    return MediaManager.GetMediaUrl(this.LinkField.TargetItem);
                }

                if (this.IsInternal)
                {
                    var target = this.GetTarget();

                    if (target != null)
                    {
                        return LinkManager.GetItemUrl(target);
                    }
                }

                return this.LinkField.Url;
            }
        }

        public virtual Item GetTarget()
        {
            if (this.IsInternal || this.IsMediaLink)
            {
                return this.LinkField.TargetItem;
            }

            return null;
        }

	    public virtual TItemWrapper GetTarget<TItemWrapper>() where TItemWrapper : ItemWrapper
	    {
		    var target = this.GetTarget();

		    if (target == null) return null;

		    return this.Factory.WrapItem<TItemWrapper>(target);
	    }

	    public static implicit operator string(GeneralLinkFieldWrapper field)
        {
            return field.Url;
        }
    }
}
