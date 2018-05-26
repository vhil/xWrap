namespace Xwrap.Pipelines
{
	using System.Collections.Generic;
	using System.Linq;
	using Sitecore.Diagnostics;
	using System;
	using Exceptions;
	using FieldWrappers.Abstractions;

	public class WrapFieldProcessor
	{
		private readonly string fieldWrapperType;

		public WrapFieldProcessor(string fieldWrapperType)
		{
			Assert.IsNotNullOrEmpty(fieldWrapperType, nameof(fieldWrapperType));

			this.fieldWrapperType = fieldWrapperType;
			this.FieldTypes = new List<string>();
		}

		public List<string> FieldTypes { get; }

		public void Process(WrapFieldArgs args)
		{
			if (args.FieldWrapper != null) return;

			if (this.FieldTypes.Any(x => x.Trim().ToLower() == args.FieldType))
			{
				var type = Type.GetType(this.fieldWrapperType);

				if (type == null)
				{
					throw new FieldWrappingException($"Configured field wrapper type '{this.fieldWrapperType}' " +
					                                $"can not be found. Please check configuration.");
				}

				var fieldWrapper = Activator.CreateInstance(type, args.Field) as IFieldWrapper;

				if (fieldWrapper == null)
				{
					throw new FieldWrappingException($"Configured field wrapper type '{this.fieldWrapperType}' " +
					                                $"can not be instantiated.");
				}

				args.FieldWrapper = fieldWrapper;
			}
		}
	}
}