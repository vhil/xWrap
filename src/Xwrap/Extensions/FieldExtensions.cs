namespace Xwrap.Extensions
{
	using Sitecore.Data.Fields;
	using FieldWrappers.Abstractions;
	using Exceptions;

	/// <summary>
	/// xWrap field extensions methods
	/// </summary>
	public static class FieldExtensions
	{
		private static IFieldWrapperFactory FieldWrapperFactory => Xwrap.FieldWrapperFactory.Instance;

		/// <summary>Wraps Sitecore field and returns an xWrap strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <typeparam name="TField">Target field wrapper type, inherited from <see cref="IFieldWrapper"/></typeparam>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		public static TField AsStronglyTypedField<TField>(this Field field) where TField : IFieldWrapper
		{
			return FieldWrapperFactory.WrapField<TField>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="IRichTextFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="IRichTextFieldWrapper"></returns>
		public static IRichTextFieldWrapper AsRichTextField(this Field field)
		{
			return FieldWrapperFactory.WrapField<IRichTextFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="IIntegerFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="IIntegerFieldWrapper"></returns>
		public static IIntegerFieldWrapper AsIntegerField(this Field field)
		{
			return FieldWrapperFactory.WrapField<IIntegerFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="INumberFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="INumberFieldWrapper"></returns>
		public static INumberFieldWrapper AsNumberField(this Field field)
		{
			return FieldWrapperFactory.WrapField<INumberFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="ICheckboxFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="ICheckboxFieldWrapper"></returns>
		public static ICheckboxFieldWrapper AsCheckboxField(this Field field)
		{
			return FieldWrapperFactory.WrapField<ICheckboxFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="IFileFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="IFileFieldWrapper"></returns>
		public static IFileFieldWrapper AsFileField(this Field field)
		{
			return FieldWrapperFactory.WrapField<IFileFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="IDateTimeFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="IDateTimeFieldWrapper"></returns>
		public static IDateTimeFieldWrapper AsDateTimeField(this Field field)
		{
			return FieldWrapperFactory.WrapField<IDateTimeFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="IGeneralLinkFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="IGeneralLinkFieldWrapper"></returns>
		public static IGeneralLinkFieldWrapper AsGeneralLinkField(this Field field)
		{
			return FieldWrapperFactory.WrapField<IGeneralLinkFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="IImageFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="IImageFieldWrapper"></returns>
		public static IImageFieldWrapper AsImageField(this Field field)
		{
			return FieldWrapperFactory.WrapField<IImageFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="ILinkFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="ILinkFieldWrapper"></returns>
		public static ILinkFieldWrapper AsLinkField(this Field field)
		{
			return FieldWrapperFactory.WrapField<ILinkFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="IListFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="IListFieldWrapper"></returns>
		public static IListFieldWrapper AsListField(this Field field)
		{
			return FieldWrapperFactory.WrapField<IListFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="INameValueListFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="INameValueListFieldWrapper"></returns>
		public static INameValueListFieldWrapper AsNameValueListField(this Field field)
		{
			return FieldWrapperFactory.WrapField<INameValueListFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="INameLookupValueListFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="INameLookupValueListFieldWrapper"></returns>
		public static INameLookupValueListFieldWrapper AsNameLookupValueField(this Field field)
		{
			return FieldWrapperFactory.WrapField<INameLookupValueListFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="ITextFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="ITextFieldWrapper"></returns>
		public static ITextFieldWrapper AsTextField(this Field field)
		{
			return FieldWrapperFactory.WrapField<ITextFieldWrapper>(field);
		}

		/// <summary>Wraps Sitecore field and returns an instance of <see cref="IInternalLinkFieldWrapper"/> strongly typed field wrapper.
		/// Throws exception in case source field does not match the target field type.</summary>
		/// <exception cref="Xwrap.Exceptions.FieldWrappingException">if the source field does not match the target field type</exception>
		/// <returns cref="IInternalLinkFieldWrapper"></returns>
		public static IInternalLinkFieldWrapper AsInternalLinkField(this Field field)
		{
			return FieldWrapperFactory.WrapField<IInternalLinkFieldWrapper>(field);
		}
	}
}