namespace Xwrap.Pipelines
{
	using Sitecore.Data.Fields;
	using Sitecore.Pipelines;
	using FieldWrappers.Abstractions;

	public class WrapFieldArgs : PipelineArgs
	{
		public Field Field { get; }
		public string FieldType => this.Field.Type.Trim().ToLower();
		public IFieldWrapper FieldWrapper { get; set; }

		public WrapFieldArgs(Field field)
		{
			this.Field = field;
		}
	}
}
