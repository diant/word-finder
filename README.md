# Word finder

Word finder for English(UK)
Uses the SOWPODS dictionary

## CLI

### Installation

- Go to folder `src\WordFinder.CLI\` and run the following commands:
- `dotnet pack -c Release`
- `dotnet tool install -g --add-source .\nupkg wfind`

Once installed, update with `dotnet tool update -g --add-source .\nupkg wfind`

Alternatively, on the folder `src\WordFinder.CLI\tools` there are 3 scripts to install, update and uninstall the tool.
- `install.ps1`
- `update.ps1`
- `uninstall.ps1`

### Usage

Type `wfind` or `wfind --help` for more information

## API

Exports a REST API with the following endpoints:
- `POST /api/wordfinder/{FindWordsOptions}` to get all words containing the given letters

## WEB

MVC web application
