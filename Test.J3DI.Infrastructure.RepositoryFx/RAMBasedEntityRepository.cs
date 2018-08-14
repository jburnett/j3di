using J3DI.Domain;
using J3DI.Infrastructure.RepositoryFx;
using System.Collections.Generic;
using System.ComponentModel;


namespace Test.J3DI.Infrastructure.RepositoryFx
{
    // TODO: REVIEW: should this class be abstract?

    public class RAMBasedEntityRepository<TEntity, TEntityId> : IEntityRepository< TEntity, TEntityId > 
        where TEntity : EntityBase<TEntityId>
    {

        #region IEntityRepository matching methods

        EntityBase<TEntityId> IEntityRepository<TEntity, TEntityId>.FindById(TEntityId entityId)
        {
            return FindById(entityId);
        }

        void IEntityRepository<TEntity, TEntityId>.Add(EntityBase<TEntityId> entity)
        {
            this.Add(entity as TEntity);
        }

        void IEntityRepository<TEntity, TEntityId>.Remove(EntityBase<TEntityId> entity)
        {
            this.Remove(entity as TEntity);
        }

        void IEntityRepository<TEntity, TEntityId>.Update(EntityBase<TEntityId> entity)
        {
            this.Update(entity as TEntity);
        }

        #endregion IEntityRepository matching methods
        


        #region IEntityRepository impl

        public virtual TEntity FindById(TEntityId entityId)
        {
            return _repository[entityId];
        }


        public virtual void Add(TEntity entity)
        {
            if (OnAddingEntity(entity).Cancel == false)
            {
                _repository.Add(entity.Id, entity);
                OnAddedEntity(entity);
            }
        }


        public virtual void Remove(TEntity entity)
        {
            if(OnRemovingEntity(entity).Cancel == false)
            {
                if (_repository.Remove(entity.Id))
                {
                    OnRemovedEntity(entity);
                }
            }
        }


        public virtual void Update(TEntity entity)
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
        }


        #region IEntityRepository Events

        public virtual event AddingEntityEventHandler<TEntity, TEntityId> AddingEntity;
        public virtual event AddedEntityEventHandler<TEntity, TEntityId> AddedEntity;

        public virtual event RemovingEntityEventHandler<TEntity, TEntityId> RemovingEntity;
        public virtual event RemovedEntityEventHandler<TEntity, TEntityId> RemovedEntity;

        public virtual event UpdatingEntityEventHandler<TEntity, TEntityId> UpdatingEntity;
        public virtual event UpdatedEntityEventHandler<TEntity, TEntityId> UpdatedEntity;

        #endregion IEntityRepository Events

        
        #endregion IEntityRepository impl

        protected virtual CancelEventArgs OnAddingEntity(TEntity entity)
        {
            System.Diagnostics.Trace.Assert(null != entity);

            var cea = new CancelEventArgs(false);

            if (null != AddingEntity)
            {
                this.AddingEntity(entity, cea);
            }

            return cea;
        }

        protected virtual void OnAddedEntity(TEntity entity)
        {
            System.Diagnostics.Trace.Assert(null != entity);

            if (null != AddedEntity)
            {
                this.AddedEntity(entity);
            }
        }


        protected virtual CancelEventArgs OnRemovingEntity(TEntity entity)
        {
            System.Diagnostics.Trace.Assert(null != entity);

            var cea = new CancelEventArgs(false);

            if (null != RemovingEntity)
            {
                this.RemovingEntity(entity, cea);
            }

            return cea;
        }

        protected virtual void OnRemovedEntity(TEntity entity)
        {
            System.Diagnostics.Trace.Assert(null != entity);

            if (null != RemovedEntity)
            {
                this.RemovedEntity(entity);
            }
        }


        protected virtual CancelEventArgs OnUpdatingEntity(TEntity entity)
        {
            System.Diagnostics.Trace.Assert(null != entity);

            var cea = new CancelEventArgs(false);

            if (null != UpdatingEntity)
            {
                this.UpdatingEntity(entity, cea);
            }

            return cea;
        }

        protected virtual void OnUpdatedEntity(TEntity entity)
        {
            System.Diagnostics.Trace.Assert(null != entity);

            if (null != UpdatedEntity)
            {
                this.UpdatedEntity(entity);
            }
        }



        // An in-memory repository
        private Dictionary<TEntityId, TEntity> _repository = new Dictionary<TEntityId, TEntity>();

    }

}