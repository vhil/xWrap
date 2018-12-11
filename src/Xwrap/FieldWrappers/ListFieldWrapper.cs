namespace Xwrap.FieldWrappers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

	/// <summary>
	/// Default field wrapper type for list Sitecore field types such as 'treelist' or 'multilist' Implements <see cref="IFieldWrapper{IEnumerable{Guid}}"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.FieldWrapper" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IListFieldWrapper" />
	public class ListFieldWrapper : FieldWrapper, IListFieldWrapper
    {
        private IEnumerable<Guid> ids;

		/// <summary>
		/// Initializes a new instance of the <see cref="ListFieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		public ListFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="ListFieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public ListFieldWrapper(BaseItem item, string fieldName)
            : base(item, fieldName)
        {
        }

		/// <summary>
		/// Gets a value indicating whether this field has a valid value.
		/// </summary>
		public override bool HasValue => this.ids != null && this.Value.Any();

		/// <summary>
		/// Gets the list of selected items.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Item> GetItems()
        {
            foreach (var id in this.Value)
            {
                var item = this.OriginalField.Database.GetItem(id.ToString());

                if (item != null)
                {
                    yield return item;
                }
            }
        }

		/// <summary>
		/// Wraps Sitecore items and returns <see cref="IEnumerable{TItemWrapper}" /> of xWrap strongly typed item wrappers.
		/// Items which are not inherited from target template are being skipped and not included into result.
		/// </summary>
		/// <typeparam name="TItemWrapper"></typeparam>
		/// <returns></returns>
		public IEnumerable<TItemWrapper> WrapItems<TItemWrapper>() where TItemWrapper : ItemWrapper
	    {
			var items = this.GetItems();

			if (items == null) return null;

			return this.Factory.WrapItems<TItemWrapper>(items);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<Item> GetEnumerator()
        {
            return this.GetItems().GetEnumerator();
        }

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

		/// <summary>
		/// Gets the enumerable of selected item IDs.
		/// </summary>
		public IEnumerable<Guid> Value
        {
            get
            {
                if (this.ids == null)
                {
                    var listField = (MultilistField)this.OriginalField;
                    var guids = new List<Guid>();

                    foreach (var id in listField.Items)
                    {
                        Guid guid;

                        if (Guid.TryParse(id, out guid))
                        {
                            guids.Add(guid);
                        }
                    }

                    this.ids = guids;
                }

                return this.ids;
            }
        }
    }
}