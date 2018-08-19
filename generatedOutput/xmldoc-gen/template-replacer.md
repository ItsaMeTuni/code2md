
# TemplateReplacer

**Summary:** 
**Remarks:** 

## Fields

|Modifiers            |Type          | Name         | Description
|---------------------|--------------|:------------:|------------
|  | `string` | template | **Summary:**  **Remarks:** 
|  | `List<TypeData>` | typeDatas | **Summary:**  **Remarks:** 
|  | `string` | classFragment | **Summary:**  **Remarks:** 
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

## Properties

|Modifiers            | Type            | Name            | Acessors             | Description
|---------------------|-----------------|:---------------:|----------------------|------------

<br/>
<br/>

## Methods


### **`Void` Replace()**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------

<br/>
<br/>


### **`List<PageData>` CreatePages()**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------

<br/>
<br/>


### **`Void` SetupFragmentTemplates()**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------

<br/>
<br/>


### **`string` TypeFragment([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 

<br/>
<br/>


### **`string` EnumTypeFragment([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 

<br/>
<br/>


### **`string` FieldFragments([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 

<br/>
<br/>


### **`string` EnumElementFragment([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 

<br/>
<br/>


### **`string` PropertyFragments([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 

<br/>
<br/>


### **`string` MethodFragments([`TypeData`](#xmldocgentypedata) _type)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`TypeData`](#xmldocgentypedata) | \_type | ` ` | 

<br/>
<br/>


### **`string` ParamFragments([`MethodData`](#xmldocgenmethoddata) _method)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`MethodData`](#xmldocgenmethoddata) | \_method | ` ` | 

<br/>
<br/>


### **`string` CleanUpTags(`string` _in)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_in | ` ` | 

<br/>
<br/>


### **`string` GetInBetweenTags(`string` _input, [`Tags`](#xmldocgentags) _startTag, [`Tags`](#xmldocgentags) _endTag)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_input | ` ` | 
|  |[`Tags`](#xmldocgentags) | \_startTag | ` ` | 
|  |[`Tags`](#xmldocgentags) | \_endTag | ` ` | 

<br/>
<br/>


### **`string` ToPagePath(`string` _text)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_text | ` ` | 

<br/>
<br/>


### **`Void` CleanOutputFolder()**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------

<br/>
<br/>

