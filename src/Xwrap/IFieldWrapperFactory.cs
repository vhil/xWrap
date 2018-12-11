namespace Xwrap
{
	using Sitecore.Data;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using FieldWrappers.Abstractions;
	using Exceptions;
	using System;

	/// <summary>
	/// Field wrapper factory abstraction for creating instances of <see cref="IFieldWrapper"/> types.
	/// </summary>
	public interface IFieldWrapperFactory
	{
		/// <summary>
		/// Wraps Sitecore field and returns an xWrap strongly typed field wrapper covered in <see cref="IFieldWrapper"/> interface.
		/// Throws exception in case source field does not match the target field type.
		/// </summary>
		/// <param name="field">Field to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		IFieldWrapper WrapField(Field field);

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <typeparam name="TField">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		/// <param name="field">Field to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		TField WrapField<TField>(Field field) where TField : IFieldWrapper;

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper covered in <see cref="IFieldWrapper"/> interface.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		IFieldWrapper WrapField(Item item, string fieldName);

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <typeparam name="TField">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		TField WrapField<TField>(Item item, string fieldName) where TField : IFieldWrapper;

		/// <summary>
		/// Wraps Sitecore field and returns an xWrap strongly typed field wrapper covered in <see cref="IFieldWrapper"/> interface.
		/// Throws exception in case source field does not match the target field type.
		/// </summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		IFieldWrapper WrapField(Item item, ID fieldId);

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <typeparam name="TField">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		TField WrapField<TField>(Item item, ID fieldId) where TField : IFieldWrapper;
	}
}