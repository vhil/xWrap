namespace Xwrap
{
	using Sitecore.Data;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using FieldWrappers.Abstractions;

	public interface IFieldWrapperFactory
	{
		IFieldWrapper WrapField(Field field);
		TField WrapField<TField>(Field field) where TField : IFieldWrapper;
		IFieldWrapper WrapField(Item item, string fieldName);
		TField WrapField<TField>(Item item, string fieldName) where TField : IFieldWrapper;
		IFieldWrapper WrapField(Item item, ID fieldId);
		TField WrapField<TField>(Item item, ID fieldId) where TField : IFieldWrapper;
	}
}