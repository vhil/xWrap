namespace Xwrap.Mvc.RenderingParameters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
	using FieldWrappers.Abstractions;
	using Sitecore.Data;
    using Sitecore.Data.Items;

    public class ListFieldWrapper : RenderingParametersFieldWrapper<IEnumerable<Guid>>, IListFieldWrapper
    {
        public ListFieldWrapper(string fieldName, string value)
            : base(fieldName, value)
        {
        }

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