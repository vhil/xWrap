namespace Xwrap.Mvc.RenderingParameters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
	using FieldWrappers.Abstractions;
	using Sitecore.Data;
    using Sitecore.Data.Items;

	/// <summary>
	/// Rendering parameters field wrapper for list Sitecore field types such as 'treelist' or 'multilist'. Implements <see cref="IFieldWrapper{IEnumerable{Guid}}"/>
	/// </summary>
	/// <seealso cref="Xwrap.Mvc.RenderingParameters.RenderingParametersFieldWrapper{System.Collections.Generic.IEnumerable{System.Guid}}" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IListFieldWrapper" />
	public class ListFieldWrapper : RenderingParametersFieldWrapper<IEnumerable<Guid>>, IListFieldWrapper
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ListFieldWrapper"/> class.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="value">The value.</param>
		public ListFieldWrapper(string fieldName, string value)
            : base(fieldName, value)
        {
        }

		/// <summary>
		/// Gets the list of selected items.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Item> GetItems()
        {
            foreach (var id in this.Value)
            {
                var item = Sitecore.Context.Database.GetItem(new ID(id));
                if (item != null)
                {
                    yield return item;
                }
            }
        }

		/// <summary>
		/// Wraps Sitecore items and returns <see cref="T:System.Collections.Generic.IEnumerable`1" /> of xWrap strongly typed item wrappers.
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
		public override IEnumerable<Guid> Value
        {
            get
            {
                if (!this.HasValue)
                {
                    yield break;
                }

                var list = this.RawValue.Split('|');

                foreach (var id in list)
                {
                    Guid guid;
                    if (Guid.TryParse(id, out guid))
                    {
                        yield return guid;
                    }
                }
            }
        }
    }
}