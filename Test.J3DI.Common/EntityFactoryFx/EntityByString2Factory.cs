using J3DI.Domain;
using J3DI.Infrastructure.EntityFactoryFx;
using System;
using System.Collections.Generic;
using Test.J3DI.Common;


namespace Test.J3DI.Infrastructure.EntityFactoryFx {

    public class EntityByString2Factory : EntityFactoryBaseForTesting<EntityByString2, string>
    {
        #region IEntityFactory impl

        public override IEnumerable< EntityBase< string > > BuildEntities(System.Data.IDataReader reader)
        {
            if (null == reader  && ThrowExceptionOnNullDataReader) { throw new ArgumentNullException("reader"); }

            List< EntityByString2 > list = new List< EntityByString2 >();

            for (int count = 0; count < 15; ++count)
            {
                list.Add( Create( System.Guid.NewGuid().ToString() ));
            }

            return list;
        }


	    public override EntityBase<string> BuildEntity(System.Data.IDataRecord record)
        {
            if (null == record  && ThrowExceptionOnNullDataReader) { throw new ArgumentNullException("record"); }

            return Create( System.Guid.NewGuid().ToString() );
        }

        #endregion IEntityFactory impl


        #region Special additions for testing purposes only

        // For testing purposes, allow almost-direct entity creation.  This is a bad pattern in 
        //  normal (non-test) code; factories should always validate data and create entities.
        public EntityByString2 Create(string id)
        {
            return new EntityByString2(string.Format("EntityByString2_{0}", id ));
        }

        #endregion Special additions for testing purposes only
    }
}