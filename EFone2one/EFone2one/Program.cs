using System;
using System.Data.Entity;
using System.Linq;

namespace EFone2one
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<PeopleContext>());
            
            var context = new PeopleContext();
            context.Database.Log = Console.WriteLine; // so we can inspect SQL statements

            AddOnePerson(context);

            var people = context.Persons.Include(x => x.Address).ToList();
            Console.WriteLine("We have " + people.Count + " people in db.");

            Console.WriteLine("done.");
            Console.ReadKey();
        }

        private static void AddOnePerson(PeopleContext context)
        {
            // We need to create new Person without AddressId, save that to db to get its Id
            // and then create Address and fill both ends of relationship (PersonId and AddressId)
            // That's why Person.AddressId is nullable.

            var john = new Person {Name = "john"};
            context.Persons.Add(john);
            context.SaveChanges();

            var address = new Address() {StreetName = "street", PersonId = john.Id};
            john.Address = address;
            context.Addresses.Add(address);
            context.SaveChanges();
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }

    public class PeopleContext : DbContext
    {
        public IDbSet<Person> Persons { get; set; }
        public IDbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // one to many Person - Address
            modelBuilder.Entity<Person>()
                .HasOptional(x => x.Address).WithMany()
                .HasForeignKey(x => x.AddressId);

            // one to many Address - Person
            modelBuilder.Entity<Address>()
                .HasRequired(x => x.Person).WithMany()
                .HasForeignKey(x => x.PersonId);
        }
    }
}
