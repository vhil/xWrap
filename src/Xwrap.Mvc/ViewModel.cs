namespace Xwrap.Mvc
{
	using Sitecore.Data.Items;
	using RenderingParameters;

	public class ViewModel : IViewModel
	{
		public ViewModel(IViewModel viewModel) 
			: this(viewModel.PageItem, viewModel.RenderingItem, viewModel.RenderingParameters)
		{
		}

		public ViewModel(
			Item pageItem,
			Item renderingItem,
			IRenderingParametersWrapper renderingParameters)
		{
			this.PageItem = pageItem;
			this.RenderingItem = renderingItem;
			this.RenderingParameters = renderingParameters;
		}

		public Item PageItem { get; }
		public Item RenderingItem { get; }
		public IRenderingParametersWrapper RenderingParameters { get; }
	}

	public class ViewModel<TRenderingItem> : IViewModel<TRenderingItem>
		where TRenderingItem : ItemWrapper
	{
		public ViewModel(IViewModel<TRenderingItem> viewModel)
			: this(viewModel.PageItem, viewModel.RenderingItem)
		{
		}

		public ViewModel(Item pageItem, TRenderingItem renderingItem)
		{
			this.PageItem = pageItem;
			this.RenderingItem = renderingItem;
		}

		public Item PageItem { get; }
		public TRenderingItem RenderingItem { get; }
	}

	public class ViewModel<TRenderingItem, TRenderingParameters> : ViewModel<TRenderingItem>, IViewModel<TRenderingItem, TRenderingParameters>
		where TRenderingItem : ItemWrapper
		where TRenderingParameters : RenderingParametersWrapper
	{
		public ViewModel(IViewModel<TRenderingItem, TRenderingParameters> viewModel)
			: this(viewModel.PageItem, viewModel.RenderingItem, viewModel.RenderingParameters)
		{
		}

		public ViewModel(Item pageItem, TRenderingItem renderingItem, TRenderingParameters renderingParameters)
			:base(pageItem, renderingItem)
		{
			this.RenderingParameters = renderingParameters;
		}

		public TRenderingParameters RenderingParameters { get; }
	}
}
