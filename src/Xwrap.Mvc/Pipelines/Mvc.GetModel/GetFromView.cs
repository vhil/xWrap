namespace Xwrap.Mvc.Pipelines.Mvc.GetModel
{
	using System.Web.Compilation;
	using Sitecore.Mvc.Pipelines.Response.GetModel;
	using Sitecore.Mvc.Presentation;
	using Extensions;

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

				// Check to see if no model has been set
				if (!modelType.IsAssignableTo(typeof(IViewModel)))
				{
					return null;
				}

				return ViewModelFactory.Instance().GetViewModel();
			}
			catch
			{
				return null;
			}
		}
	}
}