
== Filtering ==

    dotnet test --filter "ClassName=ExpressiveReflection.Tests.TypeReflectionTests&Name=Test083"

== Test Results Output ==

    dotnet test --logger "trx;LogFileName=abc.trx"

With test output in dotnet core there doesn't appear to be a working listener for the System.Diagnostics.Debug output stream, which used to be captured to the same channel that Console.WriteLine() style output was written
