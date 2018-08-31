# TemplateReplacer

### Summary


### Remarks


## Fields

#### ` string template`
**Summary:** 
<br/>
**Remarks:** 
#### ` List<TypeData> typeDatas`
**Summary:** 
<br/>
**Remarks:** 
#### ` string classFragment`
**Summary:** 
<br/>
**Remarks:** 
#### ` string fieldsFragment`
**Summary:** 
<br/>
**Remarks:** 
#### ` string propertiesFragment`
**Summary:** 
<br/>
**Remarks:** 
#### ` string methodsFragment`
**Summary:** 
<br/>
**Remarks:** 
#### ` string paramsFragment`
**Summary:** 
<br/>
**Remarks:** 
#### ` string fieldFragment`
**Summary:** 
<br/>
**Remarks:** 
#### ` string methodFragment`
**Summary:** 
<br/>
**Remarks:** 
#### ` string propertyFragment`
**Summary:** 
<br/>
**Remarks:** 
#### ` string enumElementFragment`
**Summary:** 
<br/>
**Remarks:** 
#### ` string enumFragment`
**Summary:** 
<br/>
**Remarks:** 
#### ` string paramFragment`
**Summary:** 
<br/>
**Remarks:** 
#### ` string output`
**Summary:** 
<br/>
**Remarks:** 
#### ` Char tagChar`
**Summary:** 
<br/>
**Remarks:** 

<br/>
<br/>

## Methods


### **List<PageData> Replace()**

**Summary:** 
<br/>
**Remarks:** 


### **Void SetupFragmentTemplates()**

**Summary:** 
<br/>
**Remarks:** 

```
 [TypeData](#xmldocgentypedata) _type   //
```
### **string TypeFragment([TypeData](#xmldocgentypedata) _type)**

**Summary:** 
<br/>
**Remarks:** 

```
 [TypeData](#xmldocgentypedata) _type   //
```
### **string EnumTypeFragment([TypeData](#xmldocgentypedata) _type)**

**Summary:** 
<br/>
**Remarks:** 

```
 [TypeData](#xmldocgentypedata) _type   //
```
### **string FieldFragments([TypeData](#xmldocgentypedata) _type)**

**Summary:** 
<br/>
**Remarks:** 

```
 [TypeData](#xmldocgentypedata) _type   //
```
### **string EnumElementFragment([TypeData](#xmldocgentypedata) _type)**

**Summary:** 
<br/>
**Remarks:** 

```
 [TypeData](#xmldocgentypedata) _type   //
```
### **string PropertyFragments([TypeData](#xmldocgentypedata) _type)**

**Summary:** 
<br/>
**Remarks:** 

```
 [TypeData](#xmldocgentypedata) _type   //
```
### **string MethodFragments([TypeData](#xmldocgentypedata) _type)**

**Summary:** 
<br/>
**Remarks:** 

```
 [MethodData](#xmldocgenmethoddata) _method   //
```
### **string ParamFragments([MethodData](#xmldocgenmethoddata) _method)**

**Summary:** 
<br/>
**Remarks:** 

```
 string _in   //
```
### **string CleanUpTags(string _in)**

**Summary:** 
<br/>
**Remarks:** 

```
 string _input   //
 [Tags](#xmldocgentags) _startTag   //
 [Tags](#xmldocgentags) _endTag   //
```
### **string GetInBetweenTags(string _input, [Tags](#xmldocgentags) _startTag, [Tags](#xmldocgentags) _endTag)**

**Summary:** 
<br/>
**Remarks:** 

```
 string _text   //
```
### **string ToPagePath(string _text)**

**Summary:** 
<br/>
**Remarks:** 


### **Void CleanOutputFolder()**

**Summary:** 
<br/>
**Remarks:** 

