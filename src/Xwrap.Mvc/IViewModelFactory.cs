namespace Xwrap.Mvc
{
	using Xwrap.Mvc.RenderingParameters;
	using Sitecore.Data.Items;

	public interface IViewModelFactory
	{
		IViewModel GetViewModel();
		IFormViewModel<TForm> GetViewModel<TForm>() where TForm : IFormData, new();
		IFormViewModel<TForm> GetViewModel<TForm>(TForm form) where TForm : IFormData;
		Item GetPageItem();
		Item GetRenderingItem();
		IRenderingParametersWrapper GetRenderingParameters();
	}
}
