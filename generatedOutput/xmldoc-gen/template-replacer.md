# TemplateReplacer

**Summary:** 
**Remarks:** 

## Fields

|Modifiers            |Type          | Name         | Description
|---------------------|--------------|:------------:|------------
|  | `string` | template | **Summary:**  **Remarks:** 
|  | `List<TypeData>` | typeDatas | **Summary:**  **Remarks:** 
|  | `string` | classFragment | **Summary:**  **Remarks:** 
|  | `string` | fieldsFragment | **Summary:**  **Remarks:** 
|  | `string` | propertiesFragment | **Summary:**  **Remarks:** 
|  | `string` | methodsFragment | **Summary:**  **Remarks:** 
|  | `string` | paramsFragment | **Summary:**  **Remarks:** 
|  | `string` | fieldFragment | **Summary:**  **Remarks:** 
|  | `string` | methodFragment | **Summary:**  **Remarks:** 
|  | `string` | propertyFragment | **Summary:**  **Remarks:** 
|  | `string` | enumElementFragment | **Summary:**  **Remarks:** 
|  | `string` | enumFragment | **Summary:**  **Remarks:** 
|  | `string` | paramFragment | **Summary:**  **Remarks:** 
|  | `string` | output | **Summary:**  **Remarks:** 
|  | `Char` | tagChar | **Summary:**  **Remarks:** 

<br/>
<br/>

## Methods


### **`List<PageData>` Replace()**

**Summary:** 
**Remarks:** 



### **`Void` SetupFragmentTemplates()**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 
### **`string` TypeFragment([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 
### **`string` EnumTypeFragment([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 
### **`string` FieldFragments([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 
### **`string` EnumElementFragment([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 
### **`string` PropertyFragments([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 
### **`string` MethodFragments([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`MethodData`](#xmldocgenmethoddata) | \_method | ` ` | 
### **`string` ParamFragments([`MethodData`](#xmldocgenmethoddata) _method)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_in | ` ` | 
### **`string` CleanUpTags(`string` _in)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_input | ` ` | 
|  |[`Tags`](#xmldocgentags) | \_startTag | ` ` | 
|  |[`Tags`](#xmldocgentags) | \_endTag | ` ` | 
### **`string` GetInBetweenTags(`string` _input, [`Tags`](#xmldocgentags) _startTag, [`Tags`](#xmldocgentags) _endTag)**

**Summary:** 
**Remarks:** 


| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_text | ` ` | 
### **`string` ToPagePath(`string` _text)**

**Summary:** 
**Remarks:** 



### **`Void` CleanOutputFolder()**

**Summary:** 
**Remarks:** 


