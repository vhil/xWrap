namespace Xwrap.Mvc.Pipelines.GetRenderingItem
{
	using Sitecore.Data.Items;
	using Sitecore.Diagnostics;

	public abstract class GetRenderingItemProcessor
	{
		public void Process(GetRenderingItemArgs args)
		{
			Assert.IsNotNull(args, nameof(args));
			if (args?.RenderingItem != null) return;

			args.RenderingItem = this.GetRenderingItem(args);
		}

		protected abstract Item GetRenderingItem(GetRenderingItemArgs args);
	}
}
