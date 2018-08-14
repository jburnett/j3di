$projPaths = @(
    "J3DI.Domain",
    "J3DI.Infrastructure.EntityFactoryFx"
    "J3DI.Infrastructure.RepositoryFx"
)

# Ensure everything is built
Write-Verbose "Pack projects"
$projPaths | %{
    write-host ""   # separator
    dotnet pack -c Release $_
}
