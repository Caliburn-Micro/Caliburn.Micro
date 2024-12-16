<a name='assembly'></a>
# Caliburn.Micro.Platform.Core

## Contents

- [AssemblySource](#T-Caliburn-Micro-AssemblySource 'Caliburn.Micro.AssemblySource')
  - [FindTypeByNames](#F-Caliburn-Micro-AssemblySource-FindTypeByNames 'Caliburn.Micro.AssemblySource.FindTypeByNames')
  - [Instance](#F-Caliburn-Micro-AssemblySource-Instance 'Caliburn.Micro.AssemblySource.Instance')
  - [AddRange(assemblies)](#M-Caliburn-Micro-AssemblySource-AddRange-System-Collections-Generic-IEnumerable{System-Reflection-Assembly}- 'Caliburn.Micro.AssemblySource.AddRange(System.Collections.Generic.IEnumerable{System.Reflection.Assembly})')
- [AssemblySourceCache](#T-Caliburn-Micro-AssemblySourceCache 'Caliburn.Micro.AssemblySourceCache')
  - [ExtractTypes](#F-Caliburn-Micro-AssemblySourceCache-ExtractTypes 'Caliburn.Micro.AssemblySourceCache.ExtractTypes')
  - [Install](#F-Caliburn-Micro-AssemblySourceCache-Install 'Caliburn.Micro.AssemblySourceCache.Install')
- [ExtensionMethods](#T-Caliburn-Micro-ExtensionMethods 'Caliburn.Micro.ExtensionMethods')
  - [GetAssemblyName(assembly)](#M-Caliburn-Micro-ExtensionMethods-GetAssemblyName-System-Reflection-Assembly- 'Caliburn.Micro.ExtensionMethods.GetAssemblyName(System.Reflection.Assembly)')
  - [GetValueOrDefault\`\`2(dictionary,key)](#M-Caliburn-Micro-ExtensionMethods-GetValueOrDefault``2-System-Collections-Generic-IDictionary{``0,``1},``0- 'Caliburn.Micro.ExtensionMethods.GetValueOrDefault``2(System.Collections.Generic.IDictionary{``0,``1},``0)')
- [NameTransformer](#T-Caliburn-Micro-NameTransformer 'Caliburn.Micro.NameTransformer')
  - [UseEagerRuleSelection](#P-Caliburn-Micro-NameTransformer-UseEagerRuleSelection 'Caliburn.Micro.NameTransformer.UseEagerRuleSelection')
  - [AddRule(replacePattern,replaceValue,globalFilterPattern)](#M-Caliburn-Micro-NameTransformer-AddRule-System-String,System-String,System-String- 'Caliburn.Micro.NameTransformer.AddRule(System.String,System.String,System.String)')
  - [AddRule(replacePattern,replaceValueList,globalFilterPattern)](#M-Caliburn-Micro-NameTransformer-AddRule-System-String,System-Collections-Generic-IEnumerable{System-String},System-String- 'Caliburn.Micro.NameTransformer.AddRule(System.String,System.Collections.Generic.IEnumerable{System.String},System.String)')
  - [Transform(source)](#M-Caliburn-Micro-NameTransformer-Transform-System-String- 'Caliburn.Micro.NameTransformer.Transform(System.String)')
  - [Transform(source,getReplaceString)](#M-Caliburn-Micro-NameTransformer-Transform-System-String,System-Func{System-String,System-String}- 'Caliburn.Micro.NameTransformer.Transform(System.String,System.Func{System.String,System.String})')
- [RegExHelper](#T-Caliburn-Micro-RegExHelper 'Caliburn.Micro.RegExHelper')
  - [NameRegEx](#F-Caliburn-Micro-RegExHelper-NameRegEx 'Caliburn.Micro.RegExHelper.NameRegEx')
  - [NamespaceRegEx](#F-Caliburn-Micro-RegExHelper-NamespaceRegEx 'Caliburn.Micro.RegExHelper.NamespaceRegEx')
  - [SubNamespaceRegEx](#F-Caliburn-Micro-RegExHelper-SubNamespaceRegEx 'Caliburn.Micro.RegExHelper.SubNamespaceRegEx')
  - [GetCaptureGroup(groupName,regEx)](#M-Caliburn-Micro-RegExHelper-GetCaptureGroup-System-String,System-String- 'Caliburn.Micro.RegExHelper.GetCaptureGroup(System.String,System.String)')
  - [GetNameCaptureGroup(groupName)](#M-Caliburn-Micro-RegExHelper-GetNameCaptureGroup-System-String- 'Caliburn.Micro.RegExHelper.GetNameCaptureGroup(System.String)')
  - [GetNamespaceCaptureGroup(groupName)](#M-Caliburn-Micro-RegExHelper-GetNamespaceCaptureGroup-System-String- 'Caliburn.Micro.RegExHelper.GetNamespaceCaptureGroup(System.String)')
  - [NamespaceToRegEx(srcNamespace)](#M-Caliburn-Micro-RegExHelper-NamespaceToRegEx-System-String- 'Caliburn.Micro.RegExHelper.NamespaceToRegEx(System.String)')
- [Rule](#T-Caliburn-Micro-NameTransformer-Rule 'Caliburn.Micro.NameTransformer.Rule')
  - [GlobalFilterPattern](#F-Caliburn-Micro-NameTransformer-Rule-GlobalFilterPattern 'Caliburn.Micro.NameTransformer.Rule.GlobalFilterPattern')
  - [ReplacePattern](#F-Caliburn-Micro-NameTransformer-Rule-ReplacePattern 'Caliburn.Micro.NameTransformer.Rule.ReplacePattern')
  - [ReplacementValues](#F-Caliburn-Micro-NameTransformer-Rule-ReplacementValues 'Caliburn.Micro.NameTransformer.Rule.ReplacementValues')
  - [GlobalFilterPatternRegex](#P-Caliburn-Micro-NameTransformer-Rule-GlobalFilterPatternRegex 'Caliburn.Micro.NameTransformer.Rule.GlobalFilterPatternRegex')
  - [ReplacePatternRegex](#P-Caliburn-Micro-NameTransformer-Rule-ReplacePatternRegex 'Caliburn.Micro.NameTransformer.Rule.ReplacePatternRegex')
- [StringSplitter](#T-Caliburn-Micro-StringSplitter 'Caliburn.Micro.StringSplitter')
  - [Split(message,separator)](#M-Caliburn-Micro-StringSplitter-Split-System-String,System-Char- 'Caliburn.Micro.StringSplitter.Split(System.String,System.Char)')
  - [SplitParameters(parameters)](#M-Caliburn-Micro-StringSplitter-SplitParameters-System-String- 'Caliburn.Micro.StringSplitter.SplitParameters(System.String)')
- [TypeMappingConfiguration](#T-Caliburn-Micro-TypeMappingConfiguration 'Caliburn.Micro.TypeMappingConfiguration')
  - [DefaultSubNamespaceForViewModels](#P-Caliburn-Micro-TypeMappingConfiguration-DefaultSubNamespaceForViewModels 'Caliburn.Micro.TypeMappingConfiguration.DefaultSubNamespaceForViewModels')
  - [DefaultSubNamespaceForViews](#P-Caliburn-Micro-TypeMappingConfiguration-DefaultSubNamespaceForViews 'Caliburn.Micro.TypeMappingConfiguration.DefaultSubNamespaceForViews')
  - [IncludeViewSuffixInViewModelNames](#P-Caliburn-Micro-TypeMappingConfiguration-IncludeViewSuffixInViewModelNames 'Caliburn.Micro.TypeMappingConfiguration.IncludeViewSuffixInViewModelNames')
  - [NameFormat](#P-Caliburn-Micro-TypeMappingConfiguration-NameFormat 'Caliburn.Micro.TypeMappingConfiguration.NameFormat')
  - [UseNameSuffixesInMappings](#P-Caliburn-Micro-TypeMappingConfiguration-UseNameSuffixesInMappings 'Caliburn.Micro.TypeMappingConfiguration.UseNameSuffixesInMappings')
  - [ViewModelSuffix](#P-Caliburn-Micro-TypeMappingConfiguration-ViewModelSuffix 'Caliburn.Micro.TypeMappingConfiguration.ViewModelSuffix')
  - [ViewSuffixList](#P-Caliburn-Micro-TypeMappingConfiguration-ViewSuffixList 'Caliburn.Micro.TypeMappingConfiguration.ViewSuffixList')

<a name='T-Caliburn-Micro-AssemblySource'></a>
## AssemblySource `type`

##### Namespace

Caliburn.Micro

##### Summary

A source of assemblies that are inspectable by the framework.

<a name='F-Caliburn-Micro-AssemblySource-FindTypeByNames'></a>
### FindTypeByNames `constants`

##### Summary

Finds a type which matches one of the elements in the sequence of names.

<a name='F-Caliburn-Micro-AssemblySource-Instance'></a>
### Instance `constants`

##### Summary

The singleton instance of the AssemblySource used by the framework.

<a name='M-Caliburn-Micro-AssemblySource-AddRange-System-Collections-Generic-IEnumerable{System-Reflection-Assembly}-'></a>
### AddRange(assemblies) `method`

##### Summary

Adds a collection of assemblies to AssemblySource

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| assemblies | [System.Collections.Generic.IEnumerable{System.Reflection.Assembly}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Reflection.Assembly}') | The assemblies to add |

<a name='T-Caliburn-Micro-AssemblySourceCache'></a>
## AssemblySourceCache `type`

##### Namespace

Caliburn.Micro

##### Summary

A caching subsystem for [AssemblySource](#T-Caliburn-Micro-AssemblySource 'Caliburn.Micro.AssemblySource').

<a name='F-Caliburn-Micro-AssemblySourceCache-ExtractTypes'></a>
### ExtractTypes `constants`

##### Summary

Extracts the types from the spezified assembly for storing in the cache.

<a name='F-Caliburn-Micro-AssemblySourceCache-Install'></a>
### Install `constants`

##### Summary

Installs the caching subsystem.

<a name='T-Caliburn-Micro-ExtensionMethods'></a>
## ExtensionMethods `type`

##### Namespace

Caliburn.Micro

##### Summary

Generic extension methods used by the framework.

<a name='M-Caliburn-Micro-ExtensionMethods-GetAssemblyName-System-Reflection-Assembly-'></a>
### GetAssemblyName(assembly) `method`

##### Summary

Get's the name of the assembly.

##### Returns

The assembly's name.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| assembly | [System.Reflection.Assembly](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.Assembly 'System.Reflection.Assembly') | The assembly. |

<a name='M-Caliburn-Micro-ExtensionMethods-GetValueOrDefault``2-System-Collections-Generic-IDictionary{``0,``1},``0-'></a>
### GetValueOrDefault\`\`2(dictionary,key) `method`

##### Summary

Gets the value for a key. If the key does not exist, return default(TValue);

##### Returns

The key value. default(TValue) if this key is not in the dictionary.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dictionary | [System.Collections.Generic.IDictionary{\`\`0,\`\`1}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{``0,``1}') | The dictionary to call this method on. |
| key | [\`\`0](#T-``0 '``0') | The key to look up. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TKey | The type of the keys in the dictionary. |
| TValue | The type of the values in the dictionary. |

<a name='T-Caliburn-Micro-NameTransformer'></a>
## NameTransformer `type`

##### Namespace

Caliburn.Micro

##### Summary

Class for managing the list of rules for doing name transformation.

<a name='P-Caliburn-Micro-NameTransformer-UseEagerRuleSelection'></a>
### UseEagerRuleSelection `property`

##### Summary

Flag to indicate if transformations from all matched rules are returned. Otherwise, transformations from only the first matched rule are returned.

<a name='M-Caliburn-Micro-NameTransformer-AddRule-System-String,System-String,System-String-'></a>
### AddRule(replacePattern,replaceValue,globalFilterPattern) `method`

##### Summary

Adds a transform using a single replacement value and a global filter pattern.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| replacePattern | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Regular expression pattern for replacing text |
| replaceValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The replacement value. |
| globalFilterPattern | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Regular expression pattern for global filtering |

<a name='M-Caliburn-Micro-NameTransformer-AddRule-System-String,System-Collections-Generic-IEnumerable{System-String},System-String-'></a>
### AddRule(replacePattern,replaceValueList,globalFilterPattern) `method`

##### Summary

Adds a transform using a list of replacement values and a global filter pattern.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| replacePattern | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Regular expression pattern for replacing text |
| replaceValueList | [System.Collections.Generic.IEnumerable{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.String}') | The list of replacement values |
| globalFilterPattern | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Regular expression pattern for global filtering |

<a name='M-Caliburn-Micro-NameTransformer-Transform-System-String-'></a>
### Transform(source) `method`

##### Summary

Gets the list of transformations for a given name.

##### Returns

The transformed names.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name to transform into the resolved name list |

<a name='M-Caliburn-Micro-NameTransformer-Transform-System-String,System-Func{System-String,System-String}-'></a>
### Transform(source,getReplaceString) `method`

##### Summary

Gets the list of transformations for a given name.

##### Returns

The transformed names.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name to transform into the resolved name list |
| getReplaceString | [System.Func{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.String,System.String}') | A function to do a transform on each item in the ReplaceValueList prior to applying the regular expression transform |

<a name='T-Caliburn-Micro-RegExHelper'></a>
## RegExHelper `type`

##### Namespace

Caliburn.Micro

##### Summary

Helper class for encoding strings to regular expression patterns

<a name='F-Caliburn-Micro-RegExHelper-NameRegEx'></a>
### NameRegEx `constants`

##### Summary

Regular expression pattern for valid name

<a name='F-Caliburn-Micro-RegExHelper-NamespaceRegEx'></a>
### NamespaceRegEx `constants`

##### Summary

Regular expression pattern for namespace or namespace fragment

<a name='F-Caliburn-Micro-RegExHelper-SubNamespaceRegEx'></a>
### SubNamespaceRegEx `constants`

##### Summary

Regular expression pattern for subnamespace (including dot)

<a name='M-Caliburn-Micro-RegExHelper-GetCaptureGroup-System-String,System-String-'></a>
### GetCaptureGroup(groupName,regEx) `method`

##### Summary

Creates a named capture group with the specified regular expression

##### Returns

Regular expression capture group with the specified group name

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| groupName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of capture group to create |
| regEx | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Regular expression pattern to capture |

<a name='M-Caliburn-Micro-RegExHelper-GetNameCaptureGroup-System-String-'></a>
### GetNameCaptureGroup(groupName) `method`

##### Summary

Creates a capture group for a valid name regular expression pattern

##### Returns

Regular expression capture group with the specified group name

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| groupName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of capture group to create |

<a name='M-Caliburn-Micro-RegExHelper-GetNamespaceCaptureGroup-System-String-'></a>
### GetNamespaceCaptureGroup(groupName) `method`

##### Summary

Creates a capture group for a namespace regular expression pattern

##### Returns

Regular expression capture group with the specified group name

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| groupName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of capture group to create |

<a name='M-Caliburn-Micro-RegExHelper-NamespaceToRegEx-System-String-'></a>
### NamespaceToRegEx(srcNamespace) `method`

##### Summary

Converts a namespace (including wildcards) to a regular expression string

##### Returns

Namespace converted to a regular expression

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| srcNamespace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Source namespace to convert to regular expression |

<a name='T-Caliburn-Micro-NameTransformer-Rule'></a>
## Rule `type`

##### Namespace

Caliburn.Micro.NameTransformer

##### Summary

A rule that describes a name transform.

<a name='F-Caliburn-Micro-NameTransformer-Rule-GlobalFilterPattern'></a>
### GlobalFilterPattern `constants`

##### Summary

Regular expression pattern for global filtering

<a name='F-Caliburn-Micro-NameTransformer-Rule-ReplacePattern'></a>
### ReplacePattern `constants`

##### Summary

Regular expression pattern for replacing text

<a name='F-Caliburn-Micro-NameTransformer-Rule-ReplacementValues'></a>
### ReplacementValues `constants`

##### Summary

The list of replacement values

<a name='P-Caliburn-Micro-NameTransformer-Rule-GlobalFilterPatternRegex'></a>
### GlobalFilterPatternRegex `property`

##### Summary

Regular expression for global filtering

<a name='P-Caliburn-Micro-NameTransformer-Rule-ReplacePatternRegex'></a>
### ReplacePatternRegex `property`

##### Summary

Regular expression for replacing text

<a name='T-Caliburn-Micro-StringSplitter'></a>
## StringSplitter `type`

##### Namespace

Caliburn.Micro

##### Summary

Helper class when splitting strings

<a name='M-Caliburn-Micro-StringSplitter-Split-System-String,System-Char-'></a>
### Split(message,separator) `method`

##### Summary

Splits a string with a chosen separator. 
If a substring is contained in [...] it will not be splitted.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message to split |
| separator | [System.Char](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Char 'System.Char') | The separator to use when splitting |

<a name='M-Caliburn-Micro-StringSplitter-SplitParameters-System-String-'></a>
### SplitParameters(parameters) `method`

##### Summary

Splits a string with , as separator. 
Does not split within {},[],()

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parameters | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string to split |

<a name='T-Caliburn-Micro-TypeMappingConfiguration'></a>
## TypeMappingConfiguration `type`

##### Namespace

Caliburn.Micro

##### Summary

Class to specify settings for configuring type mappings by the ViewLocator or ViewModelLocator

<a name='P-Caliburn-Micro-TypeMappingConfiguration-DefaultSubNamespaceForViewModels'></a>
### DefaultSubNamespaceForViewModels `property`

##### Summary

The default subnamespace for ViewModels. Used for creating default subnamespace mappings. Defaults to "ViewModels".

<a name='P-Caliburn-Micro-TypeMappingConfiguration-DefaultSubNamespaceForViews'></a>
### DefaultSubNamespaceForViews `property`

##### Summary

The default subnamespace for Views. Used for creating default subnamespace mappings. Defaults to "Views".

<a name='P-Caliburn-Micro-TypeMappingConfiguration-IncludeViewSuffixInViewModelNames'></a>
### IncludeViewSuffixInViewModelNames `property`

##### Summary

Flag to indicate if ViewModel names should include View suffixes (i.e. CustomerPageViewModel vs. CustomerViewModel)

<a name='P-Caliburn-Micro-TypeMappingConfiguration-NameFormat'></a>
### NameFormat `property`

##### Summary

The format string used to compose the name of a type from base name and name suffix

<a name='P-Caliburn-Micro-TypeMappingConfiguration-UseNameSuffixesInMappings'></a>
### UseNameSuffixesInMappings `property`

##### Summary

Flag to indicate whether or not the name of the Type should be transformed when adding a type mapping. Defaults to true.

<a name='P-Caliburn-Micro-TypeMappingConfiguration-ViewModelSuffix'></a>
### ViewModelSuffix `property`

##### Summary

The name suffix for ViewModels. Applies only when UseNameSuffixesInMappings = true. The default is "ViewModel".

<a name='P-Caliburn-Micro-TypeMappingConfiguration-ViewSuffixList'></a>
### ViewSuffixList `property`

##### Summary

List of View suffixes for which default type mappings should be created. Applies only when UseNameSuffixesInMappings = true.
Default values are "View", "Page"
