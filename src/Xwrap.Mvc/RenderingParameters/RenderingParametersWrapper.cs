namespace Xwrap.Mvc.RenderingParameters
{
    using System;
    using Extensions;
	using FieldWrappers.Abstractions;
    using Sitecore.Mvc.Presentation;

	/// <summary>
	/// Base type for rendering parameters wrappers
	/// </summary>
	/// <seealso cref="Xwrap.Mvc.RenderingParameters.IRenderingParametersWrapper" />
	public class RenderingParametersWrapper : IRenderingParametersWrapper
    {
        private readonly RenderingParameters parameters;

		/// <summary>
		/// Initializes a new instance of the <see cref="RenderingParametersWrapper"/> class.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <exception cref="ArgumentNullException">parameters</exception>
		public RenderingParametersWrapper(RenderingParameters parameters)
        {
	        this.parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

		/// <summary>
		/// Wraps the rendering parameters field into xWrap strongly typed rendering parameter field wrapper of <see cref="IRenderingParametersFieldWrapper" />.
		/// </summary>
		/// <typeparam name="TField">The type of the field.</typeparam>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public TField WrapParameter<TField>(string fieldName) where TField : class, IRenderingParametersFieldWrapper
        {
			var rawValue = !this.parameters.Contains(fieldName) 
				? string.Empty 
				: this.parameters[fieldName];

			if (typeof(TField).IsAssignableTo(typeof(ICheckboxFieldWrapper))) return new CheckboxFieldWrapper(fieldName, rawValue) as TField;
            if (typeof(TField).IsAssignableTo(typeof(IIntegerFieldWrapper))) return new IntegerFieldWrapper(fieldName, rawValue) as TField;
            if (typeof(TField).IsAssignableTo(typeof(ILinkFieldWrapper))) return new LinkFieldWrapper(fieldName, rawValue) as TField;
            if (typeof(TField).IsAssignableTo(typeof(IListFieldWrapper))) return new ListFieldWrapper(fieldName, rawValue) as TField;
            if (typeof(TField).IsAssignableTo(typeof(INumberFieldWrapper))) return new NumberFieldWrapper(fieldName, rawValue) as TField;
            if (typeof(TField).IsAssignableTo(typeof(IInternalLinkFieldWrapper))) return new InternalLinkFieldWrapper(fieldName, rawValue) as TField;
            if (typeof(TField).IsAssignableTo(typeof(ITextFieldWrapper))) return new TextFieldWrapper(fieldName, rawValue) as TField;

            return new RenderingParametersFieldWrapper(fieldName, rawValue) as TField;
        }

		/// <summary>
		/// Wraps the field into rendering parameters <see cref="ICheckboxFieldWrapper" /> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public ICheckboxFieldWrapper CheckboxField(string fieldName)
        {
            return this.WrapParameter<CheckboxFieldWrapper>(fieldName);
        }

		/// <summary>
		/// Wraps the field into rendering parameters <see cref="IIntegerFieldWrapper" /> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public IIntegerFieldWrapper IntegerField(string fieldName)
        {
            return this.WrapParameter<IntegerFieldWrapper>(fieldName);
        }

		/// <summary>
		/// Wraps the field into rendering parameters <see cref="ILinkFieldWrapper" /> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public ILinkFieldWrapper LinkField(string fieldName)
        {
            return this.WrapParameter<LinkFieldWrapper>(fieldName);
        }

		/// <summary>
		/// Wraps the field into rendering parameters <see cref="IListFieldWrapper" /> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public IListFieldWrapper ListField(string fieldName)
	    {
			return this.WrapParameter<ListFieldWrapper>(fieldName);
		}

		/// <summary>
		/// Wraps the field into rendering parameters <see cref="INumberFieldWrapper" /> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public INumberFieldWrapper NumberField(string fieldName)
        {
            return this.WrapParameter<NumberFieldWrapper>(fieldName);
        }

		/// <summary>
		/// Internals the link field.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public IInternalLinkFieldWrapper InternalLinkField(string fieldName)
        {
            return this.WrapParameter<InternalLinkFieldWrapper>(fieldName);
        }

		/// <summary>
		/// Wraps the field into rendering parameters <see cref="ITextFieldWrapper" /> type
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public ITextFieldWrapper TextField(string fieldName)
        {
            return this.WrapParameter<TextFieldWrapper>(fieldName);
        }
    }
}