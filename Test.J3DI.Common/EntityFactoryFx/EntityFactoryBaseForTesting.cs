using J3DI.Domain;
using J3DI.Infrastructure.EntityFactoryFx;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace Test.J3DI.Infrastructure.EntityFactoryFx {

    /// <summary>
    /// An entity factory specialized for testing purposes.
    /// </summary>
    /// <remarks>
    /// Specialized to allow control over whether BuildEntity throws exception
    /// when IDataReader parameter is null.  The exception is valuable for testing 
    /// EntityFactoryManager; disabling it is valuable for building the entity directly
    /// (without requiring an actual IDataReader impl)
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public abstract class EntityFactoryBaseForTesting<TEntity, TEntityId> : IEntityFactory<TEntity, TEntityId>
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


        #region Special additions for testing purposes only

        // For testing purposes only.  Allow test to control whether
        //  exception will be thrown when null passed to BuildEntity.
        //  Ignoring null allows very simple, direct BuildEntity which
        //  doesn't use a DataReader.  
        public bool ThrowExceptionOnNullDataReader { get; set; }


        public EntityFactoryBaseForTesting()
        {
            // Throw exception by default
            ThrowExceptionOnNullDataReader = true;
        }


        #endregion Special additions for testing purposes only
    }
}