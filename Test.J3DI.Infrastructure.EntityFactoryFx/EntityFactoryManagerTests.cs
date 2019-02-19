using J3DI.Domain;
using J3DI.Infrastructure.EntityFactoryFx;
using System;
using Test.J3DI.Common;
using Test.J3DI.Domain;
using Xunit;


namespace Test.J3DI.Infrastructure.EntityFactoryFx {

    public class EntityFactoryManagerTests {

        [Fact]
        public void can_register_nonexisting_factory()
        {
            // Register the entity factory
            EntityByString1Factory factory = new EntityByString1Factory();
            EntityFactoryManager.Register<EntityByString1, string>(factory );

            // Retrieve & verify it
            EntityByString1Factory factoryActual = EntityFactoryManager.GetFactory<EntityByString1, string>() as EntityByString1Factory;
            Assert.True(ReferenceEquals(factory, factoryActual));
        }


        [Fact]
        public void can_register_multiple_factories()
        {
            // Register the entity factory
            EntityByString1Factory factory1 = new EntityByString1Factory();
            EntityFactoryManager.Register<EntityByString1, string>(factory1);

            // Register the entity factory
            EntityByString2Factory factory2 = new EntityByString2Factory();
            EntityFactoryManager.Register<EntityByString2, string>(factory2);


            // Retrieve & verify them
            EntityByString1Factory factoryActual1 = EntityFactoryManager.GetFactory<EntityByString1, string>() as EntityByString1Factory;
            Assert.True(ReferenceEquals(factory1, factoryActual1));

            EntityByString2Factory factoryActual2 = EntityFactoryManager.GetFactory<EntityByString2, string>() as EntityByString2Factory;
            Assert.True(ReferenceEquals(factory2, factoryActual2));
        }


        [Fact]
        public void can_replace_existing_factory()
        {
            // Register the entity factory
            IEntityFactory<EntityByString1, string> factory = new EntityByString1Factory();
            EntityFactoryManager.Register<EntityByString1, string>(factory);

            // Register it again
            EntityFactoryManager.Register<EntityByString1, string>(factory);

            // Retrieve & verify it
            IEntityFactory<EntityByString1, string> factoryActual = EntityFactoryManager.GetFactory<EntityByString1, string>();
            Assert.True(ReferenceEquals(factory, factoryActual));
        }


        [Fact]
        public void verify_excp_on_duplicate_registration()
        {
            // Register the entity factory
            IEntityFactory<EntityByString1, string> factory = new EntityByString1Factory();
            EntityFactoryManager.Register<EntityByString1, string>(factory );

            // Register it again, but pass false for overwriteExisting
            Exception ex = Assert.Throws<ArgumentException>(
                () => EntityFactoryManager.Register<EntityByString1, string>(factory, false)
            );
            Assert.Contains("An item with the same key has already been added.", ex.Message);
        }


        [Fact]
        public void verify_excp_on_registering_null()
        {
            Exception ex = Assert.Throws<ArgumentNullException>(
                () => EntityFactoryManager.Register<EntityByString1, string>(null)
            );
            Assert.Contains("Value cannot be null", ex.Message);            
            Assert.Contains("entityFactory", ex.Message);
        }
    }

}