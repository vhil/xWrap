namespace Xwrap.Mvc.RenderingParameters
{
    using System;
    using System.Web;
	using FieldWrappers.Abstractions;

	public class RenderingParametersFieldWrapper : IRenderingParametersFieldWrapper
    {
        public object Original => this.RawValue;

        public string Name { get; protected set; }

        public string RawValue { get; }

        public RenderingParametersFieldWrapper(string fieldName, string value)
        {
            this.Name = fieldName;
            this.RawValue = value;
        }

        public virtual bool HasValue => !string.IsNullOrWhiteSpace(this.RawValue);

        public virtual IHtmlString Render(string parameters = null, bool editing = false)
        {
            throw new NotImplementedException();
        }

        public virtual IHtmlString Render(object parameters, bool editing = true)
        {
            throw new NotImplementedException();
        }

        public virtual IHtmlString RenderBeginField(object parameters, bool editing = true)
        {
            throw new NotImplementedException();
        }

        public virtual IHtmlString RenderBeginField(string parameters, bool editing = true)
        {
            throw new NotImplementedException();
        }

        public virtual IHtmlString RenderEndField()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return this.RawValue;
        }

        public static implicit operator string(RenderingParametersFieldWrapper field)
        {
            return field.RawValue;
        }

        public string ToHtmlString()
        {
            return this.Render().ToString();
        }
    }
}
