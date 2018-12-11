namespace Xwrap.Mvc.RenderingParameters
{
    using System;
	using FieldWrappers.Abstractions;
	using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Links;

	/// <summary>
	/// Rendering parameters field wrapper for item reference link Sitecore field types .e.g. 'droplink' Implements <see cref="ILinkFieldWrapper{Guid}"/>
	/// </summary>
	/// <seealso cref="Xwrap.Mvc.RenderingParameters.RenderingParametersFieldWrapper{System.Guid}" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.ILinkFieldWrapper" />
	public class LinkFieldWrapper : RenderingParametersFieldWrapper<Guid>, ILinkFieldWrapper
    {
        private Item target;

		/// <summary>
		/// Initializes a new instance of the <see cref="LinkFieldWrapper"/> class.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="value">The value.</param>
		public LinkFieldWrapper(string fieldName, string value)
            : base(fieldName, value)
        {

        }

		/// <summary>
		/// Gets the selected item as <see cref="Item"/>.
		/// </summary>
		protected virtual Item Target => this.target ?? (this.target = this.GetTarget());

		/// <summary>
		/// Gets the selected item URL.
		/// </summary>
		public virtual string Url => this.Target == null ? string.Empty : LinkManager.GetItemUrl(this.Target);

		/// <summary>
		/// Gets the selected item.
		/// </summary>
		/// <returns></returns>
		public virtual Item GetTarget()
        {
            if (this.HasValue)
            {
                return Sitecore.Context.Database.GetItem(this.RawValue);
            }

            return null;
        }

	    /// <summary>
	    /// Wraps Sitecore item and returns an xWrap strongly typed item wrapper.
	    /// Returns null in case source item template does not match the target template ID.
	    /// </summary>
		public virtual TItemWrapper WrapTarget<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			var item = this.GetTarget();

			if (item == null) return null;

			return this.Factory.WrapItem<TItemWrapper>(item);
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
		/// Gets the selected item ID.
		/// </summary>
		public override Guid Value => this.GetItemId(this.RawValue);

		/// <summary>
		/// Gets the item ID.
		/// </summary>
		public Guid ItemId => this.GetItemId(this.RawValue);

		/// <summary>
		/// Gets the item ID.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public Guid GetItemId(string value)
        {
            Guid id;

            if (!Guid.TryParse(value, out id))
            {
                id = default(Guid);
            }

            return id;
        }

		/// <summary>
		/// Gets a value indicating whether this field has a valid value.
		/// </summary>
		public override bool HasValue => ID.IsID(this.RawValue);
    }
}