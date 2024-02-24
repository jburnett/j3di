using System;


namespace J3DI.Domain
{
    public class EntityBase<TEntityId>
	{

		#region IEntity impl

		public TEntityId Id { get; set; }

		public DateTime Created { get; set; }

		#endregion IEntity impl


		#region Ctors

		protected EntityBase(TEntityId id, DateTime created)
		{
			// Null id is never valid
			if (null == id) { throw new ArgumentNullException("id"); }

			this.Id = id;
			this.Created = created;
		}


		// Non-null id with no value represents an unpersisted entity; its
		//	repository is responsible for assigning a domain unique id
		//	the first time the entity is written to the repository.
		protected EntityBase() : this(default(TEntityId)) { }

		protected EntityBase(TEntityId id) : this(id, DateTime.Now) { }

		#endregion Ctors


		#region Entity equality

		public override bool Equals(object obj)
		{
			if (null == obj || !(obj is EntityBase<TEntityId>))
			{
				return false;
			}

			// Invokes the operator override
			return this == (obj as EntityBase<TEntityId>);
		}


		public static bool operator ==(EntityBase<TEntityId> e1, EntityBase<TEntityId> e2)
		{
			bool areEqual = false;

			if (ReferenceEquals(e1, e2))
			{
				areEqual = true;
			}

			// Cast to object to avoid recursion
			if (((object)e1 == null) || ((object)e2 == null))
			{
				// If both are null, return true; otherwise false since only one is null
				if (((object)e1 == null) && ((object)e2 == null))
				{
					areEqual = true;
				}
			}
			else
			{
				// Two entities are equivalent if their id values match
				if (e1.Id.Equals(e2.Id))
				{
					areEqual = true;
				}
			}
		
			return areEqual;
		}


		public static bool operator !=(EntityBase<TEntityId> e1, EntityBase<TEntityId> e2)
		{
			return (!(e1 == e2));
		}


		/*
			TODO: REVIEW: Entity equality is based on Id, but should hash code? Using Id for hash code
			results in different entities (e.g., int Ids) having the same hash code.
		*/
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		#endregion Entity equality


	}
}
