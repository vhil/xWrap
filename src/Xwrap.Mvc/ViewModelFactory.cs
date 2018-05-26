namespace Xwrap.Mvc
{
	using Sitecore.Data.Items;
	using Sitecore.Mvc.Presentation;
	using Sitecore.Configuration;
	using RenderingParameters;
	using Sitecore.Pipelines;
	using Pipelines.GetRenderingItem;
	using System;

	public class ViewModelFactory : IViewModelFactory
	{
		protected readonly IItemWrapperFactory ItemWrapperFactory;

		public ViewModelFactory(IItemWrapperFactory itemWrapperFactory)
		{
			this.ItemWrapperFactory = itemWrapperFactory;
		}

		public static IViewModelFactory Instance()
		{
			return Factory.CreateObject("xWrap/mvc/viewModelFactory", true) as IViewModelFactory;
		}

		public virtual IViewModel GetViewModel()
		{
			return new ViewModel(
				this.GetPageItem(),
				this.GetRenderingItem(),
				this.GetRenderingParameters());
		}

		public IViewModel<TRenderingItem> GetViewModel<TRenderingItem>() where TRenderingItem : ItemWrapper
		{
			return new ViewModel<TRenderingItem>(
				this.GetPageItem(),
				this.GetRenderingItem<TRenderingItem>());
		}

		public IViewModel<TRenderingItem, TRenderingParameters> GetViewModel<TRenderingItem, TRenderingParameters>() 
			where TRenderingItem : ItemWrapper 
			where TRenderingParameters : RenderingParametersWrapper
		{
			return new ViewModel<TRenderingItem, TRenderingParameters>(
				this.GetPageItem(),
				this.GetRenderingItem<TRenderingItem>(),
				this.GetRenderingParameters<TRenderingParameters>());
		}

		public virtual Item GetPageItem()
		{
			return PageContext.Current.Item;
		}

		public virtual Item GetRenderingItem()
		{
			var args = new GetRenderingItemArgs(RenderingContext.Current);
			CorePipeline.Run("xWrap.getRenderingItem", args);
			return args.RenderingItem;
		}

		public TRenderingItem GetRenderingItem<TRenderingItem>() where TRenderingItem : ItemWrapper
		{
			return this.ItemWrapperFactory.WrapItem<TRenderingItem>(this.GetRenderingItem());
		}

		public virtual IRenderingParametersWrapper GetRenderingParameters()
		{
			var parameters = RenderingContext.Current.Rendering.Parameters;
			return new RenderingParametersWrapper(parameters);
		}

		public TRenderingParameters GetRenderingParameters<TRenderingParameters>() 
			where TRenderingParameters : RenderingParametersWrapper
		{
			var parameters = RenderingContext.Current.Rendering.Parameters;
			return Activator.CreateInstance(typeof(TRenderingParameters), parameters) as TRenderingParameters;
		}
	}
}
