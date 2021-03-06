# Versioning
This is version 2.0.1 of the expressive reflection library.

This package is available from nuget at: https://www.nuget.org/packages/ExpressiveReflection/2.0.1

The source for this release is available on github at: https://github.com/hannasm/ExpressiveReflectionDotNet/releases/tag/2.0.1

## Source Packages
The dotnet core releases to date do not support source code transformations, which was an essential feature for the source only distribution of this project. The source only distributions has been discontinued until this feature makes it back into the dotnet core featureset.

# ExpressiveReflectionDotnet
This is a .NET library for simplifying reflection / metadata programming and making 
any code related to reflection more readable and maintainable. This library includes
a collection of reflection related tools that would be cumbersome to implement yourself
each time you need them, but aren't available in the .NET framework directly. 

This library also presents an interface for using  .NET reflection APIs, 
based on expression tree syntax first introduced with LINQ, and makes it trivially
simple to access code metadata, in the same way that you would access those properties
in normal code.

# Expression Tree Syntax

Let's take a look at some simple examples of how to do reflection using expression tree syntax:

#### Lookup a property from the string class
```C# 
using ExpressiveReflection;

Reflection.GetMember(()=>default(string).Length); // returns PropertyInfo for string.Length
```

#### Lookup a method of the string class
```C#
using ExpressiveReflection;

Reflection.GetMethod(()=>default(string).Substring(default(int), default(int)); // returns MethodInfo for string.Substring(int,int) 
```

#### Lookup the constructor to create a decimal from long
```C#
using ExpressiveReflection;

Reflection.GetConstructor(()=>new decimal(default(long))); // returns constructorInfo for new decimal(string)
```

The ExpressiveReflection.Reflection class exposes all of the reflection methods from a single central location.

# Tests
There is a fairly comprehensive set of unit tests, and additionald examples demonstrating functionality can be found there.

# Release Notes;

[For Release Notes See Here](ExpressiveReflection.ReleaseNotes.md)
