namespace Xwrap.FieldWrappers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

    public class ListFieldWrapper : FieldWrapper, IListFieldWrapper
    {
        private IEnumerable<Guid> ids;

        public ListFieldWrapper(Field originalField) 
            : base(originalField)
        {
        }

        public ListFieldWrapper(BaseItem item, string fieldName)
            : base(item, fieldName)
        {
        }

        public override bool HasValue => this.ids != null && this.Value.Any();

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

	    public IEnumerable<TItemWrapper> WrapItems<TItemWrapper>() where TItemWrapper : ItemWrapper
	    {
			var items = this.GetItems();

			if (items == null) return null;

			return this.Factory.WrapItems<TItemWrapper>(items);
		}

	    public IEnumerator<Item> GetEnumerator()
        {
            return this.GetItems().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

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