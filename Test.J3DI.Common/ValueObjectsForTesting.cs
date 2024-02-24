using System.Diagnostics.CodeAnalysis;
using J3DI.Domain;


namespace Test.J3DI.Common
{

	[ExcludeFromCodeCoverage]
    public class ValueObjectA : ValueObjectBase
    {
        public string StringProperty1 { get; private set; }
    }


	[ExcludeFromCodeCoverage]
    public class ValueObjectB : ValueObjectBase
    {
        public string StringProperty1 { get; private set; }
    }

}