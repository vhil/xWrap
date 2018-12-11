namespace Xwrap.Mvc.RenderingParameters
{
	using System;
	using Sitecore.Data;
	using Sitecore.Data.Items;
	using Sitecore.Links;
	using FieldWrappers.Abstractions;

	/// <summary>
	/// Rendering parameters field wrapper for 'internal link' Sitecore field types. Implements <see cref="IFieldWrapper{string}"/>
	/// </summary>
	/// <seealso cref="Xwrap.Mvc.RenderingParameters.RenderingParametersFieldWrapper{System.String}" />
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IInternalLinkFieldWrapper" />
	public class InternalLinkFieldWrapper : RenderingParametersFieldWrapper<string>, IInternalLinkFieldWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InternalLinkFieldWrapper"/> class.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="value">The value.</param>
		public InternalLinkFieldWrapper(string fieldName, string value) 
			: base(fieldName, value)
		{
		}

		/// <summary>
		/// Gets the selected item path.
		/// </summary>
		public override string Value => this.Path;

		/// <summary>
		/// Gets the target item ID.
		/// </summary>
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
		public string Path => this.RawValue;

		/// <summary>
		/// Gets the target item URL.
		/// </summary>
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
		public virtual Item GetTarget()
		{
			return this.HasValue 
				? Sitecore.Context.Database.GetItem(this.RawValue) 
				: null;
		}

		/// <summary>
		/// Wraps the target linked Sitecore item and returns an xWrap strongly typed item wrapper.
		/// Returns null in case source item template does not match the target template ID.
		/// </summary>
		/// <typeparam name="TItemWrapper">Target field wrapper type, inherited from <see cref="T:Xwrap.FieldWrappers.Abstractions.IFieldWrapper" /></typeparam>
		/// <returns></returns>
		public virtual TItemWrapper WrapTarget<TItemWrapper>() where TItemWrapper : ItemWrapper
		{
			var item = this.GetTarget();

			if (item == null) return null;

			return this.Factory.WrapItem<TItemWrapper>(item);
		}
	}
}