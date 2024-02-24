using J3DI.Domain;
using System;
using System.Diagnostics.CodeAnalysis;


namespace Test.J3DI.Common
{
	[ExcludeFromCodeCoverage]
	public class EntityByInt1 : EntityBase<int>
    {

		#region Ctors

		public EntityByInt1() : base() { }
		public EntityByInt1(int id) : base(id) { }
		public EntityByInt1(int id, DateTime created) : base(id, created) { }

		#endregion Ctors
	}


	[ExcludeFromCodeCoverage]
	public class EntityByInt2 : EntityBase<int>
    {

		#region Ctors

		public EntityByInt2() : base() { }
		public EntityByInt2(int id) : base(id) { }
		public EntityByInt2(int id, DateTime created) : base(id, created) { }

		#endregion Ctors
	}



    /*
        NOTE: This class is only for test purposes. Don't use entities which
        allow nullable base types for real code.
    */
	[ExcludeFromCodeCoverage]
	public class EntityByNullableInt : EntityBase<int?>
    {

		#region Ctors

		public EntityByNullableInt() : base() { }
		public EntityByNullableInt(int? id) : base(id) { }
		public EntityByNullableInt(int? id, DateTime created) : base(id, created) { }

		#endregion Ctors
	}

}
