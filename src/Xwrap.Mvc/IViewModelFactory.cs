namespace Xwrap.Mvc
{
	using RenderingParameters;
	using Sitecore.Data.Items;

	public interface IViewModelFactory
	{
		IViewModel GetViewModel();
		IViewModel<TRenderingItem> GetViewModel<TRenderingItem>() where TRenderingItem : ItemWrapper;
		IViewModel<TRenderingItem, TRenderingParameters> GetViewModel<TRenderingItem, TRenderingParameters>() 
			where TRenderingItem : ItemWrapper
			where TRenderingParameters : RenderingParametersWrapper;
		Item GetPageItem();
		Item GetRenderingItem();
		TRenderingItem GetRenderingItem<TRenderingItem>() where TRenderingItem : ItemWrapper;
		IRenderingParametersWrapper GetRenderingParameters();
		TRenderingParameters GetRenderingParameters<TRenderingParameters>() where TRenderingParameters : RenderingParametersWrapper;
	}
}
