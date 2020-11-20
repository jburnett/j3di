using J3DI.Domain;
using J3DI.Infrastructure.EntityFactoryFx;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using Test.J3DI.Common;


namespace Test.J3DI.Infrastructure.EntityFactoryFx {

    public class EntityByString1Factory : EntityFactoryBaseForTesting<EntityByString1, string>
    {
        public EntityByString1Factory(ILogger logger = null)
        {
            this._logger = logger;
        }


        #region IEntityFactory impl

        /// <summary>
        /// Builds a small list of EntityBaseString1 for testing
        /// </summary>
        public override IEnumerable< EntityBase< string > > BuildEntities(IDataReader reader)
        {
            _logger?.LogTrace("EntityByString1Factory.BuildEntities");

            if (null == reader  && ThrowExceptionOnNullDataReader) 
            {
                var exp =  new ArgumentNullException("reader");
                _logger?.LogError(exp, null);
                throw exp;
            }

            // Create a small list of entities
            List< EntityByString1 > list = new List< EntityByString1 >();
            for (int count = 0; count < 15; ++count)
            {
                list.Add( Create( System.Guid.NewGuid().ToString() ));
            }

            return list;
        }


	    public override EntityBase<string> BuildEntity(IDataRecord record)
        {
            _logger?.LogTrace("EntityByString1Factory.BuildEntity");

            if (null == record  && ThrowExceptionOnNullDataReader) 
            {
                var exp =  new ArgumentNullException("record");
                _logger?.LogError(exp, null);
                throw exp;
            }

            return Create( System.Guid.NewGuid().ToString() );
        }

        #endregion IEntityFactory impl


        #region Special additions for testing purposes only

        // For testing purposes, allow almost-direct entity creation.  This is a bad pattern in 
        //  normal (non-test) code; factories should always validate data and create entities.
        public EntityByString1 Create(string id)
        {
            _logger?.LogTrace("EntityByString1Factory.Create");

            return new EntityByString1(string.Format("EntityByString1_{0}", id));
        }


        #endregion Special additions for testing purposes only
    }
}