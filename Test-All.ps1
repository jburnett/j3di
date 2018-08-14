$testPaths = @(
    "Test.J3DI.Domain",
    "Test.J3DI.Infrastructure.EntityFactoryFx"
    "Test.J3DI.Infrastructure.RepositoryFx"
)


$testPaths | %{
    gci $_ *.csproj | %{
        $projFile = $_

        Write-Host "Running tests in " $projFile.FullName " for framework $fx"
        dotnet test $projFile.FullName | Out-Host
        Write-Host ""   #separator
    }
}
