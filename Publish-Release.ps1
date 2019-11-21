param (
    [Parameter(Mandatory=$true)][string]$apiKey
)


$projPaths = @(
    "J3DI.Domain",
    "J3DI.Infrastructure.EntityFactoryFx"
    "J3DI.Infrastructure.RepositoryFx"
)


# Ensure everything is built
Write-Verbose "Publish projects"
$projPaths | %{
    write-host ""   # separator
    $pkg = gci $_/bin/Release *.nupkg
    dotnet nuget push -k $apiKey $pkg.FullName -s https://api.nuget.org/v3/index.json
}
