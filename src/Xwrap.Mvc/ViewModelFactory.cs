namespace Xwrap.Mvc
{
	using Sitecore.Data.Items;
	using Sitecore.Mvc.Presentation;
	using Sitecore.Configuration;
	using RenderingParameters;
	using Sitecore.Pipelines;
	using Pipelines.GetRenderingItem;
	using System;

	/// <summary>
	/// Default implementation for view model factory type. Creates instances of <see cref="IViewModel"/> types from Sitecore rendering context.
	/// </summary>
	/// <seealso cref="Xwrap.Mvc.IViewModelFactory" />
	public class ViewModelFactory : IViewModelFactory
	{
		/// <summary>
		/// The item wrapper factory dependency
		/// </summary>
		protected readonly IItemWrapperFactory ItemWrapperFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModelFactory"/> class.
		/// </summary>
		/// <param name="itemWrapperFactory">The item wrapper factory.</param>
		public ViewModelFactory(IItemWrapperFactory itemWrapperFactory)
		{
			this.ItemWrapperFactory = itemWrapperFactory;
		}

		/// <summary>
		/// Gets the configured instance of <see cref="IViewModelFactory"/> in sitecore configuration by 'xWrap/mvc/viewModelFactory' path.
		/// </summary>
		public static IViewModelFactory Instance => Factory.CreateObject("xWrap/mvc/viewModelFactory", true) as IViewModelFactory;

		/// <summary>
		/// Gets the view model.
		/// </summary>
		/// <returns></returns>
		public virtual IViewModel GetViewModel()
		{
			return new ViewModel(
				this.GetPageItem(),
				this.GetRenderingItem(),
				this.GetRenderingParameters());
		}

		/// <summary>
		/// Gets the generic view model with specified rendering item wrapper type.
		/// </summary>
		/// <typeparam name="TRenderingItem">The type of the rendering item wrapper .</typeparam>
		/// <returns></returns>
		public IViewModel<TRenderingItem> GetViewModel<TRenderingItem>() where TRenderingItem : ItemWrapper
		{
			return new ViewModel<TRenderingItem>(
				this.GetPageItem(),
				this.GetRenderingItem<TRenderingItem>());
		}

		/// <summary>
		/// Gets the generic view model with specified rendering item wrapper and rendering parameters wrapper types.
		/// </summary>
		/// <typeparam name="TRenderingItem">The type of the rendering item.</typeparam>
		/// <typeparam name="TRenderingParameters">The type of the rendering parameters.</typeparam>
		/// <returns></returns>
		public IViewModel<TRenderingItem, TRenderingParameters> GetViewModel<TRenderingItem, TRenderingParameters>() 
			where TRenderingItem : ItemWrapper 
			where TRenderingParameters : RenderingParametersWrapper
		{
			return new ViewModel<TRenderingItem, TRenderingParameters>(
				this.GetPageItem(),
				this.GetRenderingItem<TRenderingItem>(),
				this.GetRenderingParameters<TRenderingParameters>());
		}

		/// <summary>
		/// Gets the page item.
		/// </summary>
		/// <returns></returns>
		public virtual Item GetPageItem()
		{
			return PageContext.Current.Item;
		}

		/// <summary>
		/// Gets the rendering item.
		/// </summary>
		/// <returns></returns>
		public virtual Item GetRenderingItem()
		{
			var args = new GetRenderingItemArgs(RenderingContext.Current);
			CorePipeline.Run("xWrap.getRenderingItem", args);
			return args.RenderingItem;
		}

		/// <summary>
		/// Gets the generic rendering item.
		/// </summary>
		/// <typeparam name="TRenderingItem">The type of the rendering item.</typeparam>
		/// <returns></returns>
		public TRenderingItem GetRenderingItem<TRenderingItem>() where TRenderingItem : ItemWrapper
		{
			return this.ItemWrapperFactory.WrapItem<TRenderingItem>(this.GetRenderingItem());
		}

		/// <summary>
		/// Gets rendering parameters.
		/// </summary>
		/// <returns></returns>
		public virtual IRenderingParametersWrapper GetRenderingParameters()
		{
			var parameters = RenderingContext.Current.Rendering.Parameters;
			return new RenderingParametersWrapper(parameters);
		}

		/// <summary>
		/// Gets strongly typed rendering parameters wrapper.
		/// </summary>
		/// <typeparam name="TRenderingParameters">The type of the rendering parameters.</typeparam>
		/// <returns></returns>
		public virtual TRenderingParameters GetRenderingParameters<TRenderingParameters>() 
			where TRenderingParameters : RenderingParametersWrapper
		{
			var parameters = RenderingContext.Current.Rendering.Parameters;
			return Activator.CreateInstance(typeof(TRenderingParameters), parameters) as TRenderingParameters;
		}
	}
}
