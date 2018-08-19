
# MarkdownBuilder

**Summary:** 
**Remarks:** 

## Fields

|Modifiers            |Type          | Name         | Description
|---------------------|--------------|:------------:|------------

<br/>
<br/>

## Properties

|Modifiers            | Type            | Name            | Acessors             | Description
|---------------------|-----------------|:---------------:|----------------------|------------
| | `string` | Value | $property_accessors$ | **Summary:** Current value of the markdown text  **Remarks:** 

<br/>
<br/>

## Methods


### **`Void` AddText(`string` _text)**

**Summary:** Adds a line of text and a new line at the end 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_text | ` ` | 

<br/>
<br/>


### **`Void` EmptyLine()**

**Summary:** Adds an empty line to the text 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------

<br/>
<br/>


### **`Void` H1(`string` _title)**

**Summary:** Create a level 1 header (i.e. #_title) 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_title | ` ` | 

<br/>
<br/>


### **`Void` H2(`string` _title)**

**Summary:** Create a level 2 header (i.e. ##_title) 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_title | ` ` | 

<br/>
<br/>


### **`Void` H3(`string` _title)**

**Summary:** Create a level 3 header (i.e. ###_title) 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_title | ` ` | 

<br/>
<br/>


### **`Void` H4(`string` _title)**

**Summary:** Create a level 4 header (i.e. ####_title) 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_title | ` ` | 

<br/>
<br/>


### **`Void` Space1()**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------

<br/>
<br/>


### **`Void` Space2()**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------

<br/>
<br/>


### **`Void` Space3()**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------

<br/>
<br/>


### **`Void` Bullet(`string` _text)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_text | ` ` | 

<br/>
<br/>


### **`Void` AddHeader(`int` _i, `string` _title)**

**Summary:** Create a header of level _i 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`int` | \_i | ` ` | 
|  |`string` | \_title | ` ` | 

<br/>
<br/>


### **`Void` AddSpace(`int` _i)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`int` | \_i | ` ` | 

<br/>
<br/>


### **`Void` CreateTable(`string[]` _headers, [`Alignment[]`](#xmldocgenalignment) _alignments, `string[][]` _data)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string[]` | \_headers | ` ` | 
|  |[`Alignment[]`](#xmldocgenalignment) | \_alignments | ` ` | 
| params  |`string[][]` | \_data | ` ` | 

<br/>
<br/>


### **`string` CreateHeaderLink(`string` _text, `string` _toHeader, `int` _headerLevel)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_text | ` ` | 
|  |`string` | \_toHeader | ` ` | 
|  |`int` | \_headerLevel | ` ` | 

<br/>
<br/>


### **`string` CreatePageLink(`string` _text, `string` _pagePath)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_text | ` ` | 
|  |`string` | \_pagePath | ` ` | 

<br/>
<br/>


### **`string` CreateCode(`string` _code)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_code | ` ` | 

<br/>
<br/>


### **`string` Escape(`string` _text)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |`string` | \_text | ` ` | 

<br/>
<br/>


### **[`MarkdownBuilder`](#xmldocgenmarkdownbuilder) op_Addition([`MarkdownBuilder`](#xmldocgenmarkdownbuilder) _l, `string` _r)**

**Summary:** 
**Remarks:** 

| Modifiers       | Type             | Name             | Default value | Description
|-----------------|------------------|:----------------:|---------------|------------
|  |[`MarkdownBuilder`](#xmldocgenmarkdownbuilder) | \_l | ` ` | 
|  |`string` | \_r | ` ` | 

<br/>
<br/>

