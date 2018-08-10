# XMLDocGen.DummyClass
  

  
## bool Foo(string _a, Int32 _b, float _c)
  
Does foo 
  
### Remarks
  
Remarkable indeed 
  
Type | Parameter | Description
:---:|:---:|:---
string | _a | I dont know what this does
Int32 | _b | neither this
float | _c | or this

  
## Fields
  
Type | Name | Description
:---|:---|:---
string | bar | **Summary:** What's this   **Remarks:** Sure, why not?   
string | fubar | **Summary:** WHAT EVER   **Remarks:** SuRR, wHy N00t?   
string | dafuq | **Summary:** I'm lost already   **Remarks:** LorEm ipSuM DolOR sIT aMeT   

  
# XMLDocGen.Extractor
  

  
## string GetMethodName()
  

  
### Remarks
  

  
## string GetFieldName()
  

  
### Remarks
  

  
## string RemoveNamePrefix()
  

  
### Remarks
  

  
## string CleanString()
  

  
### Remarks
  

  
Type | Name | Description
:---|:---|:---

  
# XMLDocGen.Alignment
  

  
## Fields
  
Type | Name | Description
:---|:---|:---
Int32 | value__ | 
Alignment | Left | 
Alignment | Right | 
Alignment | Center | 

  
# XMLDocGen.MarkdownHelper
  

  
## string get_Value()
  

  
### Remarks
  

  
## Void set_Value()
  

  
### Remarks
  

  
## Void AddText(string _text)
  
Adds a line of text and a new line at the end 
  
### Remarks
  

  
Type | Parameter | Description
:---:|:---:|:---
string | _text | 

  
## Void EmptyLine()
  
Adds an empty line to the text 
  
### Remarks
  

  
## Void H1(string _title)
  
Create a level 1 header (i.e. #_title) 
  
### Remarks
  

  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
## Void H2(string _title)
  
Create a level 2 header (i.e. ##_title) 
  
### Remarks
  

  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
## Void H3(string _title)
  
Create a level 3 header (i.e. ###_title) 
  
### Remarks
  

  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
## Void H4(string _title)
  
Create a level 4 header (i.e. ####_title) 
  
### Remarks
  

  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
## Void AddHeader(Int32 _i, string _title)
  
Create a header of level _i 
  
### Remarks
  

  
Type | Parameter | Description
:---:|:---:|:---
Int32 | _i | Level of the header (i.e. how many "#" before the title)
string | _title | Title of the header

  
## Void CreateTable()
  

  
### Remarks
  

  
## MarkdownHelper op_Addition()
  

  
### Remarks
  

  
## Fields
  
Type | Name | Description
:---|:---|:---
string | <Value>k__BackingField | 

  
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
  

  
## Void Main()
  

  
### Remarks
  

  
## Void Generate()
  

  
### Remarks
  

  
## Void ReadAssembly(Assembly _assembly, XmlNodeList _xml)
  
Reads the assembly and creates class/method/field data used to generate the markdown 
  
### Remarks
  

  
Type | Parameter | Description
:---:|:---:|:---
Assembly | _assembly | 
XmlNodeList | _xml | 

  
## Void PrintEverything()
  

  
### Remarks
  

  
## Void ToMarkdown()
  
Converts the class list into a markdown page and outputs it into a file 
  
### Remarks
  

  
## Fields
  
Type | Name | Description
:---|:---|:---
List<ClassData> | classes | 
string | inPath | 
string | assemblyPath | 
string | outFolder | 

  
# XMLDocGen.Utils
  

  
## List<T> ListFieldToList()
  

  
### Remarks
  

  
## String[] ToArray()
  

  
### Remarks
  

  
## String[] RegexReplaceOnArray()
  

  
### Remarks
  

  
## String[] RegexMatchOnArray()
  

  
### Remarks
  

  
## string CleanString()
  

  
### Remarks
  

  
## XmlNode FindMethodMemberWithName()
  

  
### Remarks
  

  
## XmlNode FindMemberWithName()
  

  
### Remarks
  

  
## XmlNode FindFieldMemberWithName()
  

  
### Remarks
  

  
## string GetTypeNameMarkdownText()
  

  
### Remarks
  

  
## string ToGenericTypeString()
  

  
### Remarks
  

  
## Fields
  
Type | Name | Description
:---|:---|:---
Dictionary<Type,String> | customTypeNames | 

  
# XMLDocGen.Utils+<>c
  

  
## string <ToGenericTypeString>b__10_0()
  

  
### Remarks
  

  
## Fields
  
Type | Name | Description
:---|:---|:---
<>c | <>9 | 
Func<Type,String> | <>9__10_0 | 

  