using System.Threading;
using System;
using System.Collections.Generic;
using RestWithASPNET.Model;

namespace RestWithASPNET.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public List<Person> FindAll()
        {
            List<Person> people = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                people.Add(person);
            }
            return people;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "testeFN" + i,
                LastName = "testeLN" + i,
                Address = "rua de teste" + i,
                Gender = "male"
            };
        }

        public Person FindById(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "teste",
                LastName = "teste",
                Address = "rua de teste",
                Gender = "male"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

        public Person Update(Person person)
        {
            return person;
        }
    }
}