﻿namespace Xwrap.Mvc.Pipelines.GetRenderingItem
{
	using Sitecore.Data.Items;

	/// <summary>
	/// xWrap getRenderingItem pipeline processor. Tries to resolve rendering item from RenderingContext.Current.Rendering.Item
	/// </summary>
	/// <seealso cref="Xwrap.Mvc.Pipelines.GetRenderingItem.GetRenderingItemProcessor" />
	public class GetFromRenderingContextItem : GetRenderingItemProcessor
	{
		/// <summary>
		/// Gets the rendering item.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		protected override Item GetRenderingItem(GetRenderingItemArgs args)
		{
			return args.RenderingContext.Rendering.Item;
		}
	}
}