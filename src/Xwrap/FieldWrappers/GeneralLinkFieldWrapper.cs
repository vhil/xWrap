namespace Xwrap.FieldWrappers
{
	using System;
	using Abstractions;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using Sitecore.Resources.Media;
	using System.Web;
	using Sitecore.SecurityModel;

	/// <summary>
	/// Default field wrapper type for 'general link' Sitecore fields. Implements <see cref="IGeneralLinkFieldWrapper"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IGeneralLinkFieldWrapper" />
	public class GeneralLinkFieldWrapper : FieldWrapper, IGeneralLinkFieldWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GeneralLinkFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public GeneralLinkFieldWrapper(Field originalField)
			: base(originalField)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GeneralLinkFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public GeneralLinkFieldWrapper(BaseItem item, string fieldName)
			: base(item, fieldName)
		{
		}

		/// <summary>
		/// Gets the link field.
		/// </summary>
		protected LinkField LinkField => this.OriginalField;

		/// <summary>
		/// Gets the link alternate text.
		/// </summary>
		public virtual string AlternateText => this.LinkField.Title;

		/// <summary>
		/// Gets the link description.
		/// </summary>
		public virtual string Description => this.LinkField.Text;

		/// <summary>
		/// Gets a value indicating whether this link is internal.
		/// </summary>
		public virtual bool IsInternal => this.LinkField.IsInternal;

		/// <summary>
		/// Gets a value indicating whether this link is a media link.
		/// </summary>
		public virtual bool IsMediaLink => this.LinkField.IsMediaLink;

		/// <summary>
		/// Gets the link styles.
		/// </summary>
		public virtual string Styles => this.LinkField.Class;

		/// <summary>
		/// Gets the _target link property.
		/// </summary>
		public virtual string Target => this.LinkField.Target;

		/// <summary>
		/// Gets the value.
		/// </summary>
		public virtual string Value => this.Url;

		/// <summary>
		/// Gets the linked item ID.
		/// </summary>
		public virtual Guid ItemId => this.LinkField.TargetID.ToGuid();

		/// <summary>
		/// Gets the link field URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		public virtual string Url
		{
			get
			{
				var disabler = Settings.DisableSecurityOnLinkGeneration ? new SecurityDisabler() : null;

				if (this.IsMediaLink)
				{
					var mediaUrl = MediaManager.GetMediaUrl(this.LinkField.TargetItem);
					var url = HashingUtils.ProtectAssetUrl(mediaUrl);
					disabler?.Dispose();
					return url;
				}

				var friendlyUrl = this.LinkField.GetFriendlyUrl();
				disabler?.Dispose();
				return friendlyUrl;
			}
		}

		/// <summary>
		/// Gets the target linked item.
		/// </summary>
		/// <returns>
		/// Instance of <see cref="T:Sitecore.Data.Items.Item" />
		/// </returns>
		public virtual Item GetTarget()
		{
			if (this.IsInternal || this.IsMediaLink)
			{
				return this.LinkField.TargetItem;
			}

			return null;
		}

		/// <summary>
		/// Wraps the target linked Sitecore item and returns an xWrap strongly typed item wrapper.
		/// Returns null in case source item template does not match the target template ID.
		/// </summary>
		/// <typeparam name="TItemWrapper">Target field wrapper type, inherited from <see cref="T:Xwrap.FieldWrappers.Abstractions.IFieldWrapper" /></typeparam>
		/// <returns></returns>
		public virtual TItemWrapper WrapTarget<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			var target = this.GetTarget();

			if (target == null) return null;

			return this.Factory.WrapItem<TItemWrapper>(target);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="GeneralLinkFieldWrapper"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator string(GeneralLinkFieldWrapper field)
		{
			return field.Url;
		}

		public override IHtmlString RenderBeginField(object parameters, bool editing = true)
		{
			var disabler = Settings.DisableSecurityOnLinkGeneration ? new SecurityDisabler() : null;
			var url = base.RenderBeginField(parameters, editing);
			disabler?.Dispose();
			return url;
		}

		public override IHtmlString RenderBeginField(string parameters = null, bool editing = true)
		{
			var disabler = Settings.DisableSecurityOnLinkGeneration ? new SecurityDisabler() : null;
			var url = base.RenderBeginField(parameters, editing);
			disabler?.Dispose();
			return url;
		}
	}
}
