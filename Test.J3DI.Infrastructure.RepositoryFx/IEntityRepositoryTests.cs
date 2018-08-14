using FluentAssertions;
using J3DI.Domain;
using J3DI.Infrastructure.EntityFactoryFx;
using J3DI.Infrastructure.RepositoryFx;
using System;
using Test.J3DI.Common;
using Test.J3DI.Domain;
using Test.J3DI.Infrastructure.EntityFactoryFx;
using Xunit;


namespace Test.J3DI.Infrastructure.RepositoryFx {

    public class IEntityRepositoryTests {

        [Fact]
        public void receives_Add_events()
        {
            bool addingReceived = false;
            bool addedReceived = false;

            var repo = new RAMBasedEntityRepository<EntityByString1, string>();

            repo.AddingEntity += (o, cea) => {
                Assert.NotNull(o);
                Assert.NotNull(cea);
                addingReceived = true;
            };

            repo.AddedEntity += (o) => {
                Assert.NotNull(o);
                addedReceived = true;
            };

            // Add an entity
            repo.Add(new EntityByString1("7466"));

            // Verify Adding & Added events received
            Assert.True(addingReceived);
            Assert.True(addedReceived);
        }


        [Fact]
        public void can_cancel_AddingEntity_event()
        {
            bool addingReceived = false;
            bool addedReceived = false;

            var repo = new RAMBasedEntityRepository<EntityByString1, string>();

            repo.AddingEntity += (o, cea) => {
                Assert.NotNull(o);
                Assert.NotNull(cea);
                addingReceived = true;

                // Cancel the Add operation
                cea.Cancel = true;
            };

            repo.AddedEntity += (o) => {
                Assert.NotNull(o);
                addedReceived = true;
            };

            repo.Add(new EntityByString1("7466"));

            Assert.True(addingReceived);

            // Verify Added event was not sent
            Assert.False(addedReceived);

            // Verify entity was not added to repository
            Action act = () => repo.FindById("7466");
            act.ShouldThrow<System.Collections.Generic.KeyNotFoundException>();            
        }


        [Fact]
        public void receives_Remove_events()
        {
            bool addedReceived = false;
            bool removingReceived = false;
            bool removedReceived = false;
            var testEntity = new EntityByString1("7466");

            var repo = new RAMBasedEntityRepository<EntityByString1, string>();

            repo.AddedEntity += (o) => {
                Assert.NotNull(o);
                addedReceived = true;
            };
            repo.Add(testEntity);
            Assert.True(addedReceived);


            repo.RemovingEntity += (o, cea) => {
                Assert.NotNull(o);
                Assert.NotNull(cea);
                removingReceived = true;
            };

            repo.RemovedEntity += (o) => {
                Assert.NotNull(o);
                removedReceived = true;
            };

            repo.Remove(testEntity);

            Assert.True(removingReceived);
            Assert.True(removedReceived);

            // Verify entity was not added to repository
            Action act = () => repo.FindById(testEntity.Id);
            act.ShouldThrow<System.Collections.Generic.KeyNotFoundException>();            
        }


        [Fact]
        public void can_cancel_RemovingEntity_event()
        {
            bool addedReceived = false;
            bool removingReceived = false;
            bool removedReceived = false;
            var testEntity = new EntityByString1("7466");

            var repo = new RAMBasedEntityRepository<EntityByString1, string>();

            repo.AddedEntity += (o) => {
                Assert.NotNull(o);
                addedReceived = true;
            };
            repo.Add(testEntity);
            Assert.True(addedReceived);


            repo.RemovingEntity += (o, cea) => {
                Assert.NotNull(o);
                Assert.NotNull(cea);
                removingReceived = true;

                // Cancel the remove action
                cea.Cancel = true;
            };

            repo.RemovedEntity += (o) => {
                Assert.NotNull(o);
                removedReceived = true;
            };

            repo.Remove(testEntity);

            Assert.True(removingReceived);
            Assert.False(removedReceived);

            var e = repo.FindById("7466");
            Assert.True(e.Id.Equals("7466"));
        }


