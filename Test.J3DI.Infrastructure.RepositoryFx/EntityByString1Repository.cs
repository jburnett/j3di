using J3DI.Infrastructure.RepositoryFx;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Test.J3DI.Common;


namespace Test.J3DI.Infrastructure.RepositoryFx {

	[ExcludeFromCodeCoverage]
    public class EntityByString1Repository : EntityRepositoryBase<EntityByString1, string>, IEntityRepository<EntityByString1, string> 
    {

        #region IEntityRepository impl


        public override EntityByString1 FindById(string entityId)
        {
            return _repository[entityId];
        }


        public override void Add(EntityByString1 entity)
        {
            if (OnAddingEntity(entity).Cancel == false)
            {
                _repository.Add(entity.Id, entity);
                OnAddedEntity(entity);
            }
        }


        public override void Remove(EntityByString1 entity)
        {
            if(OnRemovingEntity(entity).Cancel == false)
            {
                if (_repository.Remove(entity.Id))
                {
                    OnRemovedEntity(entity);
                }
            }
        }


        public override void Update(EntityByString1 entity)
        {
            // Ensure previous exists
            if (_repository.ContainsKey(entity.Id))
            {
                if(OnUpdatingEntity(entity).Cancel == false)
                {
                    // Replace the previous 
                    _repository.Remove(entity.Id);
                    _repository.Add(entity.Id, entity);
                    OnUpdatedEntity(entity);
                }
            }
            // TODO: throw exception for update on non-existent entity?
        }


        #endregion IEntityRepository impl


        // An in-mem repo
        private Dictionary<string, EntityByString1> _repository = new Dictionary<string, EntityByString1>();

    }
}