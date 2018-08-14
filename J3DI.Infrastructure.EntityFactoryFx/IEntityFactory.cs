using J3DI.Domain;
using System;
using System.Collections.Generic;


namespace J3DI.Infrastructure.EntityFactoryFx
{
    
    public interface IEntityFactory< TEntity, TEntityId > where TEntity : EntityBase<TEntityId>
    {
        IEnumerable< EntityBase< TEntityId > > BuildEntities(System.Data.IDataReader reader);
        EntityBase< TEntityId > BuildEntity(System.Data.IDataRecord record);
        Type EntityType();
    }
    
}