
echo "Installing the word-finder tool ..."
dotnet pack -c Release -v q
echo "Tool packed! Updating ..."
dotnet tool update -g --add-source .\nupkg wfind
echo "word-finder installed successfully! Type wfinder to start exploring!"