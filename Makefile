all: test 


test: build
	dotnet test ./Test.J3DI.Domain
	dotnet test ./Test.J3DI.Infrastructure.EntityFactoryFx
	dotnet test ./Test.J3DI.Infrastructure.RepositoryFx


cover: build
	dotnet test /p:CollectCoverage=true /p:Include="[J3DI*]*" /p:Exclude="[Test.J3DI*]*"  ./Test.J3DI.Domain
	dotnet test /p:CollectCoverage=true /p:Include="[J3DI*]*" /p:Exclude="[Test.J3DI*]*"  ./Test.J3DI.Infrastructure.EntityfactoryFx
	dotnet test /p:CollectCoverage=true /p:Include="[J3DI*]*" /p:Exclude="[Test.J3DI*]*"  ./Test.J3DI.Infrastructure.RepositoryFx


build: 
	dotnet build


clean:
	dotnet clean


clean-all: clean
	@echo "### Remove all NuPkg files"
	find . -iname "*.nupkg" -exec rm {} \;
	@echo "### Remove bin and obj dirs"
	find . -type d -name "bin" -exec rm -r {} \;
	find . -type d -name "obj" -exec rm -r {} \;