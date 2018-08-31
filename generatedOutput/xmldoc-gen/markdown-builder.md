# MarkdownBuilder

### Summary


### Remarks



## Properties

#### ` string Value` 
**Acessors:** $property_accessors$
<br/>
**Summary:** Current value of the markdown text 
<br/>
**Remarks:** 

<br/>
<br/>
## Methods

```
 string _text   //
```
### **Void AddText(string _text)**

**Summary:** Adds a line of text and a new line at the end 
<br/>
**Remarks:** 


### **Void EmptyLine()**

**Summary:** Adds an empty line to the text 
<br/>
**Remarks:** 

```
 string _title   //
```
### **Void H1(string _title)**

**Summary:** Create a level 1 header (i.e. #_title) 
<br/>
**Remarks:** 

```
 string _title   //
```
### **Void H2(string _title)**

**Summary:** Create a level 2 header (i.e. ##_title) 
<br/>
**Remarks:** 

```
 string _title   //
```
### **Void H3(string _title)**

**Summary:** Create a level 3 header (i.e. ###_title) 
<br/>
**Remarks:** 

```
 string _title   //
```
### **Void H4(string _title)**

**Summary:** Create a level 4 header (i.e. ####_title) 
<br/>
**Remarks:** 


### **Void Space1()**

**Summary:** 
<br/>
**Remarks:** 


### **Void Space2()**

**Summary:** 
<br/>
**Remarks:** 


### **Void Space3()**

**Summary:** 
<br/>
**Remarks:** 

```
 string _text   //
```
### **Void Bullet(string _text)**

**Summary:** 
<br/>
**Remarks:** 

```
 int _i   //
 string _title   //
```
### **Void AddHeader(int _i, string _title)**

**Summary:** Create a header of level _i 
<br/>
**Remarks:** 

```
 int _i   //
```
### **Void AddSpace(int _i)**

**Summary:** 
<br/>
**Remarks:** 

```
 string[] _headers   //
 [Alignment[]](#xmldocgenalignment) _alignments   //
params  string[][] _data   //
```
### **Void CreateTable(string[] _headers, [Alignment[]](#xmldocgenalignment) _alignments, string[][] _data)**

**Summary:** 
<br/>
**Remarks:** 

```
 string _text   //
 string _toHeader   //
 int _headerLevel   //
```
### **string CreateHeaderLink(string _text, string _toHeader, int _headerLevel)**

**Summary:** 
<br/>
**Remarks:** 

```
 string _text   //
 string _pagePath   //
```
### **string CreatePageLink(string _text, string _pagePath)**

**Summary:** 
<br/>
**Remarks:** 

```
 string _code   //
```
### **string CreateCode(string _code)**

**Summary:** 
<br/>
**Remarks:** 

```
 string _text   //
```
### **string Escape(string _text)**

**Summary:** 
<br/>
**Remarks:** 

```
 [MarkdownBuilder](#xmldocgenmarkdownbuilder) _l   //
 string _r   //
```
### **[MarkdownBuilder](#xmldocgenmarkdownbuilder) op_Addition([MarkdownBuilder](#xmldocgenmarkdownbuilder) _l, string _r)**

**Summary:** 
<br/>
**Remarks:** 

