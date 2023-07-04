# word-finder

Word finder for English(UK)

## Credits

Word list taken from [here](https://github.com/dwyl/english-words)
Thanks to [dwyl](https://github.com/dwyl) for the word list

## CLI

### Installation

- `dotnet pack -c Release`
- `dotnet tool install -g --add-source .\nupkg wfind`

Once isntalled, update with `dotnet tool update -g --add-source .\nupkg wfind`

### Usage

- `wfind -l <letters>`
- `wfind -l <letters> -g` to get groupped results by word length (True by default)

Type `wfind` or `wfind --help` for more information

## API
