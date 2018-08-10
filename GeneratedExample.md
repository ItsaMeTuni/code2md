# XMLDocGen.DummyClass
  
## Fields
  
Type | Name | Description
:---|:---|:---
string | bar | **Summary:** What's this   **Remarks:** Sure, why not?   
string | fubar | **Summary:** WHAT EVER   **Remarks:** SuRR, wHy N00t?   
string | dafuq | **Summary:** I'm lost already   **Remarks:** LorEm ipSuM DolOR sIT aMeT   

  
## Properties
  
Type | Name | Description | Acessors
:---|:---|:---|:---
string | aProp | **Summary:** It has intersting qualities   **Remarks:** HEYA E HEYEHEYEHYEHYA E    | public get; public set; 
string | bProp | **Summary:** It has second tier qualities   **Remarks:** HEYAFASDFEAS    | public get; public set; 

  
## Methods
  
### bool Foo(string _a, Int32 _b, float _c)
  
**Summary:** Does foo 
  
**Remarks:** Remarkable indeed 
  
Type | Parameter | Description
:---:|:---:|:---
string | _a | I dont know what this does
Int32 | _b | neither this
float | _c | or this

  
# XMLDocGen.Extractor
  
## Methods
  
### string GetMethodName()
  
### string GetFieldName()
  
### string RemoveNamePrefix()
  
### string CleanString()
  
# XMLDocGen.Alignment
  
## Fields
  
Type | Name | Description
:---|:---|:---
Int32 | value__ | 
Alignment | Left | 
Alignment | Right | 
Alignment | Center | 

  
# XMLDocGen.MarkdownHelper
  
## Properties
  
Type | Name | Description | Acessors
:---|:---|:---|:---
string | Value | **Summary:** Current value of the markdown text    | public get; private set; 

  
## Methods
  
### Void AddText(string _text)
  
**Summary:** Adds a line of text and a new line at the end 
  
Type | Parameter | Description
:---:|:---:|:---
string | _text | 

  
### Void EmptyLine()
  
**Summary:** Adds an empty line to the text 
  
### Void H1(string _title)
  
**Summary:** Create a level 1 header (i.e. #_title) 
  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
### Void H2(string _title)
  
**Summary:** Create a level 2 header (i.e. ##_title) 
  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
### Void H3(string _title)
  
**Summary:** Create a level 3 header (i.e. ###_title) 
  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
### Void H4(string _title)
  
**Summary:** Create a level 4 header (i.e. ####_title) 
  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
### Void AddHeader(Int32 _i, string _title)
  
**Summary:** Create a header of level _i 
  
Type | Parameter | Description
:---:|:---:|:---
Int32 | _i | Level of the header (i.e. how many "#" before the title)
string | _title | Title of the header

  
### Void CreateTable()
  
### MarkdownHelper op_Addition()
  
# XMLDocGen.ParameterData
  
## Fields
  
Type | Name | Description
:---|:---|:---
ParameterInfo | parameterInfo | 
string | desc | 

  
# XMLDocGen.MethodData
  
## Fields
  
Type | Name | Description
:---|:---|:---
MethodInfo | methodInfo | 
List<ParameterData> | parameters | 
string | returns | 
string | remarks | 
string | summary | 

  
# XMLDocGen.ClassData
  
## Fields
  
Type | Name | Description
:---|:---|:---
TypeInfo | typeInfo | 
List<MethodData> | methods | 
List<FieldData> | fields | 
List<PropertyData> | properties | 
string | summary | 
string | remarks | 

  
# XMLDocGen.FieldData
  
## Fields
  
Type | Name | Description
:---|:---|:---
FieldInfo | fieldInfo | 
string | summary | 
string | remarks | 

  
# XMLDocGen.PropertyData
  
## Fields
  
Type | Name | Description
:---|:---|:---
PropertyInfo | propertyInfo | 
string | summary | 
string | remarks | 

  
# XMLDocGen.Program
  
## Fields
  
Type | Name | Description
:---|:---|:---
List<ClassData> | classes | 
string | xmlPath | 
string | assemblyPath | 
string | outFolder | 

  
## Methods
  
### Void Main()
  
### Void Generate()
  
### Void ReadAssembly(Assembly _assembly, XmlNodeList _xml)
  
**Summary:** Reads the assembly and creates class/method/field data used to generate the markdown 
  
Type | Parameter | Description
:---:|:---:|:---
Assembly | _assembly | 
XmlNodeList | _xml | 

  
### Void PrintEverything()
  
### Void ToMarkdown()
  
**Summary:** Converts the class list into a markdown page and outputs it into a file 
  
### string GetXmlPath()
  
# XMLDocGen.Utils
  
## Fields
  
Type | Name | Description
:---|:---|:---
Dictionary<Type,String> | customTypeNames | 

  
## Methods
  
### List<T> ListFieldToList()
  
### String[] ToArray()
  
### String[] RegexReplaceOnArray()
  
### String[] RegexMatchOnArray()
  
### string CleanString()
  
### XmlNode FindMethodMemberWithName()
  
### XmlNode FindMemberWithName()
  
### XmlNode FindFieldMemberWithName()
  
### string GetTypeNameMarkdownText()
  
### string ToGenericTypeString()
  
### bool IsEmpty()
  
### bool IsCompilerGenerated()
  
