namespace Xwrap.Mvc.RenderingParameters
{
    using System;
    using Extensions;
	using FieldWrappers.Abstractions;
    using Sitecore.Mvc.Presentation;

	public class RenderingParametersWrapper : IRenderingParametersWrapper
    {
        private readonly RenderingParameters parameters;

        public RenderingParametersWrapper(RenderingParameters parameters)
        {
	        this.parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

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
            if (typeof(TField).IsAssignableTo(typeof(ITextFieldWrapper))) return new TextFieldWrapper(fieldName, rawValue) as TField;

            return new RenderingParametersFieldWrapper(fieldName, rawValue) as TField;
        }

        public ICheckboxFieldWrapper CheckboxField(string fieldName)
        {
            return this.WrapParameter<CheckboxFieldWrapper>(fieldName);
        }

        public IIntegerFieldWrapper IntegerField(string fieldName)
        {
            return this.WrapParameter<IntegerFieldWrapper>(fieldName);
        }

        public ILinkFieldWrapper LinkField(string fieldName)
        {
            return this.WrapParameter<LinkFieldWrapper>(fieldName);
        }

	    public IListFieldWrapper ListField(string fieldName)
	    {
			return this.WrapParameter<ListFieldWrapper>(fieldName);
		}

		public INumberFieldWrapper NumberField(string fieldName)
        {
            return this.WrapParameter<NumberFieldWrapper>(fieldName);
        }

        public ITextFieldWrapper TextField(string fieldName)
        {
            return this.WrapParameter<TextFieldWrapper>(fieldName);
        }
    }
}