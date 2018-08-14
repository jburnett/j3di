using FluentAssertions;
using J3DI.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using Test.J3DI.Common;
using Xunit;


namespace Test.J3DI.Domain
{

    public class ValueObjectBaseTests
    {

		#region Ctor tests

		[Fact]
		public void verify_ValueObjectBase_ctors()
		{
			Action act;

			// Can create entity with no params
			act = () => new ValueObjectA();
			act.Should().NotBeNull();

		}

		#endregion Ctor tests


		#region ValueObject equality, inequality tests

        // TODO: what other equality & inequality tests should be included here?
        
        [Fact]
        public void diff_instances_of_same_ValueObject_are_not_equal()
        {
            var a1 = new ValueObjectA();
            var a2 = new ValueObjectA();

            a1.Should().NotBe(a2);     // verifies equality via object.Equals
            a1.Should().NotBeSameAs(a2);    // verifies separate instances
        }

        
		#endregion ValueObject equality, inequality tests

    }

}