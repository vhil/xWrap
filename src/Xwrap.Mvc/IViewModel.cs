namespace Xwrap.Mvc
{
	using Sitecore.Data.Items;
	using RenderingParameters;
	using Sitecore.Mvc.Presentation;

	/// <summary>
	/// Abstraction for view model type that holds the key Sitecore rendering properties 
	/// </summary>
	public interface IViewModel
	{
		/// <summary>
		/// Gets the page item. The context page item <see cref="PageContext"/>.Current.Item, usually same item as <see cref="Sitecore.Context"/>.Item.
		/// </summary>
		Item PageItem { get; }

		/// <summary>
		/// Gets the rendering item. The item which is being rendered – equals to data source item or if the data source is not set – falls back to PageItem.
		/// The value is being resolved through 'xWrap.GetRenderingItem' pipeline.
		/// </summary>
		Item RenderingItem { get; }

		/// <summary>
		/// Gets the rendering parameters wrapper.
		/// </summary>
		IRenderingParametersWrapper RenderingParameters { get; }
	}

	/// <summary>
	/// A generic abstraction for view model type with strongly typed rendering item wrapper
	/// </summary>
	/// <typeparam name="TRenderingItem">The type of the rendering item wrapper.</typeparam>
	public interface IViewModel<out TRenderingItem>
		where TRenderingItem : ItemWrapper
	{
		/// <summary>
		/// Gets the page item. The context page item <see cref="PageContext"/>.Current.Item, usually same item as <see cref="Sitecore.Context"/>.Item.
		/// </summary>
		Item PageItem { get; }

		/// <summary>
		/// Gets the strongly typed rendering item. The item which is being rendered – equals to data source item or if the data source is not set – falls back to PageItem.
		/// The value is being resolved through 'xWrap.GetRenderingItem' pipeline.
		/// </summary>
		TRenderingItem RenderingItem { get; }

		/// <summary>
		/// Attempts to wrap PageItem into target <see cref="ItemWrapper"/>type.
		/// Returns null if page item can't be wrapped into desired type.
		/// </summary>
		/// <typeparam name="TPageItem">The type of the page item.</typeparam>
		/// <returns></returns>
		TPageItem WrapPageItem<TPageItem>() where TPageItem : ItemWrapper;
	}

	/// <summary>
	/// A generic abstraction for view model type with strongly typed rendering item wrapper and rendering parameters wrapper
	/// </summary>
	/// <typeparam name="TRenderingItem">The type of the rendering item wrapper.</typeparam>
	/// <typeparam name="TRenderingParametersWrapper">The type of the rendering parameters wrapper.</typeparam>
	public interface IViewModel<out TRenderingItem, out TRenderingParametersWrapper> : IViewModel<TRenderingItem>
		where TRenderingItem : ItemWrapper
		where TRenderingParametersWrapper : RenderingParametersWrapper
	{
		/// <summary>
		/// Gets the strongly typed rendering parameters wrapper.
		/// </summary>
		TRenderingParametersWrapper RenderingParameters { get; }
	}
}
