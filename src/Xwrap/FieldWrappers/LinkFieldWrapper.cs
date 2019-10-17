namespace Xwrap.FieldWrappers
{
	using System;
	using Abstractions;
	using Sitecore.Data;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using Sitecore.Links;
	using System.Web;
	using Sitecore.SecurityModel;

	/// <summary>
	/// Default field wrapper type for item reference link Sitecore field types .e.g. 'droplink' Implements <see cref="ILinkFieldWrapper{Guid}"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.ILinkFieldWrapper" />
	public class LinkFieldWrapper : FieldWrapper, ILinkFieldWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LinkFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public LinkFieldWrapper(Field originalField)
			: base(originalField)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LinkFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public LinkFieldWrapper(BaseItem item, string fieldName)
			: base(item, fieldName)
		{
		}

		/// <summary>
		/// Gets the selected item ID.
		/// </summary>
		public virtual Guid ItemId => this.GetItemId(this.RawValue);

		/// <summary>
		/// Gets the selected item ID.
		/// </summary>
		public virtual Guid Value => this.GetItemId(this.RawValue);

		/// <summary>
		/// Gets the selected item URL.
		/// </summary>
		public virtual string Url
		{
			get
			{
				var disabler = Settings.DisableSecurityOnLinkGeneration ? new SecurityDisabler() : null;
				var target = this.GetTarget();
				var url = target == null ? string.Empty : LinkManager.GetItemUrl(target);
				disabler?.Dispose();
				return url;
			}
		}

		/// <summary>
		/// Gets the target item.
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Wraps the target Sitecore item and returns an xWrap strongly typed item wrapper.
		/// Returns null in case source item template does not match the target template ID.
		/// </summary>
		public virtual TItemWrapper WrapTarget<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			var target = this.GetTarget();

			if (target == null) return null;

			return this.Factory.WrapItem<TItemWrapper>(target);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="LinkFieldWrapper"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator string(LinkFieldWrapper field)
		{
			return field.Url;
		}

		/// <summary>
		/// Gets the target item.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		protected Item GetTarget(ID id)
		{
			return ID.IsNullOrEmpty(id) ? null : this.OriginalField.Database.GetItem(id, this.OriginalField.Language);
		}

		/// <summary>
		/// Gets the target item ID.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
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
