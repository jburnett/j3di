using J3DI.Domain;
using System;
using System.Collections.Generic;


namespace J3DI.Infrastructure.EntityFactoryFx
{

    public abstract class EntityFactoryBase<TEntity, TEntityId> : IEntityFactory<TEntity, TEntityId>
        where TEntity : EntityBase<TEntityId>
    {
        #region IEntityFactory impl

        public abstract IEnumerable< EntityBase< TEntityId > > BuildEntities(System.Data.IDataReader reader);
        public abstract EntityBase< TEntityId > BuildEntity(System.Data.IDataRecord record);

        public Type EntityType()
        {
            return typeof(TEntity);
        }

        #endregion IEntityFactory impl


    }

}