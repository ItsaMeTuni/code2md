# XMLDocGen.DummyClass
  
## Fields
  
Modifiers | Type | Name | Description
:---|:---|:---|:---
public  | `string` | bar | **Summary:** What's this   **Remarks:** Sure, why not?   
public  | `string` | fubar | **Summary:** WHAT EVER   **Remarks:** SuRR, wHy N00t?   
public  | `string` | dafuq | **Summary:** I'm lost already   **Remarks:** LorEm ipSuM DolOR sIT aMeT   

  
<br/> 
<br/> 

## Properties
  
Type | Name | Description | Acessors
:---|:---|:---|:---
`string` | aProp | **Summary:** It has intersting qualities   **Remarks:** HEYA E HEYEHEYEHYEHYA E    | public get; public set; 
`string` | bProp | **Summary:** It has second tier qualities   **Remarks:** HEYAFASDFEAS    | public get; public set; 

  
<br/> 
<br/> 

## Methods
  
### **public `bool` Foo(`List<String>` _a, `int` _b, `float` _c)**
  
**Summary:** Does foo 
  
**Remarks:** Remarkable indeed 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
ref  | `List<String>` | _a | I dont know what this does
--- | `int` | _b | neither this
out  | `float` | _c | or this

  
<br/> 

<br/> 
<br/> 

# XMLDocGen.Extractor
  
## Methods
  
### **public `string` ExtractMethodNameFromXmlName()**
  
<br/> 

### **public `string` ExtractFieldNameFromXmlName()**
  
<br/> 

### **private `string` RemoveNamePrefix()**
  
<br/> 

### **public `string` CleanString()**
  
<br/> 

<br/> 
<br/> 

# XMLDocGen.Alignment
  
## Fields
  
