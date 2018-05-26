namespace Xwrap.Mvc.Pipelines.GetRenderingItem
{
	using Sitecore.Data.Items;
	using Sitecore.Pipelines;
	using Sitecore.Mvc.Presentation;
	using Sitecore.Diagnostics;

	public class GetRenderingItemArgs : PipelineArgs
	{
		public RenderingContext RenderingContext { get; }

		public GetRenderingItemArgs(RenderingContext renderingContext)
		{
			Assert.IsNotNull(renderingContext, nameof(renderingContext));
			this.RenderingContext = renderingContext;
		}

		public Item RenderingItem { get; set; }
	}
}
