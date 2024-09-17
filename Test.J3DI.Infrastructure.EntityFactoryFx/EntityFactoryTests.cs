using J3DI.Domain;
using System;
using System.Diagnostics.CodeAnalysis;
using Test.J3DI.Common;
using Xunit;


namespace Test.J3DI.Infrastructure.EntityFactoryFx {

	[ExcludeFromCodeCoverage]
    public class EntityFactoryTests {

        [Fact]
        public void can_build_entity()
        {
            var e1Factory = new EntityByString1Factory();
            e1Factory.ThrowExceptionOnNullDataReader = false;

            Assert.Equal(
                typeof(EntityByString1),
                e1Factory.EntityType()
            );
            
            Assert.Equal(
                typeof(EntityByString1),
                (new EntityByString1Factory().EntityType())
            );

            EntityBase<string> e1 = e1Factory.BuildEntity(null);
            Assert.Equal( "EntityByString1", e1.Id.Split('_')[0] );
        }


        [Fact]
        public void verify_excp_on_null_datareader()
        {
            EntityByString1 e1 = null;
            Exception ex = Assert.Throws<ArgumentNullException>(
                () => e1 = (EntityByString1)(new EntityByString1Factory()).BuildEntity(null)
            );
            Assert.Contains("record", ex.Message);
        }


        [Fact]
        public void verify_correct_type_from_EntityType()
        {
            Assert.Equal(
                typeof(EntityByString1),
                (new EntityByString1Factory().EntityType())
            );

            Assert.Equal(
                typeof(EntityByString2),
                (new EntityByString2Factory().EntityType())
            );
        }

    }
}