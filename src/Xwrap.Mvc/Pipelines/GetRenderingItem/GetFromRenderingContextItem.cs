namespace Xwrap.Mvc.Pipelines.GetRenderingItem
{
	using Sitecore.Data.Items;

	public class GetFromRenderingContextItem : GetRenderingItemProcessor
	{
		protected override Item GetRenderingItem(GetRenderingItemArgs args)
		{
			return args.RenderingContext.Rendering.Item;
		}
	}
}