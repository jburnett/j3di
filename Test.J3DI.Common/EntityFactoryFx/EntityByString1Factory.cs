using J3DI.Domain;
using J3DI.Infrastructure.EntityFactoryFx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Test.J3DI.Common;


namespace Test.J3DI.Infrastructure.EntityFactoryFx {

    [ExcludeFromCodeCoverage]
    public class EntityByString1Factory : EntityFactoryBaseForTesting<EntityByString1, string>
    {
        #region IEntityFactory impl

        public override IEnumerable< EntityBase< string > > BuildEntities(IDataReader reader)
        {
            if (null == reader  && ThrowExceptionOnNullDataReader) { throw new ArgumentNullException("reader"); }

            List< EntityByString1 > list = new List< EntityByString1 >();

            for (int count = 0; count < 15; ++count)
            {
                list.Add( Create( System.Guid.NewGuid().ToString() ));
            }

            return list;
        }


	    public override EntityBase<string> BuildEntity(IDataRecord record)
        {
            if (null == record  && ThrowExceptionOnNullDataReader) { throw new ArgumentNullException("record"); }

            return Create( System.Guid.NewGuid().ToString() );
        }

        #endregion IEntityFactory impl


        #region Special additions for testing purposes only

        // For testing purposes, allow almost-direct entity creation.  This is a bad pattern in 
        //  normal (non-test) code; factories should always validate data and create entities.
        public EntityByString1 Create(string id)
        {
            return new EntityByString1(string.Format("EntityByString1_{0}", id));
        }


        #endregion Special additions for testing purposes only
    }
}