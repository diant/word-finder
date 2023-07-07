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

- `wfind -l <letters>` with default grouping by word length
- `wfind -l <letters> -g <Group Option>` to get grouped results by word length (l), points (p) or no grouping (n). Default is by word length.
- `wfind -l <letters> -c <Contains>` to get results that contains the given string.

Type `wfind` or `wfind --help` for more information

## API

Exports a REST API with the following endpoints:
- `GET /api/wordfinder/{letters}` to get all words containing the given letters
