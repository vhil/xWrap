namespace Xwrap.Mvc.RenderingParameters
{
	using System;
	using System.Web;
	using FieldWrappers.Abstractions;

	/// <summary>
	/// Base xWrap rendering parameters field wrapper type
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IRenderingParametersFieldWrapper" />
	public class RenderingParametersFieldWrapper : IRenderingParametersFieldWrapper
	{
		/// <summary>
		/// Gets the instance of item wrapper factory.
		/// </summary>
		protected IItemWrapperFactory Factory => ItemWrapperFactory.Instance;

		/// <summary>
		/// Gets the original wrapped object.
		/// </summary>
		public object Original => this.RawValue;

		/// <summary>
		/// Gets the field name.
		/// </summary>
		public string Name { get; protected set; }

		/// <summary>
		/// Gets the raw field value.
		/// </summary>
		public string RawValue { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="RenderingParametersFieldWrapper"/> class.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="value">The value.</param>
		public RenderingParametersFieldWrapper(string fieldName, string value)
		{
			this.Name = fieldName;
			this.RawValue = value;
		}

		/// <summary>
		/// Gets a value indicating whether this field has a valid value.
		/// </summary>
		public virtual bool HasValue => !string.IsNullOrWhiteSpace(this.RawValue);

		/// <summary>
		/// Rendering parameter field wrappers do not support field rendering.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="editing">if set to <c>true</c> [editing].</param>
		/// <returns></returns>
		public virtual IHtmlString Render(string parameters = null, bool editing = false)
		{
			return new HtmlString(this.RawValue);
		}

		/// <summary>
		/// Rendering parameter field wrappers do not support field rendering.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="editing">if set to <c>true</c> [editing].</param>
		/// <returns></returns>
		public virtual IHtmlString Render(object parameters, bool editing = true)
		{
			return new HtmlString(this.RawValue);
		}

		/// <summary>
		/// Rendering parameter field wrappers do not support field rendering.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="editing">if set to <c>true</c> [editing].</param>
		/// <returns></returns>
		public virtual IHtmlString RenderBeginField(object parameters, bool editing = true)
		{
			return new HtmlString(this.RawValue);
		}

		/// <summary>
		/// Rendering parameter field wrappers do not support field rendering.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="editing">if set to <c>true</c> [editing].</param>
		/// <returns></returns>
		public virtual IHtmlString RenderBeginField(string parameters, bool editing = true)
		{
			return new HtmlString(this.RawValue);
		}

		/// <summary>
		/// Rendering parameter field wrappers do not support field rendering.
		/// </summary>
		/// <returns></returns>
		public virtual IHtmlString RenderEndField()
		{
			return new HtmlString(this.RawValue);
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return this.RawValue;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="RenderingParametersFieldWrapper"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator string(RenderingParametersFieldWrapper field)
		{
			return field.RawValue;
		}

		/// <summary>
		/// To the HTML string.
		/// </summary>
		/// <returns></returns>
		public string ToHtmlString()
		{
			return this.Render().ToString();
		}
	}

	/// <summary>
	/// Base strongly typed rendering parameters field wrapper
	/// </summary>
	/// <typeparam name="TReturnType">The type of the return type.</typeparam>
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IRenderingParametersFieldWrapper" />
	public abstract class RenderingParametersFieldWrapper<TReturnType> : RenderingParametersFieldWrapper, IRenderingParametersFieldWrapper<TReturnType>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RenderingParametersFieldWrapper{TReturnType}"/> class.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="value">The value.</param>
		protected RenderingParametersFieldWrapper(string fieldName, string value)
			: base(fieldName, value)
		{
		}

		/// <summary>
		/// Gets the strongly typed value.
		/// </summary>
		public abstract TReturnType Value { get; }

		/// <summary>
		/// Rendering parameter field wrappers do not support field rendering.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="editing">if set to <c>true</c> [editing].</param>
		/// <returns></returns>
		public override IHtmlString Render(string parameters = null, bool editing = false)
		{
			return new HtmlString(this.Value.ToString());
		}

		/// <summary>
		/// Rendering parameter field wrappers do not support field rendering.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="editing">if set to <c>true</c> [editing].</param>
		/// <returns></returns>
		public override IHtmlString Render(object parameters, bool editing = true)
		{
			return new HtmlString(this.Value.ToString());
		}

		/// <summary>
		/// Rendering parameter field wrappers do not support field rendering.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="editing">if set to <c>true</c> [editing].</param>
		/// <returns></returns>
		public override IHtmlString RenderBeginField(object parameters, bool editing = true)
		{
			return new HtmlString(this.Value.ToString());
		}

		/// <summary>
		/// Rendering parameter field wrappers do not support field rendering.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="editing">if set to <c>true</c> [editing].</param>
		/// <returns></returns>
		public override IHtmlString RenderBeginField(string parameters, bool editing = true)
		{
			return new HtmlString(this.Value.ToString());
		}

		/// <summary>
		/// Rendering parameter field wrappers do not support field rendering.
		/// </summary>
		/// <returns></returns>
		public override IHtmlString RenderEndField()
		{
			return new HtmlString(this.Value.ToString());
		}
	}
}