namespace Xwrap
{
	using System;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Items;
	using Sitecore.Configuration;
	using Pipelines;
	using Sitecore.Data;
	using Sitecore.Pipelines;
	using Exceptions;
	using FieldWrappers.Abstractions;
	using Caching;
	using Extensions;
	using FieldWrappers;

	public class FieldWrapperFactory : IFieldWrapperFactory
	{
		private readonly ICacheService cacheService;

		protected virtual bool CanCache => Sitecore.Context.PageMode.IsNormal;

		public FieldWrapperFactory(ICacheService cacheService)
		{
			this.cacheService = cacheService;
		}

		public static IFieldWrapperFactory Instance => Factory.CreateObject("xWrap/fieldWrapperFactory", true) as IFieldWrapperFactory;

		/// <summary>
		/// Wraps Sitecore field and returns an xWrap strongly typed field wrapper covered in <see cref="IFieldWrapper"/> interface.
		/// Throws exception in case source field does not match the target field type.
		/// </summary>
		/// <param name="field">Field to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		public virtual IFieldWrapper WrapField(Field field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            try
            {
	            var cacheKey = $"{field.ID}_" +
	                           $"{field.Item.ID}_" +
	                           $"{field.Item.Database.Name}_" +
	                           $"{field.Item.Language.Name}_" +
	                           $"{field.Item.Version.Number}";

	            IFieldWrapper fieldWrapper;

				if (this.CanCache && field.Database.Name != "master")
				{
					fieldWrapper = this.cacheService.Get<IFieldWrapper>(cacheKey);
					if (fieldWrapper != null) return fieldWrapper;
				}

	            var wrapFieldAgrs = new WrapFieldArgs(field);
	            CorePipeline.Run("xWrap.wrapField", wrapFieldAgrs);

				fieldWrapper = wrapFieldAgrs.FieldWrapper ?? new TextFieldWrapper(field);

	            if (this.CanCache && field.Database.Name != "master")
	            {
		            this.cacheService.Set(cacheKey, fieldWrapper, TimeSpan.FromMinutes(5), true);
	            }

	            return fieldWrapper;
            }
            catch (Exception ex)
            {
                throw new FieldWrappingException($"FieldWrapperFactory: field '{field.Name}' of '{field.Type}' could not be wrapped into strongly typed field wrapper.", ex);
            }
        }

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <typeparam name="TField">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		/// <param name="field">Field to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		public virtual TField WrapField<TField>(Field field) where TField : IFieldWrapper
        {
            var stronglyTypedField = this.WrapField(field);
            if (stronglyTypedField.GetType().IsAssignableTo(typeof(TField)))
            {
                return (TField) stronglyTypedField;
            }

            throw new FieldWrappingException($"Field wrapper of type '{stronglyTypedField.GetType().Name}' can't be casted to target field wrapper '{typeof(TField).Name}'. Field '{field.Name}' of '{field.Type}'. Make sure you are calling correct type for give field.");
        }

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper covered in <see cref="IFieldWrapper"/> interface.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		public virtual IFieldWrapper WrapField(Item item, string fieldName)
        {
	        return this.WrapField<IFieldWrapper>(item, fieldName);
        }

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <typeparam name="TField">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldName">Field name to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		public virtual TField WrapField<TField>(Item item, string fieldName) where TField : IFieldWrapper
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentNullException(nameof(fieldName));

            var field = item.Fields[fieldName.ToLower()];

            if (field == null)
            {
                throw new FieldWrappingException($"Field with name '{fieldName}' can't be retrieved from item '{item.Paths.FullPath}' with ID {item.ID}.");
            }

            return this.WrapField<TField>(field);
        }
		/// <summary>
		/// Wraps Sitecore field and returns an xWrap strongly typed field wrapper covered in <see cref="IFieldWrapper"/> interface.
		/// Throws exception in case source field does not match the target field type.
		/// </summary>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		public virtual IFieldWrapper WrapField(Item item, ID fieldId)
	    {
			return this.WrapField<IFieldWrapper>(item, fieldId);
		}

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <typeparam name="TField">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		/// <param name="item">Item to get field from</param>
		/// <param name="fieldId">Field ID to wrap</param>
		/// <exception cref="FieldWrappingException">if the source field does not match the target field type</exception>
		/// <exception cref="ArgumentNullException">if one of input parameters is null</exception>
		public virtual TField WrapField<TField>(Item item, ID fieldId) where TField : IFieldWrapper
	    {
			if (item == null) throw new ArgumentNullException(nameof(item));
		    if (ID.IsNullOrEmpty(fieldId)) throw new ArgumentNullException(nameof(fieldId));

		    var field = item.Fields[fieldId];

		    if (field == null)
		    {
			    throw new FieldWrappingException($"Field with ID '{fieldId}' can't be retrieved from item '{item.Paths.FullPath}' with ID {item.ID}.");
		    }

		    return this.WrapField<TField>(field);
		}
    }
}