namespace Xwrap.CodeGeneration
{
	using System.Collections.Generic;
	using Sitecore.Data.Serialization.Yaml.Formatting;

	public class StaticFieldFormattersFactory : FieldFormattersFactory
	{
		private readonly IEnumerable<BaseFieldFormatter> formatters;

		public StaticFieldFormattersFactory(IEnumerable<BaseFieldFormatter> formatters)
			: base(new MockedConfigurationFactory())
		{
			this.formatters = formatters;
		}

		public override IEnumerable<BaseFieldFormatter> Create()
		{
			return this.formatters;
		}
	}
}
