dotnet pack ../WordFinder.CLI.csproj -c Release
dotnet tool update -g --add-source ..\nupkg wfind