using AMT.LinqExtensions;
using FluentAssertions;
using J3DI.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using Test.J3DI.Common;
using Xunit;


namespace Test.J3DI.Domain
{
    // TODO: include when doing code coverage
    // [ExcludeFromCodeCoverage]

    public class EntityBaseTests
    {

		#region Ctor tests

		[Fact]
		public void verify_EntityBase_ctors()
		{
			Action act;

			// Can create entity with no params
			act = () => new EntityByString1();
			act.Should().NotBeNull();


			foreach (string id in _validEntityIdStrings)
			{
				// Can create entity with valid id
				act = () => new EntityByString1(id);
				act.Should().NotBeNull();

				// Can create entity with valid id & creation date
				act = () => {
					var entity = new EntityByString1(id, _validCreateDates.Random());
					Console.WriteLine("Entity created: {0}", entity.Created);
					System.Diagnostics.Debug.WriteLine("Entity created: {0}", entity.Created);
				};
				act.Should().NotBeNull();
			}
			
		}


		[Fact]
		public void verify_excp_on_ctor_with_null_id()
		{
			// Verify creating entity with null string id throws appro excp
			Action ctor_with_null = () => new EntityByString1(null);
			ctor_with_null.Should().Throw<ArgumentNullException>(because: "id is null");


			// Verify creating entity with nullable id throws appro excp.
			//	NOTE: don't use nullable in production code. this one is just for testing.
			ctor_with_null = () => new EntityByNullableInt(null);
			ctor_with_null.Should().Throw<ArgumentNullException>(because: "id is null");

			ctor_with_null = () => new EntityByNullableGuid(null);
			ctor_with_null.Should().Throw<ArgumentNullException>(because: "id is null");
		}

		#endregion Ctor tests


		#region Entity equality, inequality tests

		[Fact]
		public void verify_EntityByGuid1_equality()
		{
			var e1Id = Guid.NewGuid();

			// An instance of an entity class
			EntityByGuid1 e1_ref1 = new EntityByGuid1(e1Id);
			// Another reference to the same instance
			EntityByGuid1 e1_ref2 = e1_ref1;
			// Another instance of the same entity class; same entity id, different date (which is irrelevant for equality)
			EntityByGuid1 e1_instance2 = new EntityByGuid1(e1Id, DateTime.Now.AddYears(-1));

			// Verify references to same object
			Assert.True(e1_ref1.Equals(e1_ref2));
			Assert.True(e1_ref1 == e1_ref2);
			Assert.False(e1_ref1 != e1_ref2);

			// Verify equality of different instances of same class with same Entity Id
			Assert.True(e1_ref1.Equals(e1_instance2));
			Assert.True(e1_ref1 == e1_instance2);
			Assert.False(e1_ref1 != e1_instance2);


			// Verify equality of different classes with same Entity Id
			EntityByGuid2 differentEntityType = new EntityByGuid2(e1Id);
		// TODO: REVIEW: should diff entity types w/same id type be considered equal?
			Assert.True(e1_ref1.Equals(differentEntityType));
//			Assert.False(true, "FORCED FAILURE: review equality issue re: different entity types");
			Assert.True(e1_ref1 == differentEntityType);
			Assert.False(e1_ref1 != differentEntityType);

		}


		[Fact]
		public void verify_EntityByInt1_equality()
		{
			int e1Id = -1;

			// An instance of an entity class
			EntityByInt1 e1_ref1 = new EntityByInt1(e1Id);
			// Another reference to the same instance
			EntityByInt1 e1_ref2 = e1_ref1;
			// Another instance of the same entity class; same entity id, different date (which is irrelevant for equality)
			EntityByInt1 e1_instance2 = new EntityByInt1(e1Id, DateTime.Now.AddYears(-1));

			// Verify references to same object
			Assert.True(e1_ref1.Equals(e1_ref2));
			Assert.True(e1_ref1 == e1_ref2);
			Assert.False(e1_ref1 != e1_ref2);

			// Verify equality of different instances of same class with same Entity Id
			Assert.True(e1_ref1.Equals(e1_instance2));
			Assert.True(e1_ref1 == e1_instance2);
			Assert.False(e1_ref1 != e1_instance2);
			
			
			// Verify equality of different classes with same Entity Id
			EntityByInt2 differentEntityType = new EntityByInt2(-1);
			Assert.True(e1_ref1.Equals(differentEntityType));
			Assert.True(e1_ref1 == differentEntityType);
			Assert.False(e1_ref1 != differentEntityType);

		}


