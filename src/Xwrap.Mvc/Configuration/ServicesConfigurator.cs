﻿namespace Xwrap.Mvc.Configuration
{
	using Microsoft.Extensions.DependencyInjection;
	using Sitecore.DependencyInjection;

	public class ServicesConfigurator : IServicesConfigurator
	{
		public void Configure(IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton(provider => ViewModelFactory.Instance);
		}
	}
}
