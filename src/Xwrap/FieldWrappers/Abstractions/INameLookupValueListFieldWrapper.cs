namespace Xwrap.FieldWrappers.Abstractions
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Field wrapper abstraction for 'name look value list' Sitecore field types. Implements <see cref="IFieldWrapper{IDictionary{string, Guid}}"/>
	/// </summary>
	public interface INameLookupValueListFieldWrapper : IFieldWrapper<IDictionary<string, Guid>>
	{
	}
}