		[Fact]
		public void verify_EntityByString1_equality()
		{
			// An instance of an entity class
			EntityByString1 e1_ref1 = new EntityByString1("id-1");
			// Another reference to the same instance
			EntityByString1 e1_ref2 = e1_ref1;
			// Another instance of the same entity class; same entity id, different date (which is irrelevant for equality)
			EntityByString1 e1_instance2 = new EntityByString1("id-1", DateTime.Now.AddYears(-1));

			// Another instance of the same entity class, but with different entity id
			EntityByString1 e2 = new EntityByString1("id-2");

			// Verify references to same object
			Assert.True(e1_ref1.Equals(e1_ref2));
			Assert.True(e1_ref1 == e1_ref2);
			Assert.False(e1_ref1 != e1_ref2);

			// Verify equality of different instances of same class with same Entity Id
			Assert.True(e1_ref1.Equals(e1_instance2));
			Assert.True(e1_ref1 == e1_instance2);
			Assert.False(e1_ref1 != e1_instance2);
			
			
			// Verify equality of different classes with same Entity Id
			EntityByString2 differentEntityType = new EntityByString2("id-1");
			Assert.True(e1_ref1.Equals(differentEntityType));
			Assert.True(e1_ref1 == differentEntityType);
			Assert.False(e1_ref1 != differentEntityType);

		}


		[Fact]
		public void verify_EntityByString1_inequality_when_null()
		{
			EntityByString1 e1_ref1 = new EntityByString1("id-1");

			// Verify references to same object
			Assert.False(e1_ref1.Equals(null));
			Assert.False(e1_ref1 == null);
			Assert.True(e1_ref1 != null);
		}


		[Fact]
		public void verify_null_ref_equals_null()
		{
			EntityByString1 nullRef = null;

			Assert.True(nullRef == null);
			Assert.False(nullRef != null);
		}

		#endregion Entity equality, inequality tests
		


		#region Entity type conversions

		[Fact]
		public void implicit_conversions()
		{
			EntityByInt1 entityByInt1 = new EntityByInt1(74);
			EntityByString1 entityByString1 = new EntityByString1("74");

			EntityBase<int> entityBaseInt = entityByInt1;
			Assert.NotNull(entityBaseInt);

			EntityBase<string> entityBaseString = entityByString1;
			Assert.NotNull(entityBaseString);
		}

		[Fact]
		public void explicit_conversions()
		{
			EntityBase<int> entityBaseInt = new EntityByInt1(74);
			EntityBase<string> entityBaseString = new EntityByString1("74");

			EntityByInt1 entityByInt1 = entityBaseInt as EntityByInt1;
			Assert.NotNull(entityByInt1);

			EntityByString1 entityByString1 = entityBaseString as EntityByString1;
			Assert.NotNull(entityByString1);
		}

		#endregion Entity type conversions
		

		// TODO: move to shared location for other tests?
		private readonly string[] _validEntityIdStrings = new string[] {
			string.Empty,	// TODO: should empty string be valid?
			"act",
			"0",
			"abc",
			"abcd"
			// TODO: add more interesting values.  URI's? unicode? unusual chars?
		};

		private readonly DateTime[] _validCreateDates = new DateTime[] {
			DateTime.MinValue,
			new DateTime(1999, 12, 31),
			new DateTime(2001, 9, 11),
			new DateTime(2000, 2, 29),	// a leap day
			new DateTime(2016, 8, 4, 18, 6, 23),
			DateTime.Now,
			DateTime.MaxValue
		};
    }
}
