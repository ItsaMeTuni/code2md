# What is code2md?

code2md is (or _will_ be) a command line tool that creates markdown pages from a generated XML documentation file. It is similar to doxygen but it also enables you to put the generated markdown files into mkdocs or other tools like that so you can have a easily customizable code reference.

# Features it _should_ have

* Generate one md page per type.
* Each type page should have a list of fields, properties, methods, child types, etc
* Every time there is a type name written on the page it should be a link to the type's page
* Methods should contain the following (all optional) sections: parameter list, return type, remarks and example.

# See the progress

## For the lazy ones

I added a [GeneratedExample.md file](GeneratedExample.md) file at the root of the repo. That is the most recent generated documentation file. Oopen it and take a look if you're too lazy to clone the repo, compile and execute it :)

## How to test it

Just build this project and run it, it will generate a documentation file about its own assembly.

# Why this barely works?

Because it is a huge project in development by one single guy XD. If you want to help I'd greatly appreciate it. If you like the idea I think you're gonna have to wait a bit until you can use it...
