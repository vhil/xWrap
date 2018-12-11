namespace Xwrap.Mvc.Pipelines.Mvc.GetModel
{
	using System.Web.Compilation;
	using Sitecore.Mvc.Pipelines.Response.GetModel;
	using Sitecore.Mvc.Presentation;
	using Extensions;
	using System;
	using System.Linq;
	using RenderingParameters;

	/// <summary>
	/// Tries to set the instance of <see cref="IViewModel"/> type for Sitecore view rendering.
	/// </summary>
	/// <seealso cref="Sitecore.Mvc.Pipelines.Response.GetModel.GetModelProcessor" />
	public class GetFromView : GetModelProcessor
	{
		/// <summary>
		/// Processes the pipeline step.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public override void Process(GetModelArgs args)
		{
			if (args.Result == null)
			{
				args.Result = this.GetFromViewPath(args.Rendering, args);
			}
		}

		/// <summary>
		/// Gets model from view path.
		/// </summary>
		/// <param name="rendering">The rendering.</param>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		protected virtual object GetFromViewPath(Rendering rendering, GetModelArgs args)
		{
			try
			{
				var path = rendering.Renderer is ViewRenderer renderer
					? renderer.ViewPath
					: rendering.ToString().Replace("View: ", string.Empty);

				if (string.IsNullOrWhiteSpace(path))
				{
					return null;
				}

				// Retrieve the compiled view
				var compiledViewType = BuildManager.GetCompiledType(path);
				var baseType = compiledViewType.BaseType;


				// Check to see if the view has been found and that it is a generic type
				if (baseType == null || !baseType.IsGenericType)
				{
					return null;
				}

				var modelType = baseType.GetGenericArguments()[0];
				var viewModelFactory = ViewModelFactory.Instance;

				// Check to see if no model has been set
				if (modelType.IsAssignableTo(typeof(IViewModel)))
				{
					return viewModelFactory.GetViewModel();
				}

				if (modelType.IsAssignableTo(typeof(IViewModel<ItemWrapper>))
					|| modelType.IsAssignableTo(typeof(IViewModel<ItemWrapper, RenderingParametersWrapper>)))
				{
					var modelGenericArgs = modelType.GetGenericArguments();

					var method = typeof(ViewModelFactory).GetMethods().FirstOrDefault(m =>
						string.Equals(m.Name, "GetViewModel") &&
						m.GetGenericArguments().Length.Equals(modelGenericArgs.Length));

					var genericMethod = method?.MakeGenericMethod(modelGenericArgs);

					return genericMethod?.Invoke(viewModelFactory, null);
				}

				return null;
			}
			catch
			{
				return null;
			}
		}
	}
}