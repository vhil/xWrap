
namespace Xwrap.Mvc.Pipelines.Mvc.GetModel
{
	using System.Web.Compilation;
	using Sitecore.Mvc.Pipelines.Response.GetModel;
	using Sitecore.Mvc.Presentation;
	using Extensions;
	using System;
	using System.Linq;
	using RenderingParameters;

	public class GetFromView : GetModelProcessor
	{
		public override void Process(GetModelArgs args)
		{
			if (args.Result == null)
			{
				args.Result = this.GetFromViewPath(args.Rendering, args);
			}
		}

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