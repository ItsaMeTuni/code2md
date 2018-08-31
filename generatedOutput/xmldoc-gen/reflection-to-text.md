# ReflectionToText

### Summary


### Remarks


## Fields

#### ` Dictionary<Type,String> customTypeNames`
**Summary:** 
<br/>
**Remarks:** 

<br/>
<br/>

## Methods

```
 Type _type   //
```
### **string GetMarkdownTypeName(Type _type)**

**Summary:** Gets _type's name (if there's a custom name for this type the custom name will be returned) 
<br/>
**Remarks:** 

```
 Type _type   //
```
### **string GetReadableGenericTypeName(Type _type)**

**Summary:** Gets the name of a generic type like List<string> instead of List`1 
<br/>
**Remarks:** If _type is not a generic type then the output will be _type.Name 

```
 ParameterInfo _param   //
```
### **string GetModifiersString(ParameterInfo _param)**

**Summary:** 
<br/>
**Remarks:** 

```
 FieldInfo _field   //
```
### **string GetModifiersString(FieldInfo _field)**

**Summary:** 
<br/>
**Remarks:** 

```
 MethodInfo _method   //
```
### **string GetModfiersString(MethodInfo _method)**

**Summary:** 
<br/>
**Remarks:** 

```
 Type _type   //
out  int _count   //
```
### **Type GetElementTypeRecursively(Type _type, int _count)**

**Summary:** 
<br/>
**Remarks:** 

```
 Type _type   //
```
### **Type GetElementTypeRecursively(Type _type)**

**Summary:** 
<br/>
**Remarks:** 

```
 MethodInfo _method   //
```
### **string GetTextSignature(MethodInfo _method)**

**Summary:** 
<br/>
**Remarks:** 

```
 PropertyInfo _property   //
```
### **string GetAcessorsStr(PropertyInfo _property)**

**Summary:** 
<br/>
**Remarks:** 

