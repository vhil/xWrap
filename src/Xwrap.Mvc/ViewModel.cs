using Sitecore.Mvc.Presentation;
using Xwrap.Extensions;

namespace Xwrap.Mvc
{
	using Sitecore.Data.Items;
	using RenderingParameters;

	/// <summary>
	/// Default type for view model object that holds the key Sitecore rendering properties 
	/// </summary>
	/// <seealso cref="Xwrap.Mvc.IViewModel" />
	public class ViewModel : IViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModel"/> class.
		/// </summary>
		/// <param name="viewModel">The view model.</param>
		public ViewModel(IViewModel viewModel)
			: this(viewModel.PageItem, viewModel.RenderingItem, viewModel.RenderingParameters)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModel"/> class.
		/// </summary>
		/// <param name="pageItem">The page item.</param>
		/// <param name="renderingItem">The rendering item.</param>
		/// <param name="renderingParameters">The rendering parameters.</param>
		public ViewModel(
			Item pageItem,
			Item renderingItem,
			IRenderingParametersWrapper renderingParameters)
		{
			this.PageItem = pageItem;
			this.RenderingItem = renderingItem;
			this.RenderingParameters = renderingParameters;
		}

		/// <summary>
		/// Gets the page item. The context page item <see cref="PageContext" />.Current.Item, usually same item as <see cref="Sitecore.Context" />.Item.
		/// </summary>
		public Item PageItem { get; }

		/// <summary>
		/// Gets the rendering item. The item which is being rendered – equals to data source item or if the data source is not set – falls back to PageItem.
		/// The value is being resolved through 'xWrap.GetRenderingItem' pipeline.
		/// </summary>
		public Item RenderingItem { get; }

		/// <summary>
		/// Gets the rendering parameters wrapper.
		/// </summary>
		public IRenderingParametersWrapper RenderingParameters { get; }
	}

	/// <summary>
	/// Default generic type for view model type with strongly typed rendering item wrapper
	/// </summary>
	/// <typeparam name="TRenderingItem">The type of the rendering item wrapper.</typeparam>
	/// <seealso cref="Xwrap.Mvc.IViewModel" />
	public class ViewModel<TRenderingItem> : IViewModel<TRenderingItem>
		where TRenderingItem : ItemWrapper
	{
		/// <summary>
		/// The wrapped page item
		/// </summary>
		protected object wrappedPageItem;

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModel{TRenderingItem}"/> class.
		/// </summary>
		/// <param name="viewModel">The view model.</param>
		public ViewModel(IViewModel<TRenderingItem> viewModel)
			: this(viewModel.PageItem, viewModel.RenderingItem)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModel{TRenderingItem}"/> class.
		/// </summary>
		/// <param name="pageItem">The page item.</param>
		/// <param name="renderingItem">The rendering item.</param>
		public ViewModel(Item pageItem, TRenderingItem renderingItem)
		{
			this.PageItem = pageItem;
			this.RenderingItem = renderingItem;
		}

		/// <summary>
		/// Gets the page item. The context page item <see cref="T:Sitecore.Mvc.Presentation.PageContext" />.Current.Item, usually same item as <see cref="T:Sitecore.Context" />.Item.
		/// </summary>
		public Item PageItem { get; }

		/// <summary>
		/// Gets the strongly typed rendering item. The item which is being rendered – equals to data source item or if the data source is not set – falls back to PageItem.
		/// The value is being resolved through 'xWrap.GetRenderingItem' pipeline.
		/// </summary>
		public TRenderingItem RenderingItem { get; }

		/// <summary>
		/// Attempts to wrap PageItem into target <see cref="T:Xwrap.ItemWrapper" />type.
		/// Returns null if page item can't be wrapped into desired type.
		/// </summary>
		/// <typeparam name="TPageItem">The type of the page item.</typeparam>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public TPageItem WrapPageItem<TPageItem>() where TPageItem : ItemWrapper
		{
			if (!(this.wrappedPageItem is TPageItem))
			{
				this.wrappedPageItem = this.PageItem.WrapItem<TPageItem>();
			}

			return (TPageItem)this.wrappedPageItem;
		}
	}

	/// <summary>
	/// Default generic type for view model type with strongly typed rendering item wrapper and rendering parameters wrapper
	/// </summary>
	/// <typeparam name="TRenderingItem">The type of the rendering item.</typeparam>
	/// <typeparam name="TRenderingParameters">The type of the rendering parameters.</typeparam>
	/// <seealso cref="Xwrap.Mvc.IViewModel" />
	public class ViewModel<TRenderingItem, TRenderingParameters> : ViewModel<TRenderingItem>, IViewModel<TRenderingItem, TRenderingParameters>
		where TRenderingItem : ItemWrapper
		where TRenderingParameters : RenderingParametersWrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModel{TRenderingItem, TRenderingParameters}"/> class.
		/// </summary>
		/// <param name="viewModel">The view model.</param>
		public ViewModel(IViewModel<TRenderingItem, TRenderingParameters> viewModel)
			: this(viewModel.PageItem, viewModel.RenderingItem, viewModel.RenderingParameters)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModel{TRenderingItem, TRenderingParameters}"/> class.
		/// </summary>
		/// <param name="pageItem">The page item.</param>
		/// <param name="renderingItem">The rendering item.</param>
		/// <param name="renderingParameters">The rendering parameters.</param>
		public ViewModel(Item pageItem, TRenderingItem renderingItem, TRenderingParameters renderingParameters)
			: base(pageItem, renderingItem)
		{
			this.RenderingParameters = renderingParameters;
		}

		/// <summary>
		/// Gets the strongly typed rendering parameters wrapper.
		/// </summary>
		public TRenderingParameters RenderingParameters { get; }
	}
}