Modifiers | Type | Name | Description
:---|:---|:---|:---
public  | `int` | value__ | 
public static const  | [`Alignment`](#xmldocgenalignment) | Left | 
public static const  | [`Alignment`](#xmldocgenalignment) | Right | 
public static const  | [`Alignment`](#xmldocgenalignment) | Center | 

  
<br/> 
<br/> 

# XMLDocGen.MarkdownHelper
  
## Properties
  
Type | Name | Description | Acessors
:---|:---|:---|:---
`string` | Value | **Summary:** Current value of the markdown text    | public get; private set; 

  
<br/> 
<br/> 

## Methods
  
### **public `Void` AddText(`string` _text)**
  
**Summary:** Adds a line of text and a new line at the end 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `string` | _text | 

  
<br/> 

### **public `Void` EmptyLine()**
  
**Summary:** Adds an empty line to the text 
  
<br/> 

### **public `Void` H1(`string` _title)**
  
**Summary:** Create a level 1 header (i.e. #_title) 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `string` | _title | Title of the header

  
<br/> 

### **public `Void` H2(`string` _title)**
  
**Summary:** Create a level 2 header (i.e. ##_title) 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `string` | _title | Title of the header

  
<br/> 

### **public `Void` H3(`string` _title)**
  
**Summary:** Create a level 3 header (i.e. ###_title) 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `string` | _title | Title of the header

  
<br/> 

### **public `Void` H4(`string` _title)**
  
**Summary:** Create a level 4 header (i.e. ####_title) 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `string` | _title | Title of the header

  
<br/> 

### **public `Void` Space1()**
  
<br/> 

### **public `Void` Space2()**
  
<br/> 

### **public `Void` Space3()**
  
<br/> 

### **private `Void` AddHeader(`int` _i, `string` _title)**
  
**Summary:** Create a header of level _i 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `int` | _i | Level of the header (i.e. how many "#" before the title)
--- | `string` | _title | Title of the header

  
<br/> 

### **private `Void` AddSpace()**
  
<br/> 

### **public `Void` CreateTable()**
  
<br/> 

### **public `string` CreateLink()**
  
<br/> 

### **public `string` CreateCode()**
  
<br/> 

### **public [`MarkdownHelper`](#xmldocgenmarkdownhelper) op_Addition()**
  
<br/> 

<br/> 
<br/> 

# XMLDocGen.ParameterData
  
## Fields
  
Modifiers | Type | Name | Description
:---|:---|:---|:---
public  | `ParameterInfo` | parameterInfo | 
public  | `string` | desc | 

  
<br/> 
<br/> 

# XMLDocGen.MethodData
  
## Fields
  
Modifiers | Type | Name | Description
:---|:---|:---|:---
public  | `MethodInfo` | methodInfo | 
public  | `List<ParameterData>` | parameters | 
public  | [`CommentData`](#xmldocgencommentdata) | commentData | 

  
<br/> 
<br/> 

# XMLDocGen.ClassData
  
## Fields
  
Modifiers | Type | Name | Description
:---|:---|:---|:---
public  | `TypeInfo` | typeInfo | 
public  | `List<MethodData>` | methods | 
public  | `List<FieldData>` | fields | 
public  | `List<PropertyData>` | properties | 
public  | [`CommentData`](#xmldocgencommentdata) | commentData | 

  
<br/> 
<br/> 

# XMLDocGen.FieldData
  
## Fields
  
Modifiers | Type | Name | Description
:---|:---|:---|:---
public  | `FieldInfo` | fieldInfo | 
public  | [`CommentData`](#xmldocgencommentdata) | commentData | 

  
<br/> 
<br/> 

# XMLDocGen.PropertyData
  
## Fields
  
Modifiers | Type | Name | Description
:---|:---|:---|:---
public  | `PropertyInfo` | propertyInfo | 
public  | [`CommentData`](#xmldocgencommentdata) | commentData | 

  
<br/> 
<br/> 

# XMLDocGen.CommentData
  
## Fields
  
Modifiers | Type | Name | Description
:---|:---|:---|:---
public  | `string` | summary | 
public  | `string` | remarks | 
public  | `string` | returns | 

  
<br/> 
<br/> 

# XMLDocGen.Program
  
## Fields
  
Modifiers | Type | Name | Description
:---|:---|:---|:---
private static  | `string` | xmlPath | 
private static  | `string` | assemblyPath | 
private static  | `string` | outFolder | 
private static  | `List<ClassData>` | classes | 
public static  | `Assembly` | assembly | 
private static  | `XmlNodeList` | xml | 

  
<br/> 
<br/> 

## Methods
  
### **private `Void` Main()**
  
<br/> 

### **private `Void` Generate()**
  
<br/> 

### **private `Void` ReadAssembly()**
  
**Summary:** Reads the assembly and gathers together reflected information about types/members and their respective xml comments 
  
<br/> 

### **private `Void` ToMarkdown()**
  
**Summary:** Converts the class list into a markdown page and outputs it into a file 
  
<br/> 

### **private `string` GetXmlPath()**
  
**Summary:** Gets the path of the xml documentation file in xmlPath (can be a relative or an absolute path) 
  
<br/> 

### **private [`CommentData`](#xmldocgencommentdata) GetCommentData(`XmlNode` _node)**
  
**Summary:** Extracts xml comment information into a CommentData given an xml node 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `XmlNode` | _node | The node to extract the information from

  
<br/> 

### **private `Void` LoadConfigs()**
  
<br/> 

<br/> 
<br/> 

# XMLDocGen.Utils
  
## Fields
  
Modifiers | Type | Name | Description
:---|:---|:---|:---
private static  | `Dictionary<Type,String>` | customTypeNames | 

  
<br/> 
<br/> 

## Methods
  
### **public `List<T>` ListFieldToList()**
  
<br/> 

### **public `string` ToArray()**
  
<br/> 

### **public `string` RegexReplaceOnArray(`string` _arr, `string` _regex, `string` _replace)**
  
**Summary:** Does the same as Regex.Replace but on a whole array. 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `string` | _arr | The array to operate on
--- | `string` | _regex | The regular expression
--- | `string` | _replace | What to put in the place of a regex match

  
<br/> 

### **public `string` RegexMatchOnArray(`string` _arr, `string` _regex)**
  
**Summary:** Gets the Regex Match value from each string on an array 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `string` | _arr | The array to operate on
--- | `string` | _regex | The regular expression

  
<br/> 

### **public `string` CleanString(`string` _in)**
  
**Summary:** Cleans a string (i.e. removes new lines, removes consecutive spacesm removes spaces at the start of the string) 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `string` | _in | The string you want to clean

  
<br/> 

### **public `XmlNode` FindMethodMemberWithName(`XmlNodeList` _nodeList, `string` _methodName)**
  
**Summary:** Searches for a method xml node with name _methodName (ignoring parameters and prefix) 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `XmlNodeList` | _nodeList | The xml node list to search in
--- | `string` | _methodName | The name of the method you're looking for

  
<br/> 

### **public `XmlNode` FindMemberWithName(`XmlNodeList` _nodeList, `string` _memberName)**
  
**Summary:** Searches for an xml node with name _memberName 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `XmlNodeList` | _nodeList | The xml node list to search in
--- | `string` | _memberName | The name of the member you're looking for

  
<br/> 

### **public `XmlNode` FindFieldMemberWithName(`XmlNodeList` _nodeList, `string` _fieldName)**
  
**Summary:** Searches for a field xml node with name _fieldName (ignoring prefix) 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `XmlNodeList` | _nodeList | The xml node list to search in
--- | `string` | _fieldName | The name of the field you're looking for

  
<br/> 

### **public `string` GetTypeNameMarkdownText(`Type` _type)**
  
**Summary:** Gets _type's name (if there's a custom name for this type the custom name will be returned) 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `Type` | _type | The _type you want to get the name from

  
<br/> 

### **private `string` GetReadableGenericTypeName(`Type` _type)**
  
**Summary:** Gets the name of a generic type like List<string> instead of List`1 
  
**Remarks:** If _type is not a generic type then the output will be _type.Name 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `Type` | _type | The generic type name

  
<br/> 

### **public `bool` IsEmpty(`string` _string)**
  
**Summary:** Cehcks if a string is null or empty 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `string` | _string | The string to check

  
<br/> 

### **public `bool` IsCompilerGenerated(`MemberInfo` _member)**
  
**Summary:** Checks if a member was generated by the compiler 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `MemberInfo` | _member | Member to check

  
<br/> 

### **public `bool` IsFromAssembly(`Type` _type, `Assembly` _assembly)**
  
**Summary:** Checks if the type _type is declared in the assembly we're reading 
  
Modifiers | Type | Parameter | Description
:---|:---|:---:|:---
--- | `Type` | _type | Type to check
--- | `Assembly` | _assembly | 

  
<br/> 

### **public `string` GetModifiersString()**
  
<br/> 

### **public `string` GetModifiersString()**
  
<br/> 

### **public `string` GetModfiersString()**
  
<br/> 

<br/> 
<br/> 

