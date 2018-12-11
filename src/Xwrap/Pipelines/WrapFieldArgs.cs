namespace Xwrap.Pipelines
{
	using Sitecore.Data.Fields;
	using Sitecore.Pipelines;
	using FieldWrappers.Abstractions;

	/// <summary>
	/// xWrap wrap field pipeline args type
	/// </summary>
	/// <seealso cref="Sitecore.Pipelines.PipelineArgs" />
	public class WrapFieldArgs : PipelineArgs
	{
		/// <summary>
		/// Gets the original field.
		/// </summary>
		public Field Field { get; }

		/// <summary>
		/// Gets the Sitecore type of the field.
		/// </summary>
		public string FieldType => this.Field.Type.Trim().ToLower();

		/// <summary>
		/// Gets or sets the field wrapper.
		/// </summary>
		public IFieldWrapper FieldWrapper { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapFieldArgs"/> class.
		/// </summary>
		/// <param name="field">The field.</param>
		public WrapFieldArgs(Field field)
		{
			this.Field = field;
		}
	}
}
