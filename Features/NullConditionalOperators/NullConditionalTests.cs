﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Features.NullConditionalOperators.DataStructures;

namespace Features.NullConditionalOperators
{
    [TestFixture]
    public class NullConditionalTests
    {
        [Test]
        public void GetRoadAndNumberDetails_ReturnsExpectedElementsFromTestData()
        {
            var testDataCollection = CreateTestPeople();
            var filters = new PersonFilters();

            var result = filters.GetHouseAndRoadDetailsWherePresent(testDataCollection);
            var expected = new List<string> { "25 Somewhere Street", "12 Another Road" };

            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void NullCoalescingOperatorSubstituesMissingPropertyValue()
        {
            var testDataCollection = CreateTestPeople();
            var filters = new PersonFilters();

            var results = filters.CollectFirstNamesWithNullCoalescing(testDataCollection);
            var substituedValues = results.Where(n => n == "Unknown");

            Assert.AreEqual(2, substituedValues.Count());
        }

        [Test]
        public void NamesByDateCheck_ReturnsNullWhenParameterIsNull()
        {
            var filters = new PersonFilters();

            Assert.IsNull(filters.CollectNamesByDateWithNullCheckOnParameter(null, DateTime.Now));
        }

        [Test]
        public void NamesByDateCheck_ReturnsExpectedNamesWhenParameterIsNotNull()
        {
            var testDataCollection = CreateNamedAndDatedPeople();
            var filters = new PersonFilters();

            var results = filters.CollectNamesByDateWithNullCheckOnParameter(testDataCollection, new DateTime(2012, 1, 1));
            var expected = new List<string> { "Amy Hill", "Alison Smith" };
        }

        [Test]
        public void EmptyCollectionReturnedWhenParameterIsNull_WithNullCoalescingCheckOnFilterResult()
        {
            var filters = new PersonFilters();

            var result = filters.CollectNamesByDate_ReturningEmptyCollectionFullNullParameter(null, DateTime.Now);
            Assert.AreEqual(0, result.Count());
        }
        
        private IEnumerable<Person> CreateTestPeople()
        {
            return new List<Person>
            {
                new Person { Forename = "Fred", Lastname = "Bloggs" },
                new Person { Lastname = "Smith", HomeAddress = new Address { HouseNumber = 25, Road = "Somewhere Street" } },
                new Person { Lastname = "Oates", HomeAddress = new Address { HouseNumber = 12, Road = "Another Road"  } }
            };
        }

        private IEnumerable<Person> CreateNamedAndDatedPeople()
        {
            return new List<Person>
            {
                new Person { Forename = "Amy", Lastname = "Hill", DateJoined = new DateTime(2013, 8, 17) },
                new Person { Forename = "Allister", Lastname = "Crouch", DateJoined = new DateTime(2011, 10, 10) },
                new Person { Forename = "Alison", Lastname = "Smith", DateJoined = new DateTime(2015, 6, 23) }
            };
        }
    }
}
