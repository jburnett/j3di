using J3DI.Domain;
using System;
using System.Diagnostics.CodeAnalysis;


namespace Test.J3DI.Common
{

	// EntityByString1 and EntityByString2 are distinct entity classes
	//	and should never be equal
	[ExcludeFromCodeCoverage]
	public class EntityByString1 : EntityBase<string>
    {

		#region Ctors

		public EntityByString1() : base() { }
		public EntityByString1(string id) : base(id) { }
		public EntityByString1(string id, DateTime created) : base(id, created) { }

		#endregion Ctors
	}


	[ExcludeFromCodeCoverage]
	public class EntityByString2 : EntityBase<string>
    {

		#region Ctors

		public EntityByString2() : base() { }
		public EntityByString2(string id) : base(id) { }
		public EntityByString2(string id, DateTime created) : base(id, created) { }

		#endregion Ctors
	}


}