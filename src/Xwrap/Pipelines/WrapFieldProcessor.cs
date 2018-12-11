namespace Xwrap.Pipelines
{
	using System.Collections.Generic;
	using System.Linq;
	using Sitecore.Diagnostics;
	using System;
	using Exceptions;
	using FieldWrappers.Abstractions;

	/// <summary>
	/// xWrap wrap field processor
	/// </summary>
	public class WrapFieldProcessor
	{
		private readonly string fieldWrapperType;

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapFieldProcessor"/> class.
		/// </summary>
		/// <param name="fieldWrapperType">Type of the field wrapper.</param>
		public WrapFieldProcessor(string fieldWrapperType)
		{
			Assert.IsNotNullOrEmpty(fieldWrapperType, nameof(fieldWrapperType));

			this.fieldWrapperType = fieldWrapperType;
			this.FieldTypes = new List<string>();
		}

		/// <summary>
		/// Gets the field types.
		/// </summary>
		public List<string> FieldTypes { get; }

		/// <summary>
		/// Processes the wrap field pipeline.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">
		/// Configured field wrapper type '{this.fieldWrapperType}' " +
		/// 					                                $"can not be found. Please check configuration.
		/// or
		/// Configured field wrapper type '{this.fieldWrapperType}' " +
		/// 					                                $"can not be instantiated.
		/// </exception>
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