#!/bin/sh

# Creates a new project, add it to the sln file and to the test project

dotnet new console -n "$1"
dotnet sln add "$1"/"$1".csproj
dotnet add Tests/Tests.csproj reference "$1"/
