using J3DI.Domain;
using System.Collections.Generic;


namespace J3DI.Infrastructure.EntityFactoryFx
{

    public class EntityFactoryManager
    {

        public static void Register< TEntity, TEntityId >(IEntityFactory<TEntity, TEntityId> entityFactory, bool overwriteExisting = true)
            where TEntity: EntityBase<TEntityId>
		{
            if (null == entityFactory)  { throw new System.ArgumentNullException("entityFactory"); }

            string entityTypeName = entityFactory.EntityType().Name;
            System.Diagnostics.Debug.Assert(null != entityTypeName);

            System.Diagnostics.Debug.Assert(null != repo);

            if (overwriteExisting && repo.ContainsKey(entityTypeName)) {
                // remove if exists
                repo.Remove(entityTypeName);
            }

            // TODO: use custom exception for case when factory already exists for the key.
            //  Currently just allowing exception from Dictionary
            repo.Add(entityTypeName, entityFactory);
        }


        public static IEntityFactory<TEntity, TEntityId> GetFactory<TEntity, TEntityId>()
            where TEntity: EntityBase<TEntityId>
		{
            string entityTypeName = typeof(TEntity).Name;
            System.Diagnostics.Debug.Assert(null != entityTypeName);
            
            System.Diagnostics.Debug.Assert(null != repo);

            IEntityFactory<TEntity, TEntityId> factory = null;
            if (repo.ContainsKey(entityTypeName))
            {
                factory = repo[entityTypeName] as IEntityFactory<TEntity, TEntityId>;
            }

            return factory;
		}


         private static Dictionary<string, object> repo = new Dictionary<string , object>();

   }

}