del /Q ExpressiveReflection*.nupkg
nuget pack -Symbol -Prop Configuration=Release
nuget push ExpressiveReflection*.nupkg


REM vim: set expandtab ts=2 sw=2: 
