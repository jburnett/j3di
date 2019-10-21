all: clean test 


test: build
	dotnet test ./test.j3di.domain
	dotnet test ./test.j3di.infrastructure.entityfactoryfx
	dotnet test ./test.j3di.infrastructure.repositoryfx

cover: build
	dotnet test /p:CollectCoverage=true /p:Include="[j3di*]*" /p:Exclude="[test.j3di*]*"  ./test.j3di.domain
	dotnet test /p:CollectCoverage=true /p:Include="[j3di*]*" /p:Exclude="[test.j3di*]*"  ./test.j3di.infrastructure.entityfactoryfx
	dotnet test /p:CollectCoverage=true /p:Include="[j3di*]*" /p:Exclude="[test.j3di*]*"  ./test.j3di.infrastructure.repositoryfx

build: 
	dotnet build


clean:
	dotnet clean
