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

    public class EntityByString1RepositoryTests {


        #region IEntityRepository.Add tests

        [Fact]
        public void can_add_entity_to_empty_repository()
        {
            string entityId = _DefaultEntityId( 7466 );
            var tempRepo = new EntityByString1Repository();

            var entity = new EntityByString1( entityId );

            // Add to empty repository
            tempRepo.Add( entity );

            var entityFound = tempRepo.FindById( entityId );

            // Verify the correct entity found
            entityFound.Should().Be(entity);
        }


        [Fact]
        public void valid_excp_on_overwrite_entity_in_repository()
        {
            // Create Id of an existing entity
            string entityId = _DefaultEntityId( 74 );

            // Verify the entity is already in the repository
            var entityInRepo = _repo.FindById( entityId );
            entityInRepo.Should().NotBeNull();
            
            // Create a 2nd entity with the same id
            var duplicate = new EntityByString1( entityId );

            // Try to overwrite the original entity with the new one
            Action act = () => {
                // Add to empty repository
                _repo.Add( duplicate );
            };

            // Verify the expected exception, and that message includes entity id
// TODO: excp messages differ b/t net452 and netcoreapp1.0. Intentional?
//            act.ShouldThrow<System.ArgumentException>().Where(e => e.Message.Contains(entityId));
            act.ShouldThrow<System.ArgumentException>();
        }

        #endregion IEntityRepository.Add tests
        

        #region IEntityRepository.FindById tests

        [Fact]
        public void valid_excp_on_FindById_on_empty_repository()
        {
            // Create action to find an entity from an empty repository
            Action act = () => {
                (new EntityByString1Repository()).FindById("DoesNotExist");
            };

            // Verify the correct exception thrown during the action
            act.ShouldThrow<System.Collections.Generic.KeyNotFoundException>();
        }


        [Fact]
        public void valid_excp_on_FindById_of_nonexisting_entity_on_nonempty_repository()
        {
            // Create action to find an entity from an empty repository
            Action act = () => {
                _repo.FindById("DoesNotExist");
            };

            // Verify the correct exception thrown during the action
            act.ShouldThrow<System.Collections.Generic.KeyNotFoundException>();
        }


        [Fact]
        public void can_find_valid_entities_in_repository()
        {
            // Use FindById multiple times to ensure entities are not released/freed
            for (uint searches = 0; searches < 2; ++searches)
            {

                for (uint i = 0; i < DEFAULT_ENTITY_COUNT; ++i)
                {
                    Action act = () => {
                        _repo.FindById(
                            _DefaultEntityId(i)
                        );
                    };

                    act.Should().NotBeNull();
                }

            }
        }

        #endregion IEntityRepository.FindById tests


        #region IEntityRepository.Remove tests

        [Fact]
        public void can_remove_first_entity_from_repository()
        {
            string entityId = _DefaultEntityId(0);

            // Can find entity before removal
            var entity = _repo.FindById(entityId);

            // Remove it 
            _repo.Remove(entity);

            // Verify it cannot be found again
            Action act = () => {
                _repo.FindById(entityId);
            };

            act.ShouldThrow<System.Collections.Generic.KeyNotFoundException>();
        }


        [Fact]
        public void can_remove_last_entity_from_repository()
        {
            string entityId = _DefaultEntityId(DEFAULT_ENTITY_COUNT - 1);

            // Can find entity before removal
            var entity = _repo.FindById(entityId);

            // Remove it 
            _repo.Remove(entity);

            // Verify it cannot be found again
            Action act = () => {
                _repo.FindById(entityId);
            };

            act.ShouldThrow< System.Collections.Generic.KeyNotFoundException >();
        }

        [Fact]
        public void no_excp_on_Remove_nonexisting_entity_on_nonempty_repository()
        {
            var entityToRemove = new EntityByString1( _DefaultEntityId(877466) );

            // Create action to find an entity from an empty repository
            Action act = () => {
                _repo.Remove( entityToRemove );
            };

            // Verify the correct exception thrown during the action
            act.ShouldNotThrow();
        }

        #endregion IEntityRepository.Remove tests


        #region Test Setup (ctor)

        public EntityByString1RepositoryTests()
        {
            _PopulateTestRepo(_repo);
        }

        #endregion Test Setup (ctor)


        #region Private methods

        private void _PopulateTestRepo(EntityByString1Repository target)
        {
            for (uint i = 0; i < DEFAULT_ENTITY_COUNT; ++i)
            {
                target.Add(
                    new EntityByString1(
                        _DefaultEntityId(i)
                    )
                );
            }
        }

        private string _DefaultEntityId(uint idx)
        {
            return string.Format("TestEntity_{0}", idx);
        }

        #endregion Private methods


        #region Private data

        private readonly uint DEFAULT_ENTITY_COUNT = 100;
        private EntityByString1Repository _repo = new EntityByString1Repository();

        #endregion Private data

    }

}

