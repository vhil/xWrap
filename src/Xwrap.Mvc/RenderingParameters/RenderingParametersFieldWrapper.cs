namespace Xwrap.Mvc.RenderingParameters
{
    using System;
    using System.Web;
	using FieldWrappers.Abstractions;

	public class RenderingParametersFieldWrapper : IRenderingParametersFieldWrapper
	{
	    protected IItemWrapperFactory Factory => ItemWrapperFactory.Instance;

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
	        return new HtmlString(this.RawValue);
        }

		public virtual IHtmlString Render(object parameters, bool editing = true)
        {
            return new HtmlString(this.RawValue);
        }

        public virtual IHtmlString RenderBeginField(object parameters, bool editing = true)
        {
			return new HtmlString(this.RawValue);
		}

		public virtual IHtmlString RenderBeginField(string parameters, bool editing = true)
        {
			return new HtmlString(this.RawValue);
		}

		public virtual IHtmlString RenderEndField()
        {
			return new HtmlString(this.RawValue);
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

	public abstract class RenderingParametersFieldWrapper<TReturnType> : RenderingParametersFieldWrapper, IRenderingParametersFieldWrapper<TReturnType>
	{
		protected RenderingParametersFieldWrapper(string fieldName, string value) 
			: base(fieldName, value)
		{
		}

		public abstract TReturnType Value { get; }

		public override IHtmlString Render(string parameters = null, bool editing = false)
		{
			return new HtmlString(this.Value.ToString());
		}

		public override IHtmlString Render(object parameters, bool editing = true)
		{
			return new HtmlString(this.Value.ToString());
		}

		public override IHtmlString RenderBeginField(object parameters, bool editing = true)
		{
			return new HtmlString(this.Value.ToString());
		}

		public override IHtmlString RenderBeginField(string parameters, bool editing = true)
		{
			return new HtmlString(this.Value.ToString());
		}

		public override IHtmlString RenderEndField()
		{
			return new HtmlString(this.Value.ToString());
		}
	}
}