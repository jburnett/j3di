using J3DI.Domain;
using System.ComponentModel;


namespace J3DI.Infrastructure.RepositoryFx
{
    
    public interface IEntityRepository< TEntity, TEntityId > 
        where TEntity : EntityBase<TEntityId>
    {
        EntityBase<TEntityId> FindById(TEntityId entityId);


        #region Adding entities

        void Add(EntityBase<TEntityId> entity);
        event AddingEntityEventHandler<TEntity, TEntityId> AddingEntity;
        event AddedEntityEventHandler<TEntity, TEntityId> AddedEntity;

        #endregion Adding entities


        #region Removing entities

        void Remove(EntityBase<TEntityId> entity);
        event RemovingEntityEventHandler<TEntity, TEntityId> RemovingEntity;
        event RemovedEntityEventHandler<TEntity, TEntityId> RemovedEntity;

        #endregion Removing entities


        #region Updating entities

        void Update(EntityBase<TEntityId> entity);
        event UpdatingEntityEventHandler<TEntity, TEntityId> UpdatingEntity;
        event UpdatedEntityEventHandler<TEntity, TEntityId> UpdatedEntity;

        #endregion Updating entities

    }


    #region Event handler delegates

    public delegate void AddingEntityEventHandler<TEntity, TEntityId>(object sender, CancelEventArgs args)
        where TEntity : EntityBase<TEntityId>;

    public delegate void AddedEntityEventHandler<TEntity, TEntityId>(object sender)
        where TEntity : EntityBase<TEntityId>;

    public delegate void RemovingEntityEventHandler<TEntity, TEntityId>(object sender, CancelEventArgs args)
        where TEntity : EntityBase<TEntityId>;

    public delegate void RemovedEntityEventHandler<TEntity, TEntityId>(object sender)
        where TEntity : EntityBase<TEntityId>;

    public delegate void UpdatingEntityEventHandler<TEntity, TEntityId>(object sender, CancelEventArgs args)
        where TEntity : EntityBase<TEntityId>;

    public delegate void UpdatedEntityEventHandler<TEntity, TEntityId>(object sender)
        where TEntity : EntityBase<TEntityId>;

    #endregion Event handler delegates

}