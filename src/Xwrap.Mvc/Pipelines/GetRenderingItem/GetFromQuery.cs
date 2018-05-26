namespace Xwrap.Mvc.Pipelines.GetRenderingItem
{
	using System;
	using Sitecore.Data.Items;

	public class GetFromQuery : GetRenderingItemProcessor
	{
		protected override Item GetRenderingItem(GetRenderingItemArgs args)
		{
			// Check for a sitecore query datasource
			var query = args.RenderingContext.Rendering.DataSource;
			if (query.StartsWith("./", StringComparison.InvariantCulture))
			{
				// Relative to the current context item
				var contextItem = args.RenderingContext.PageContext.Item;
				if (contextItem != null)
				{
					return contextItem.Axes.SelectSingleItem(query);
				}
			}
			else if (!string.IsNullOrEmpty(query))
			{
				// Straight sitecore query
				return args.RenderingContext.ContextItem.Database.SelectSingleItem(query);
			}

			return null;
		}
	}
}
