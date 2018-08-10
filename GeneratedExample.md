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
  
### **bool Foo(string _a, Int32 _b, float _c)**
  
**Summary:** Does foo 
  
**Remarks:** Remarkable indeed 
  
Type | Parameter | Description
:---:|:---:|:---
string | _a | I dont know what this does
Int32 | _b | neither this
float | _c | or this

  
# XMLDocGen.Extractor
  
## Methods
  
### **string GetMethodName()**
  
### **string GetFieldName()**
  
### **string RemoveNamePrefix()**
  
### **string CleanString()**
  
# XMLDocGen.Alignment
  
## Fields
  
Type | Name | Description
:---|:---|:---
Int32 | value__ | 
[Alignment](#XMLDocGen.Alignment) | Left | 
[Alignment](#XMLDocGen.Alignment) | Right | 
[Alignment](#XMLDocGen.Alignment) | Center | 

  
# XMLDocGen.MarkdownHelper
  
## Properties
  
Type | Name | Description | Acessors
:---|:---|:---|:---
string | Value | **Summary:** Current value of the markdown text    | public get; private set; 

  
## Methods
  
### **Void AddText(string _text)**
  
**Summary:** Adds a line of text and a new line at the end 
  
Type | Parameter | Description
:---:|:---:|:---
string | _text | 

  
### **Void EmptyLine()**
  
**Summary:** Adds an empty line to the text 
  
### **Void H1(string _title)**
  
**Summary:** Create a level 1 header (i.e. #_title) 
  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
### **Void H2(string _title)**
  
**Summary:** Create a level 2 header (i.e. ##_title) 
  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
### **Void H3(string _title)**
  
**Summary:** Create a level 3 header (i.e. ###_title) 
  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
### **Void H4(string _title)**
  
**Summary:** Create a level 4 header (i.e. ####_title) 
  
Type | Parameter | Description
:---:|:---:|:---
string | _title | Title of the header

  
### **Void AddHeader(Int32 _i, string _title)**
  
**Summary:** Create a header of level _i 
  
Type | Parameter | Description
:---:|:---:|:---
Int32 | _i | Level of the header (i.e. how many "#" before the title)
string | _title | Title of the header

  
### **Void CreateTable()**
  
### **string CreateLink()**
  
### **[MarkdownHelper](#XMLDocGen.MarkdownHelper) op_Addition()**
  
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
[CommentData](#XMLDocGen.CommentData) | commentData | 

  
# XMLDocGen.ClassData
  
## Fields
  
Type | Name | Description
:---|:---|:---
TypeInfo | typeInfo | 
List<MethodData> | methods | 
List<FieldData> | fields | 
List<PropertyData> | properties | 
[CommentData](#XMLDocGen.CommentData) | commentData | 

  
# XMLDocGen.FieldData
  
## Fields
  
Type | Name | Description
:---|:---|:---
FieldInfo | fieldInfo | 
[CommentData](#XMLDocGen.CommentData) | commentData | 

  
# XMLDocGen.PropertyData
  
## Fields
  
Type | Name | Description
:---|:---|:---
PropertyInfo | propertyInfo | 
[CommentData](#XMLDocGen.CommentData) | commentData | 

  
# XMLDocGen.CommentData
  
## Fields
  
Type | Name | Description
:---|:---|:---
string | summary | 
string | remarks | 
string | returns | 

  
# XMLDocGen.Program
  
## Fields
  
Type | Name | Description
:---|:---|:---
string | xmlPath | 
string | assemblyPath | 
string | outFolder | 
List<ClassData> | classes | 
Assembly | assembly | 
XmlNodeList | xml | 

  
## Methods
  
### **Void Main()**
  
### **Void Generate()**
  
### **Void ReadAssembly()**
  
**Summary:** Reads the assembly and gathers together reflected information about types/members and their respective xml comments 
  
### **Void ToMarkdown()**
  
**Summary:** Converts the class list into a markdown page and outputs it into a file 
  
### **string GetXmlPath()**
  
**Summary:** Gets the path of the xml documentation file in xmlPath (can be a relative or an absolute path) 
  
### **[CommentData](#XMLDocGen.CommentData) GetCommentData(XmlNode _node)**
  
**Summary:** Extracts xml comment information into a CommentData given an xml node 
  
Type | Parameter | Description
:---:|:---:|:---
XmlNode | _node | The node to extract the information from

  
# XMLDocGen.Utils
  
## Fields
  
Type | Name | Description
:---|:---|:---
Dictionary<Type,String> | customTypeNames | 

  
## Methods
  
### **List<T> ListFieldToList()**
  
### **String[] ToArray()**
  
### **String[] RegexReplaceOnArray(String[] _arr, string _regex, string _replace)**
  
**Summary:** Does the same as Regex.Replace but on a whole array. 
  
Type | Parameter | Description
:---:|:---:|:---
String[] | _arr | The array to operate on
string | _regex | The regular expression
string | _replace | What to put in the place of a regex match

  
### **String[] RegexMatchOnArray(String[] _arr, string _regex)**
  
**Summary:** Gets the Regex Match value from each string on an array 
  
Type | Parameter | Description
:---:|:---:|:---
String[] | _arr | The array to operate on
string | _regex | The regular expression

  
### **string CleanString(string _in)**
  
**Summary:** Cleans a string (i.e. removes new lines, removes consecutive spacesm removes spaces at the start of the string) 
  
Type | Parameter | Description
:---:|:---:|:---
string | _in | The string you want to clean

  
### **XmlNode FindMethodMemberWithName(XmlNodeList _nodeList, string _methodName)**
  
**Summary:** Searches for a method xml node with name _methodName (ignoring parameters and prefix) 
  
Type | Parameter | Description
:---:|:---:|:---
XmlNodeList | _nodeList | The xml node list to search in
string | _methodName | The name of the method you're looking for

  
### **XmlNode FindMemberWithName(XmlNodeList _nodeList, string _memberName)**
  
**Summary:** Searches for an xml node with name _memberName 
  
Type | Parameter | Description
:---:|:---:|:---
XmlNodeList | _nodeList | The xml node list to search in
string | _memberName | The name of the member you're looking for

  
### **XmlNode FindFieldMemberWithName(XmlNodeList _nodeList, string _fieldName)**
  
**Summary:** Searches for a field xml node with name _fieldName (ignoring prefix) 
  
Type | Parameter | Description
:---:|:---:|:---
XmlNodeList | _nodeList | The xml node list to search in
string | _fieldName | The name of the field you're looking for

  
### **string GetTypeNameMarkdownText(Type _type)**
  
**Summary:** Gets _type's name (if there's a custom name for this type the custom name will be returned) 
  
Type | Parameter | Description
:---:|:---:|:---
Type | _type | The _type you want to get the name from

  
### **string GetReadableGenericTypeName(Type _type)**
  
**Summary:** Gets the name of a generic type like List<string> instead of List`1 
  
**Remarks:** If _type is not a generic type then the output will be _type.Name 
  
Type | Parameter | Description
:---:|:---:|:---
Type | _type | The generic type name

  
### **bool IsEmpty(string _string)**
  
**Summary:** Cehcks if a string is null or empty 
  
Type | Parameter | Description
:---:|:---:|:---
string | _string | The string to check

  
### **bool IsCompilerGenerated(MemberInfo _member)**
  
**Summary:** Checks if a member was generated by the compiler 
  
Type | Parameter | Description
:---:|:---:|:---
MemberInfo | _member | Member to check

  
### **bool IsFromAssembly(Type _type, Assembly _assembly)**
  
**Summary:** Checks if the type _type is declared in the assembly we're reading 
  
Type | Parameter | Description
:---:|:---:|:---
Type | _type | Type to check
Assembly | _assembly | 

  
