# xWrap

Strongly-typed wrapping framework for Sitecore CMS.

xWrap is a small and light-weigh library created to improve the developer workflow when building Sitecore solutions. 

Functionality and features
 - provides functionality for strongly-typed wrapping fields 
 - provides functionality for strongly-typed wrapping items
 - provides functionality for strongly-typed wrapping rendering parameters  
 - provides functionality for MVC support based on strongly-typed view models 
 - built on a `wrapper` pattern
 - super light-weight
 - uses only standard Sitecore API, no inventions
 - native support for Sitecore Experience Editor
 - compliant with helix principles and modular architecture
 - provides convenient field and item extensions
 - fully configurable through sitecore include config files

The framework is a set of NuGet packages that can be used in your solution:
 - ```xWrap.Framework```
 - ```xWrap```
 - ```xWrap.Mvc.Framework```
 - ```xWrap.Mvc```
 
Within helix modular architecture:
- Install ```xWrap.Mvc``` nuget package to your project layer (will include config files)
- Install ```xWrap.Mvc.Framework``` nuget package to your feature or foundation layer module
 
# Getting started
 
This section covers the basic framework functionality.

1. Wrapping fields
2. Wrapping items
3. View rendering with strongly-typed fields
4. View rendering with strongly-typed datasource
5. View rendering with strongly-typed datasource and rendering parameters
6. Controller rendering with strongly-typed fields
7. Controller rendering with strongly-typed datasource
8. Controller rendering with strongly-typed datasource and rendering parameters

## 1. Wrapping fields

Item fields can be wrapped into strongly-typed intepretation by using convenient extension methods:
```cs
	Item item = Sitecore.Context.Item;
	ILinkFieldWrapper linkField = item.LinkField("link field name");
	Guid linkedItemId = linkField.Value;
	Item linkedItem = linkField.GetTarget();
```