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

	public class ItemWrapper
	{
		protected IItemWrapperFactory ItemWrapperFactory => Xwrap.ItemWrapperFactory.Instance;
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

		public ItemWrapper(Item item)
		{
			this.OriginalItem = item ?? throw new ItemWrappingException(
				$"Unable to wrap item. Constructor argument '{nameof(item)}' was null.");

			this.ValidateTemplate();
		}

		public IFieldWrapper this[string fieldName] => this.FieldWrapperFactory.WrapField(this.OriginalItem, fieldName);

		public IFieldWrapper this[ID fieldId] => this.FieldWrapperFactory.WrapField(this.OriginalItem, fieldId);

		/// <summary>
		/// Gets children of an item and returns <see cref="IEnumerable{TItemWrapper}"/> of xWrap strongly typed item wrappers.
		/// Child items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		public IEnumerable<TItemWrapper> GetChildren<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			return this.ItemWrapperFactory.GetChildren<TItemWrapper>(this);
		}

		/// <summary>
		/// Gets first child inherited from target template and returns an instance of xWrap strongly typed item wrapper.
		/// </summary>
		public TItemWrapper GetFirstChild<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			return this.GetChildren<TItemWrapper>().FirstOrDefault();
		}

		protected TFieldWrapper WrapField<TFieldWrapper>(string fieldName) where TFieldWrapper : IFieldWrapper
		{
			return this.FieldWrapperFactory.WrapField<TFieldWrapper>(this.OriginalItem, fieldName);
		}

		protected TFieldWrapper WrapField<TFieldWrapper>(ID fieldId) where TFieldWrapper : IFieldWrapper
		{
			return this.FieldWrapperFactory.WrapField<TFieldWrapper>(this.OriginalItem, fieldId);
		}

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