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
}
