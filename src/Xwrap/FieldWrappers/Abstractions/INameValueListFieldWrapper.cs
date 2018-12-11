namespace Xwrap.FieldWrappers.Abstractions
{
	using System.Collections.Specialized;

	/// <summary>
	/// Field wrapper abstraction for 'name value list' Sitecore field types. Implements <see cref="IFieldWrapper{NameValueCollection}"/>
	/// </summary>
	public interface INameValueListFieldWrapper : IFieldWrapper<NameValueCollection>
	{
	}
}