        [Fact]
        public void no_events_on_Remove_nonexistent()
        {
            bool removingReceived = false;
            bool removedReceived = false;
            var testEntity = new EntityByString1("does_not_exist");

            var repo = new RAMBasedEntityRepository<EntityByString1, string>();

            repo.RemovingEntity += (o, cea) => {
                Assert.NotNull(o);
                Assert.NotNull(cea);
                removingReceived = true;
            };

            repo.RemovedEntity += (o) => {
                Assert.NotNull(o);
                removedReceived = true;
            };

            repo.Remove(testEntity);

            Assert.True(removingReceived);
            Assert.False(removedReceived);

            // Verify entity was not added to repository
            Action act = () => repo.FindById(testEntity.Id);
            act.ShouldThrow<System.Collections.Generic.KeyNotFoundException>();            
        }


        [Fact]
        public void receives_Update_events()
        {
            bool addingReceived = false;
            bool addedReceived = false;
            bool removingReceived = false;
            bool removedReceived = false;
            bool UpdatingReceived = false;
            bool UpdatedReceived = false;
            var testEntity = new EntityByString1("7466");

            var repo = new RAMBasedEntityRepository<EntityByString1, string>();
            repo.Add(testEntity);

            repo.AddingEntity += (o, cea) => {
                Assert.NotNull(o);
                Assert.NotNull(cea);
                addingReceived = true;
            };
            repo.AddedEntity += (o) => {
                Assert.NotNull(o);
                addedReceived = true;
            };

            repo.RemovingEntity += (o, cea) => {
                Assert.NotNull(o);
                Assert.NotNull(cea);
                removingReceived = true;
            };
            repo.RemovedEntity += (o) => {
                Assert.NotNull(o);
                removedReceived = true;
            };


            repo.UpdatingEntity += (o, cea) => {
                Assert.NotNull(o);
                Assert.NotNull(cea);
                UpdatingReceived = true;
            };
            repo.UpdatedEntity += (o) => {
                Assert.NotNull(o);
                UpdatedReceived = true;
            };


            var copyOfTestEntity = new EntityByString1(testEntity.Id);
            repo.Update(copyOfTestEntity);

            Assert.True(UpdatingReceived);
            Assert.True(UpdatedReceived);
            // Ensure other events were not triggered
            Assert.False(addingReceived);
            Assert.False(addedReceived);
            Assert.False(removingReceived);
            Assert.False(removedReceived);
        }


        [Fact]
        public void can_cancel_UpdatingEntity_event()
        {
            bool addingReceived = false;
            bool addedReceived = false;
            bool removingReceived = false;
            bool removedReceived = false;
            bool UpdatingReceived = false;
            bool UpdatedReceived = false;
            var testEntity = new EntityByString1("7466");

            var repo = new RAMBasedEntityRepository<EntityByString1, string>();
            repo.Add(testEntity);

            repo.AddingEntity += (o, cea) => {
                Assert.NotNull(o);
                Assert.NotNull(cea);
                addingReceived = true;
            };
            repo.AddedEntity += (o) => {
                Assert.NotNull(o);
                addedReceived = true;
            };

            repo.RemovingEntity += (o, cea) => {
                Assert.NotNull(o);
                Assert.NotNull(cea);
                removingReceived = true;
            };
            repo.RemovedEntity += (o) => {
                Assert.NotNull(o);
                removedReceived = true;
            };


            repo.UpdatingEntity += (o, cea) => {
                Assert.NotNull(o);
                Assert.NotNull(cea);
                UpdatingReceived = true;

                // Cancel the Update action
                cea.Cancel = true;
            };

            repo.UpdatedEntity += (o) => {
                Assert.NotNull(o);
                UpdatedReceived = true;
            };


            var copyOfTestEntity = new EntityByString1(testEntity.Id);
            repo.Update(copyOfTestEntity);

            Assert.True(UpdatingReceived);
            Assert.False(UpdatedReceived);
            // Ensure other events were not triggered
            Assert.False(addingReceived);
            Assert.False(addedReceived);
            Assert.False(removingReceived);
            Assert.False(removedReceived);
        }


    }

}

