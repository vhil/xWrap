namespace Xwrap.Mvc.Pipelines.GetRenderingItem
{
	using Sitecore.Data.Items;
	using Sitecore.Diagnostics;

	/// <summary>
	/// Abstract xWrap getRenderingItem pipeline processor
	/// </summary>
	public abstract class GetRenderingItemProcessor
	{
		/// <summary>
		/// Processes the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public void Process(GetRenderingItemArgs args)
		{
			Assert.IsNotNull(args, nameof(args));
			if (args?.RenderingItem != null) return;

			args.RenderingItem = this.GetRenderingItem(args);
		}

		/// <summary>
		/// Gets the rendering item.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		protected abstract Item GetRenderingItem(GetRenderingItemArgs args);
	}
}
