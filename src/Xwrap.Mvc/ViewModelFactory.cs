namespace Xwrap.Mvc
{
	using Sitecore.Data.Items;
	using Sitecore.Mvc.Presentation;
	using Sitecore.Configuration;
	using RenderingParameters;
	using Sitecore.Pipelines;
	using Pipelines.GetRenderingItem;

	public class ViewModelFactory : IViewModelFactory
	{
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

		public virtual IFormViewModel<TForm> GetViewModel<TForm>() where TForm : IFormData, new()
		{
			var viewModel = this.GetViewModel();
			return new FormViewModel<TForm>(new TForm(), viewModel);
		}

		public virtual IFormViewModel<TForm> GetViewModel<TForm>(TForm form) where TForm : IFormData
		{
			var viewModel = this.GetViewModel();
			return new FormViewModel<TForm>(form, viewModel);
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

		public virtual IRenderingParametersWrapper GetRenderingParameters()
		{
			var parameters = RenderingContext.Current.Rendering.Parameters;
			return new RenderingParametersWrapper(parameters);
		}
	}
}
