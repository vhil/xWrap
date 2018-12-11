namespace Xwrap.FieldWrappers
{
	using System;
	using System.Collections.Generic;
	using System.Web;
	using Helpers;
	using Abstractions;
	using Sitecore.Data.Fields;
	using Sitecore.Pipelines;
	using Sitecore.Pipelines.RenderField;
	using Sitecore.Data.Items;

	/// <summary>
	/// Base field wrapper type for Sitecore fields. Implements <see cref="IFieldWrapper"/>
	/// </summary>
	/// <seealso cref="Xwrap.FieldWrappers.Abstractions.IFieldWrapper" />
	public class FieldWrapper : IFieldWrapper
	{
		/// <summary>
		/// Gets the item wrapper factory instance.
		/// </summary>
		protected IItemWrapperFactory Factory => ItemWrapperFactory.Instance;
		private Stack<string> endFieldStack;
		protected virtual Stack<string> EndFieldStack => this.endFieldStack ?? (this.endFieldStack = new Stack<string>());

		/// <summary>
		/// Initializes a new instance of the <see cref="FieldWrapper"/> class.
		/// </summary>
		/// <param name="originalField">The original field.</param>
		/// <exception cref="System.ArgumentNullException">originalField</exception>
		public FieldWrapper(Field originalField)
		{
			this.Original = originalField ?? throw new ArgumentNullException(nameof(originalField));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FieldWrapper"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		public FieldWrapper(BaseItem item, string fieldName)
			: this(item.Fields[fieldName])
		{
		}

		/// <summary>
		/// Gets the original Sitecore field.
		/// </summary>
		public Field OriginalField => (Field)this.Original;

		/// <summary>
		/// Gets the original wrapped object.
		/// </summary>
		public object Original { get; }

		/// <summary>
		/// Gets the field name.
		/// </summary>
		public string Name => this.OriginalField.Name;

		/// <summary>
		/// Gets the raw field value.
		/// </summary>
		/// <value>
		/// The raw value.
		/// </value>
		public string RawValue => this.OriginalField.Value;

		/// <summary>
		/// Renders the field using default Sitecore field renderer. Leaves the option to insert html inside the field until RenderEndField method is being called.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="editing">if set to <c>true</c> [editing].</param>
		/// <returns></returns>
		public virtual IHtmlString RenderBeginField(string parameters = null, bool editing = true)
		{
			var renderFieldArgs = new RenderFieldArgs
			{
				Item = this.OriginalField.Item,
				FieldName = this.OriginalField.Name,
				DisableWebEdit = !editing,
				RawParameters = parameters ?? string.Empty
			};

			if (renderFieldArgs.Item == null)
			{
				return new HtmlString(string.Empty);
			}

			CorePipeline.Run("renderField", renderFieldArgs);
			var result = renderFieldArgs.Result;
			var str = result.FirstPart ?? string.Empty;
			this.EndFieldStack.Push(result.LastPart ?? string.Empty);

			return new HtmlString(str);
		}

		/// <summary>
		/// Renders the field using default Sitecore field renderer. Leaves the option to insert html inside the field until RenderEndField method is being called.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="editing">if set to <c>true</c> [editing].</param>
		/// <returns></returns>
		public virtual IHtmlString RenderBeginField(object parameters, bool editing = true)
		{
			var renderFieldArgs = new RenderFieldArgs
			{
				Item = this.OriginalField.Item,
				FieldName = this.OriginalField.Name,
				DisableWebEdit = !editing
			};

			if (parameters != null)
			{
				TypeHelper.CopyProperties(parameters, renderFieldArgs);
				TypeHelper.CopyProperties(parameters, renderFieldArgs.Parameters);
			}

			if (renderFieldArgs.Item == null)
			{
				return new HtmlString(string.Empty);
			}

			CorePipeline.Run("renderField", renderFieldArgs);
			var result = renderFieldArgs.Result;
			var str = result.FirstPart ?? string.Empty;
			this.EndFieldStack.Push(result.LastPart ?? string.Empty);

			return new HtmlString(str);
		}

		/// <summary>
		/// Renders the end of field. The RenderBeginField method must be called before.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException">There was a call to EndField with no corresponding call to BeginField</exception>
		public virtual IHtmlString RenderEndField()
		{
			if (this.EndFieldStack.Count == 0)
			{
				throw new InvalidOperationException("There was a call to EndField with no corresponding call to BeginField");
			}

			return new HtmlString(this.EndFieldStack.Pop());
		}

		/// <summary>
		/// Renders the field using default Sitecore field renderer.
		/// </summary>
		/// <param name="parameters">The parameters</param>
		/// <param name="editing">Specify if the field should be editable when rendered.</param>
		/// <returns></returns>
		public virtual IHtmlString Render(string parameters = null, bool editing = true)
		{
			return new HtmlString(this.RenderBeginField(parameters, editing) + this.RenderEndField().ToString());
		}

		/// <summary>
		/// Renders the field using default Sitecore field renderer.
		/// </summary>
		/// <param name="parameters">The parameters</param>
		/// <param name="editing">Specify if the field should be editable when rendered.</param>
		/// <returns></returns>
		public IHtmlString Render(object parameters, bool editing = true)
		{
			return new HtmlString(this.RenderBeginField(parameters, editing) + this.RenderEndField().ToString());
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="FieldWrapper"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator string(FieldWrapper field)
		{
			return field.RawValue;
		}

		/// <summary>
		/// Returns an HTML-encoded string.
		/// </summary>
		/// <returns>
		/// An HTML-encoded string.
		/// </returns>
		public string ToHtmlString()
		{
			return this.Render().ToString();
		}

		/// <summary>
		/// Gets a value indicating whether this field has a valid value.
		/// </summary>
		public virtual bool HasValue => !string.IsNullOrWhiteSpace(this.RawValue);
	}
}
