using J3DI.Domain;
using System.ComponentModel;


namespace J3DI.Infrastructure.RepositoryFx
{

    public abstract class EntityRepositoryBase<TEntity, TEntityId> : IEntityRepository<TEntity, TEntityId> 
        where TEntity : EntityBase<TEntityId>
    {

        #region IEntityRepository matching methods
        // IEntityRepository invocations are mapped to the abstract methods

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

        public abstract TEntity FindById(TEntityId entityId);

        public abstract void Add(TEntity entity);

        public abstract void Remove(TEntity entity);

        public abstract void Update(TEntity entity);


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

    }

}