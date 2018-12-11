namespace Xwrap.Configuration
{
	using Microsoft.Extensions.DependencyInjection;
	using Sitecore.DependencyInjection;

	/// <summary>
	/// Sitecore DI container services configurator for xWrap dependencies
	/// </summary>
	/// <seealso cref="Sitecore.DependencyInjection.IServicesConfigurator" />
	public class ServicesConfigurator : IServicesConfigurator
	{
		public void Configure(IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton(provider => FieldWrapperFactory.Instance);
			serviceCollection.AddSingleton(provider => ItemWrapperFactory.Instance);
		}
	}
}