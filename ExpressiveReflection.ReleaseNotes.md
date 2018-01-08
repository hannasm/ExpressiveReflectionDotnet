      * 2.0.1 - Experimented with getting the source only distribution working, it turns out the source transformation feature and nuget content in general just isn't available with dotnet core, and so the source distribution is gone until further notice.
      * 2.0.0 - Updated library, releases are now targeting .NET core 2
      * 1.4.1 - Fix for issue with calls to Reflection.IsNullAssignable() and extension method <Type>.IsNullAssignable() calling the wrong function in TypeReflection.
      * 1.4.0 - Hit some need for looking up a generic method, and only have a Type[] available to use to do it. Found a solution on stackoverflow that works, and doesn't require intense mental effort to use. I would like something cleaner down the road but it will do for now: GetMethodExt()
      * 1.3.3 - Fix issues with Transmute(), where two separate members defined on the same type have the same metadata toekn, because the two members are declared in different modules and by chance they happened to overlap
      * 1.3.2 - Implemented a MemberReflection.Transmute() method to complement the other Transmute() offerings
      * 1.3.2 - rewrite constructorReflection.Transmute() to use same MetadataToken trick that the MethodReflection.Transmtue() relies on
      * 1.3.1 - Fix for bug in MethodReflection.Transmute() leading to completely random method derivations being returned
      * 1.3.1 - Fix for bug in ConstructorReflection.Transmute() leading to no transmute operation taking place
      * 1.3.0 - added Transmute() methods for Constructors / Methods / Types allowing quick change of generic type arguments
      * 1.3.0 - added a few new overloads to MethodReflection.From() to allow support for methods returning void
      * 1.2.7 - added collection reflection with helpers for coercing a type to / from IEnumerable
      * 1.2.6 - apparently the binary nuget package didn't have a dll in it
      * 1.2.5 - fixing an issue with the comments produced in source distribution
      * 1.2.4 - after hitting some issues with filesystem path length limitations renaming the folder the Sources package installs to in the hopes of minimizing the filename slizes
      * 1.2.3 - last attempt at packaging didn't work right when multiple projcets included the same package in the solution so rewrote it again to use .pp transformations
      * 1.2.2 - this is a large scale rewrite of the Sources distrbution, making this Sources package safer for use, however no new functionality was added otherwise
      * 1.2.1 - bumping the version to fix some minor things in the soruce package
      * 1.2.0 - added getValue(MemberInfo) / setValue(MemberInfo) methods to MemberReflection
      * 1.2.0 - add ExpressiveReflection.Extensions namespace and exposed most functionality as extension methods on their corresponding types
