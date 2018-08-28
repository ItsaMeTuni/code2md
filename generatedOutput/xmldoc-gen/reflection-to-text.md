# ReflectionToText

**Summary:** 
**Remarks:** 

## Fields

|Modifiers            |Type          | Name         | Description
|---------------------|--------------|:------------:|------------
|  | `Dictionary<Type,String>` | customTypeNames | **Summary:**  **Remarks:** 

<br/>
<br/>

## Methods

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`Type` | \_type | ` ` | 
### **`string` GetMarkdownTypeName(`Type` _type)**

**Summary:** Gets _type's name (if there's a custom name for this type the custom name will be returned) 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`Type` | \_type | ` ` | 
### **`string` GetReadableGenericTypeName(`Type` _type)**

**Summary:** Gets the name of a generic type like List<string> instead of List`1 
**Remarks:** If _type is not a generic type then the output will be _type.Name 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`ParameterInfo` | \_param | ` ` | 
### **`string` GetModifiersString(`ParameterInfo` _param)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`FieldInfo` | \_field | ` ` | 
### **`string` GetModifiersString(`FieldInfo` _field)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`MethodInfo` | \_method | ` ` | 
### **`string` GetModfiersString(`MethodInfo` _method)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`Type` | \_type | ` ` | 
| out  |`int` | \_count | ` ` | 
### **`Type` GetElementTypeRecursively(`Type` _type, `int` _count)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`Type` | \_type | ` ` | 
### **`Type` GetElementTypeRecursively(`Type` _type)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`MethodInfo` | \_method | ` ` | 
### **`string` GetTextSignature(`MethodInfo` _method)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`PropertyInfo` | \_property | ` ` | 
### **`string` GetAcessorsStr(`PropertyInfo` _property)**

**Summary:** 
**Remarks:** 


