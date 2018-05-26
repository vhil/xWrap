namespace Xwrap.Mvc
{
	using Sitecore.Data.Items;
	using Xwrap.Mvc.RenderingParameters;

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
}
