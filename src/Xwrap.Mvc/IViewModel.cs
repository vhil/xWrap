namespace Xwrap.Mvc
{
	using Sitecore.Data.Items;
	using RenderingParameters;

	public interface IViewModel
	{
		Item PageItem { get; }
		Item RenderingItem { get; }
		IRenderingParametersWrapper RenderingParameters { get; }
	}

	public interface IViewModel<out TRenderingItem>
		where TRenderingItem : ItemWrapper
	{
		Item PageItem { get; }
		TRenderingItem RenderingItem { get; }
	}

	public interface IViewModel<out TRenderingItem, out TRenderingParametersWrapper> : IViewModel<TRenderingItem>
		where TRenderingItem : ItemWrapper
		where TRenderingParametersWrapper : RenderingParametersWrapper
	{
		TRenderingParametersWrapper RenderingParameters { get; }
	}
}
