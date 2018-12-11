namespace Xwrap.Mvc.Pipelines.GetRenderingItem
{
	using Sitecore.Data.Items;
	using Sitecore.Pipelines;
	using Sitecore.Mvc.Presentation;
	using Sitecore.Diagnostics;

	/// <summary>
	/// xWrap getRenderingItem pipeline arguments
	/// </summary>
	/// <seealso cref="Sitecore.Pipelines.PipelineArgs" />
	public class GetRenderingItemArgs : PipelineArgs
	{
		/// <summary>
		/// Gets the rendering context.
		/// </summary>
		public RenderingContext RenderingContext { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="GetRenderingItemArgs"/> class.
		/// </summary>
		/// <param name="renderingContext">The rendering context.</param>
		public GetRenderingItemArgs(RenderingContext renderingContext)
		{
			Assert.IsNotNull(renderingContext, nameof(renderingContext));
			this.RenderingContext = renderingContext;
		}

		/// <summary>
		/// Gets or sets the rendering item.
		/// </summary>
		public Item RenderingItem { get; set; }
	}
}
