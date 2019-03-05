namespace Xwrap
{
	using System;
	using Sitecore.Data.Items;
	using Exceptions;
	using System.Collections.Generic;
	using Extensions;
	using System.Linq;
	using Sitecore.Data;
	using FieldWrappers.Abstractions;
	using Sitecore;

	/// <summary>
	/// The base class for all xWrap item wrappers.
	/// </summary>
	public class ItemWrapper
	{
		/// <summary>
		/// Gets the item wrapper factory.
		/// </summary>
		protected IItemWrapperFactory ItemWrapperFactory => Xwrap.ItemWrapperFactory.Instance;

		/// <summary>
		/// Gets the field wrapper factory.
		/// </summary>
		protected IFieldWrapperFactory FieldWrapperFactory => Xwrap.FieldWrapperFactory.Instance;

		/// <summary>
		/// The original Sitecore item, the item wrapper was created of.
		/// </summary>
		public Item OriginalItem { get; }

		/// <summary>
		/// Returns this.OriginalItem.ID.Guid
		/// </summary>
		public Guid Id => this.OriginalItem.ID.Guid;

		/// <summary>
		/// returns this.OriginalItem.TemplateID.Guid
		/// </summary>
		public Guid TemplateId => this.OriginalItem.TemplateID.Guid;

		/// <summary>
		/// returns this.OriginalItem.Name
		/// </summary>
		public string Name => this.OriginalItem.Name;

		/// <summary>
		/// returns this.OriginalItem.DisplayName
		/// </summary>
		public string DisplayName => this.OriginalItem.DisplayName;

		/// <summary>
		/// returns this.OriginalItem.Paths.FullPath
		/// </summary>
		public string FullPath => this.OriginalItem.Paths.FullPath;

		/// <summary>
		/// returns strongly typed <see cref="IDateTimeFieldWrapper"/> taken from "__Created" sitecore item field
		/// </summary>
		public IDateTimeFieldWrapper CreatedDate => this.WrapField<IDateTimeFieldWrapper>(FieldIDs.Created);

		/// <summary>
		/// returns strongly typed <see cref="IDateTimeFieldWrapper"/> taken from "__Updated" sitecore item field
		/// </summary>
		public IDateTimeFieldWrapper UpdatedDate => this.WrapField<IDateTimeFieldWrapper>(FieldIDs.Updated);

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <exception cref="Xwrap.Exceptions.ItemWrappingException">Unable to wrap item. Constructor argument '{nameof(item)}</exception>
		public ItemWrapper(Item item)
		{
			this.OriginalItem = item ?? throw new ItemWrappingException(
				$"Unable to wrap item. Constructor argument '{nameof(item)}' was null.");

			this.ValidateTemplate();
		}

		/// <summary>
		/// Gets the <see cref="IFieldWrapper"/> by the specified field name.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		public IFieldWrapper this[string fieldName] => this.FieldWrapperFactory.WrapField(this.OriginalItem, fieldName);

		/// <summary>
		/// Gets the <see cref="IFieldWrapper"/> by the specified field ID.
		/// </summary>
		/// <param name="fieldId">The field ID.</param>
		public IFieldWrapper this[ID fieldId] => this.FieldWrapperFactory.WrapField(this.OriginalItem, fieldId);

		/// <summary>
		/// Gets children of an item and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		public IEnumerable<TItemWrapper> WrapChildren<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			return this.ItemWrapperFactory.WrapChildren<TItemWrapper>(this);
		}

		/// <summary>
		/// Gets first child inherited from target template and returns an instance of xWrap strongly typed item wrapper.
		/// </summary>
		public TItemWrapper WrapFirstChild<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			return this.WrapChildren<TItemWrapper>().FirstOrDefault();

		}

		/// <summary>
		/// Gets first parent item inherited from target template and returns an instance of xWrap strongly typed item wrapper.
		/// </summary>
		public TItemWrapper WrapFirstParent<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			return this.ItemWrapperFactory.WrapFirstParent<TItemWrapper>(this.OriginalItem);
		}

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <typeparam name="TFieldWrapper">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="System.ArgumentNullException">if one of input parameters is null</exception>
		protected TFieldWrapper WrapField<TFieldWrapper>(string fieldName) where TFieldWrapper : IFieldWrapper
		{
			return this.FieldWrapperFactory.WrapField<TFieldWrapper>(this.OriginalItem, fieldName);
		}

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <typeparam name="TFieldWrapper">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="System.ArgumentNullException">if one of input parameters is null</exception>
		protected TFieldWrapper WrapField<TFieldWrapper>(ID fieldId) where TFieldWrapper : IFieldWrapper
		{
			return this.FieldWrapperFactory.WrapField<TFieldWrapper>(this.OriginalItem, fieldId);
		}

		/// <summary>
		/// Validates the template.
		/// </summary>
		/// <exception cref="Xwrap.Exceptions.ItemWrappingException">Unable to wrap item '{this.FullPath}' of template {this.TemplateId}. " +
		/// 					$"'{this.GetType().Name}' item wrapper expects item of {expectedTemplateAttr.TemplateId}</exception>
		protected virtual void ValidateTemplate()
		{
			var expectedTemplateAttr = this.GetTemplateIdAttribute();

			if (expectedTemplateAttr != null && !this.OriginalItem.IsDerived(expectedTemplateAttr.TemplateId))
			{
				throw new ItemWrappingException(
					$"Unable to wrap item '{this.FullPath}' of template {this.TemplateId}. " +
					$"'{this.GetType().Name}' item wrapper expects item of {expectedTemplateAttr.TemplateId} template ID.");
			}
		}
	}
}