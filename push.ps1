param (
    [string]$PackagePath,
    [string]$ApiKey
)

dotnet nuget push -k $ApiKey -s https://api.nuget.org/v3/index.json $PackagePath
