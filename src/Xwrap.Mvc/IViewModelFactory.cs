namespace Xwrap.Mvc
{
	using RenderingParameters;
	using Sitecore.Data.Items;

	/// <summary>
	/// An abstraction for view model factory type. Creates instances of <see cref="IViewModel"/> types from Sitecore rendering context.
	/// </summary>
	public interface IViewModelFactory
	{
		/// <summary>
		/// Gets the view model.
		/// </summary>
		IViewModel GetViewModel();

		/// <summary>
		/// Gets the generic view model with specified rendering item wrapper type.
		/// </summary>
		/// <typeparam name="TRenderingItem">The type of the rendering item wrapper .</typeparam>
		IViewModel<TRenderingItem> GetViewModel<TRenderingItem>() where TRenderingItem : ItemWrapper;

		/// <summary>
		/// Gets the generic view model with specified rendering item wrapper and rendering parameters wrapper types.
		/// </summary>
		/// <typeparam name="TRenderingItem">The type of the rendering item.</typeparam>
		/// <typeparam name="TRenderingParameters">The type of the rendering parameters.</typeparam>
		IViewModel<TRenderingItem, TRenderingParameters> GetViewModel<TRenderingItem, TRenderingParameters>()
			where TRenderingItem : ItemWrapper
			where TRenderingParameters : RenderingParametersWrapper;

		/// <summary>
		/// Gets the page item.
		/// </summary>
		Item GetPageItem();

		/// <summary>
		/// Gets the rendering item.
		/// </summary>
		Item GetRenderingItem();

		/// <summary>
		/// Gets the generic rendering item.
		/// </summary>
		/// <typeparam name="TRenderingItem">The type of the rendering item.</typeparam>
		TRenderingItem GetRenderingItem<TRenderingItem>() where TRenderingItem : ItemWrapper;

		/// <summary>
		/// Gets rendering parameters.
		/// </summary>
		/// <returns></returns>
		IRenderingParametersWrapper GetRenderingParameters();

		/// <summary>
		/// Gets strongly typed rendering parameters wrapper.
		/// </summary>
		/// <typeparam name="TRenderingParameters">The type of the rendering parameters.</typeparam>
		/// <returns></returns>
		TRenderingParameters GetRenderingParameters<TRenderingParameters>() where TRenderingParameters : RenderingParametersWrapper;
	}
}
