using J3DI.Domain;
using System;
using System.Diagnostics.CodeAnalysis;


namespace Test.J3DI.Common
{
    [ExcludeFromCodeCoverage]
	public class EntityByGuid1 : EntityBase<Guid>
    {

		#region Ctors

		public EntityByGuid1() : base() { }
		public EntityByGuid1(Guid id) : base(id) { }
		public EntityByGuid1(Guid id, DateTime created) : base(id, created) { }

		#endregion Ctors

	}


    [ExcludeFromCodeCoverage]
	public class EntityByGuid2 : EntityBase<Guid>
    {

		#region Ctors

		public EntityByGuid2() : base() { }
		public EntityByGuid2(Guid id) : base(id) { }
		public EntityByGuid2(Guid id, DateTime created) : base(id, created) { }

		#endregion Ctors
	}


    /*
        NOTE: This class is only for test purposes. Don't use entities which
        allow nullable base types for real code.
    */
    [ExcludeFromCodeCoverage]
	public class EntityByNullableGuid : EntityBase<Guid?>
    {

		#region Ctors

		public EntityByNullableGuid() : base() { }
		public EntityByNullableGuid(Guid? id) : base(id) { }
		public EntityByNullableGuid(Guid? id, DateTime created) : base(id, created) { }

		#endregion Ctors
        
	}


}


