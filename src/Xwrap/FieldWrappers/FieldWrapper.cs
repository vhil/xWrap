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

    public class FieldWrapper : IFieldWrapper
    {
        private Stack<string> endFieldStack;
        protected virtual Stack<string> EndFieldStack => this.endFieldStack ?? (this.endFieldStack = new Stack<string>());

        public FieldWrapper(Field originalField)
        {
	        this.Original = originalField ?? throw new ArgumentNullException(nameof(originalField));
        }

        public FieldWrapper(BaseItem item, string fieldName) 
            : this(item.Fields[fieldName])
        {
        }

        public Field OriginalField => (Field) this.Original;
        public object Original { get; }
        public string Name => this.OriginalField.Name;

        public string RawValue => this.OriginalField.Value;

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

        public virtual IHtmlString RenderEndField()
        {
            if (this.EndFieldStack.Count == 0)
            {
                throw new InvalidOperationException("There was a call to EndField with no corresponding call to BeginField");
            }

            return new HtmlString(this.EndFieldStack.Pop());
        }

        public virtual IHtmlString Render(string parameters = null, bool editing = true)
        {
            return new HtmlString(this.RenderBeginField(parameters, editing) + this.RenderEndField().ToString());
        }

        public IHtmlString Render(object parameters, bool editing = true)
        {
            return new HtmlString(this.RenderBeginField(parameters, editing) + this.RenderEndField().ToString());
        }
        
        public static implicit operator string(FieldWrapper field)
        {
            return field.RawValue;
        }

        public string ToHtmlString()
        {
            return this.Render().ToString();
        }

        public virtual bool HasValue => !string.IsNullOrWhiteSpace(this.RawValue);
    }
}
