namespace Xwrap.Mvc.Configuration
{
	using Microsoft.Extensions.DependencyInjection;
	using Sitecore.DependencyInjection;

	/// <summary>
	/// Sitecore DI container services configurator for xWrap MVC dependencies
	/// </summary>
	/// <seealso cref="Sitecore.DependencyInjection.IServicesConfigurator" />
	public class ServicesConfigurator : IServicesConfigurator
	{
		/// <summary>
		/// Configures the service collection.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Configure(IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton(provider => ViewModelFactory.Instance);
		}
	}
}
