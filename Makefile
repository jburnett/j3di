all: test 


test: build
	dotnet test ./Test.J3DI.Domain
	dotnet test ./Test.J3DI.Infrastructure.EntityFactoryFx
	dotnet test ./Test.J3DI.Infrastructure.RepositoryFx


cover: build
	dotnet test --no-build /p:CollectCoverage=true ./Test.J3DI.Domain/Test.J3DI.Domain.csproj
	dotnet test --no-build /p:CollectCoverage=true ./Test.J3DI.Infrastructure.EntityfactoryFx/Test.J3DI.Infrastructure.EntityfactoryFx.csproj
	dotnet test --no-build /p:CollectCoverage=true ./Test.J3DI.Infrastructure.RepositoryFx/Test.J3DI.Infrastructure.RepositoryFx.csproj

cover-report:
	reportgenerator \
		-targetdir:coveragereport \
		"-reports:./Test.J3DI.Domain/coverage.net6.0.json"
		
#		./Test.J3DI.Domain/coverage.net6.0.json;./Test.J3DI.Common/coverage.net6.0.json;./Test.J3DI.Infrastructure.RepositoryFx/coverage.net6.0.json;./Test.J3DI.Infrastructure.EntityFactoryFx/coverage.net6.0.json


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