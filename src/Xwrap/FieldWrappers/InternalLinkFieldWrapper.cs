namespace Xwrap.FieldWrappers
{
	using System;
	using Sitecore.Data;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using Sitecore.Links;
	using Sitecore.StringExtensions;
	using Abstractions;

	/// <summary>
	/// Default field wrapper type for 'internal link' Sitecore fields. Implements <see cref="IInternalLinkFieldWrapper"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IInternalLinkFieldWrapper" />
	public class InternalLinkFieldWrapper : FieldWrapper, IInternalLinkFieldWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InternalLinkFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public InternalLinkFieldWrapper(Field originalField)
			: base(originalField)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InternalLinkFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public InternalLinkFieldWrapper(BaseItem item, string fieldName)
			: base(item, fieldName)
		{
		}

		/// <summary>
		/// Gets the strongly typed field value.
		/// </summary>
		public string Value => this.Path;

		/// <summary>
		/// Gets the target item ID.
		/// </summary>
		/// <value>
		/// The target item ID.
		/// </value>
		public Guid TargetId
		{
			get
			{
				var target = this.GetTarget();

				return target?.ID.Guid ?? ID.Null.Guid;
			}
		}

		/// <summary>
		/// Gets the target item path.
		/// </summary>
		/// <value>
		/// The target item path.
		/// </value>
		public string Path => this.RawValue;

		/// <summary>
		/// Gets the target item URL.
		/// </summary>
		/// <value>
		/// The target item URL.
		/// </value>
		public string Url
		{
			get
			{
				var target = this.GetTarget();

				return target != null ? LinkManager.GetItemUrl(target) : string.Empty;
			}
		}

		/// <summary>
		/// Gets the target linked item.
		/// </summary>
		/// <returns></returns>
		public Item GetTarget()
		{
			return this.Path.IsNullOrEmpty()
				? null
				: this.OriginalField.Database.GetItem(this.Path);
		}

		/// <summary>
		/// Wraps the target linked Sitecore item and returns an xWrap strongly typed item wrapper.
		/// Returns null in case source item template does not match the target template ID.
		/// </summary>
		/// <typeparam name="TItemWrapper">Target field wrapper type, inherited from <see cref="IFieldWrapper" /></typeparam>
		/// <returns></returns>
		public TItemWrapper WrapTarget<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			var target = this.GetTarget();

			if (target == null) return null;

			return this.Factory.WrapItem<TItemWrapper>(target);
		}
	}
}
