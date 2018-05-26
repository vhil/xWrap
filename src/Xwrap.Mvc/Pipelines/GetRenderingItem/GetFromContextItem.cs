namespace Xwrap.Mvc.Pipelines.GetRenderingItem
{
	using Sitecore;
	using Sitecore.Data.Items;

	public class GetFromContextItem : GetRenderingItemProcessor
	{
		protected override Item GetRenderingItem(GetRenderingItemArgs args)
		{
			return Context.Item;
		}
	}
}
