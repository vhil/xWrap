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
        public string AlternateText => this.LinkField.Title;
        public string Description => this.LinkField.Text;
        public bool IsInternal => this.LinkField.IsInternal;
        public bool IsMediaLink => this.LinkField.IsMediaLink;
        public string Styles => this.LinkField.Class;
        public string Target => this.LinkField.Target;
        public string Value => this.Url;
        public Guid ItemId => this.LinkField.TargetID.ToGuid();

        public string Url
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

        public Item GetTarget()
        {
            if (this.IsInternal || this.IsMediaLink)
            {
                return this.LinkField.TargetItem;
            }

            return null;
        }

        public static implicit operator string(GeneralLinkFieldWrapper field)
        {
            return field.Url;
        }
    }
}
