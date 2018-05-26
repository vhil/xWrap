namespace Xwrap.Mvc.Pipelines.GetRenderingItem
{
	using Sitecore.Data.Items;

	public class GetFromPageContextItem : GetRenderingItemProcessor
	{
		protected override Item GetRenderingItem(GetRenderingItemArgs args)
		{
			return args.RenderingContext.PageContext.Item;
		}
	}
}
