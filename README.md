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

Item fields can be wrapped into strongly-typed intepretation by using convenient extension methods usign both field names or field IDs:
```cs
var item = Sitecore.Context.Item;
ILinkFieldWrapper linkField = item.LinkField("link field name");
Guid linkedItemId = linkField.Value;
Item linkedItem = linkField.GetTarget();
INumberFieldWrapper numberField = linkedItem.NumberField(new ID("{686E1737-890D-4AA8-9EA2-AB7AC7CB0525}"));
decimal numberFieldValue = numberField.Value;
```

## 2. Wrapping items

There is an option to wrap custom sitecore item templates into item wrappers:

```cs
[TemplateId("{655C4BD7-1D6A-4806-95EA-22B94603CC8F}")]
public class TestItem : ItemWrapper
{
	public TestItem(Item item) : base(item)
	{
	}

	public ITextFieldWrapper Text => this.WrapField<ITextFieldWrapper>("text");
	public ICheckboxFieldWrapper Checkbox => this.WrapField<ICheckboxFieldWrapper>("checkbox");
	public IDateTimeFieldWrapper DateTime => this.WrapField<IDateTimeFieldWrapper>("datetime");
	public ILinkFieldWrapper Link => this.WrapField<ILinkFieldWrapper>("link");
	public IImageFieldWrapper Image => this.WrapField<IImageFieldWrapper>("image");
	public IGeneralLinkFieldWrapper GeneralLink => this.WrapField<IGeneralLinkFieldWrapper>("general link");
	public IFileFieldWrapper File => this.WrapField<IFileFieldWrapper>("file");
	public IRichTextFieldWrapper RichText => this.WrapField<IRichTextFieldWrapper>("rich text");
	public INumberFieldWrapper Number => this.WrapField<INumberFieldWrapper>("number");
	public IIntegerFieldWrapper Integer => this.WrapField<IIntegerFieldWrapper>("integer");
	public INameValueListFieldWrapper NameValueList => this.WrapField<INameValueListFieldWrapper>("Name value list");
	public INameLookupValueListFieldWrapper NameLookupValue => this.WrapField<INameLookupValueListFieldWrapper>("Name lookup value list");
}
```

And then use it like this:
```cs
var testItem = new TestItem(Sitecore.Context.Item);
```

```TemplateId``` attribute is optional but nice to have in order to validate the item which is being passed to be wrapped.

## 3. View rendering with strongly-typed fields

If you building a simple view rendering, and you don't need a controller for it, you can use this option:
1. Define model of the view to be ```Xwrap.Mvc.IViewModel```
2. Use field extensions to render fields
```html
@using Xwrap.Extensions
@model Xwrap.Mvc.IViewModel

<div class="row">
	<div class="col-md-12">
		ImageField: @Model.RenderingItem.ImageField("image")
	</div>
</div>
```

## 4. View rendering with strongly-typed datasource

Building a simple view rendering without a controller and datasource item wrapping into a strongly-typed representation:
1. Create a wrapper for your item template, fx ```TestItem```

```cs
[TemplateId("{655C4BD7-1D6A-4806-95EA-22B94603CC8F}")]
public class TestItem : ItemWrapper
{
	public TestItem(Item item) : base(item)
	{
	}
	
	public IImageFieldWrapper Image => this.WrapField<IImageFieldWrapper>("image");
}
```

2. Define model of the view to be ```Xwrap.Mvc.IViewModel<TestItem>```
3. Use rendering item to render fields

```html
@using Xwrap.Extensions
@model Xwrap.Mvc.IViewModel<TestItem>

<div class="row">
	<div class="col-md-12">
		ImageField: @Model.RenderingItem.Image
	</div>
</div>
```

## 5. View rendering with strongly-typed datasource and rendering parameters


Building a simple view rendering without a controller with datasource item and rendering parameters wrapping into a strongly-typed representation:
1. Create a wrapper for your item template, fx ```TestItem```

```cs
[TemplateId("{655C4BD7-1D6A-4806-95EA-22B94603CC8F}")]
public class TestItem : ItemWrapper
{
	public TestItem(Item item) : base(item)
	{
	}
	
	public IImageFieldWrapper Image => this.WrapField<IImageFieldWrapper>("image");
}
```
2. Create rendering parameters wrapper for your parameters:

```cs
public class TestRenderingParameters : RenderingParametersWrapper
{
	public TestRenderingParameters(RenderingParameters parameters) : base(parameters)
	{
	}

	public ICheckboxFieldWrapper CheckboxParam => this.CheckboxField("checkbox parameter name");
}
```
3. Define model of the view to be ```Xwrap.Mvc.IViewModel<TestItem, TestRenderingParameters>```
4. Use rendering item to render fields and rendering parameters to access strongly-typed params

```html
@using Xwrap.Extensions
@model Xwrap.Mvc.IViewModel<TestItem, TestRenderingParameters>

<div class="row">
	<div class="col-md-12">
		@if (@Model.RenderingParameters.CheckboxParam.Value)
		{
			Image field: @Model.RenderingItem.Image
		}
	</div>
</div>
```

## 6. Controller rendering with strongly-typed fields

## 7. Controller rendering with strongly-typed datasource

## 8. Controller rendering with strongly-typed datasource and rendering parameters

# Documentation

## List of available field wrapper extensions:
```cs
ITextFieldWrapper TextField = item.TextField("text");
ICheckboxFieldWrapper CheckboxField = item.CheckboxField("checkbox");
IDateTimeFieldWrapper DateTimeField = item.DateTimeField("datetime");
ILinkFieldWrapper LinkField = item.LinkField("link");
IImageFieldWrapper ImageField = item.ImageField("image");
IGeneralLinkFieldWrapper GeneralLinkField = item.GeneralLinkField("general link");
IFileFieldWrapper FileField = item.FileField("file");
IRichTextFieldWrapper RichTextField = item.RichTextField("rich text");
INumberFieldWrapper NumberField = item.NumberField("number");
IIntegerFieldWrapper IntegerField = item.IntegerField("integer");
INameValueListFieldWrapper NameValueListField = item.NameValueListField("Name value list");
INameLookupValueListFieldWrapper NameLookupValueField = item.NameLookupValueField("Name lookup value list");
````

## List of available rendering parameter field wrapping options:
```cs
public class TestRenderingParameters : RenderingParametersWrapper
{
	public TestRenderingParameters(RenderingParameters parameters) : base(parameters)
	{
	}

	public ITextFieldWrapper TextParam => this.TextField("text parameter");
	public ILinkFieldWrapper LinkParam => this.LinkField("link parameter");
	public IListFieldWrapper ListParam => this.ListField("list parameter");
	public IIntegerFieldWrapper IntegerParam => this.IntegerField("integer parameter");
	public INumberFieldWrapper NumberParam => this.NumberField("number parameter");
	public ICheckboxFieldWrapper CheckboxParam => this.CheckboxField("number parameter");
}
```