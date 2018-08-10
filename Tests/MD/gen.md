# XMLDocGen.DummyClass

## Foo
Does foo 
### Remarks
Remarkable indeed 
Name | Description
--- | ---
_a | I dont know what this does
_b | neither this
_c | or this

# XMLDocGen.Extractor

## GetMethodName

### Remarks

## RemoveNamePrefix

### Remarks

## CleanString

### Remarks

# XMLDocGen.MarkdownHelper

## get_Value

### Remarks

## set_Value

### Remarks

## AddText
Adds a line of text and a new line at the end 
### Remarks

Name | Description
--- | ---
_text | 

## H1
Create a level 1 header (i.e. #_title) 
### Remarks

Name | Description
--- | ---
_title | Title of the header

## H2
Create a level 2 header (i.e. ##_title) 
### Remarks

Name | Description
--- | ---
_title | Title of the header

## H3
Create a level 3 header (i.e. ###_title) 
### Remarks

Name | Description
--- | ---
_title | Title of the header

## AddHeader
Create a header of level _i 
### Remarks

Name | Description
--- | ---
_i | Level of the header (i.e. how many "#" before the title)
_title | Title of the header

## CreateTable

### Remarks

## op_Addition

### Remarks

# XMLDocGen.ParameterData

# XMLDocGen.MethodData

# XMLDocGen.ClassData

# XMLDocGen.FieldData

# XMLDocGen.Program

## Main

### Remarks

## Generate

### Remarks

## ReadAssembly
Reads the assembly and creates class/method/field data used to generate the markdown 
### Remarks

Name | Description
--- | ---
_assembly | 
_xml | 

## PrintEverything

### Remarks

## ToMarkdown
Converts the class list into a markdown page and outputs it into a file 
### Remarks

# XMLDocGen.Utils

## ListFieldToList

### Remarks

## ToArray

### Remarks

## RegexReplaceOnArray

### Remarks

## RegexMatchOnArray

### Remarks

## CleanString

### Remarks

## FindMethodMemberWithName

### Remarks

## FindMemberWithName

### Remarks

