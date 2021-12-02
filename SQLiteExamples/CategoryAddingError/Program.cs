using System;
using System.IO;
using System.Linq;

namespace CategoryAddingError
{
    class Program
    {
        private static readonly string dbName = "worker.db";
        static void Main(string[] args)
        {
            if (File.Exists(dbName))
            {
                Console.Write("elfk БД (да - y)? ");
                if (Console.ReadLine().ToUpper().StartsWith("Y"))
                {
                    File.Delete(dbName);
                }
            }

            WorkerContext db = new(dbName);
            db.Database.EnsureCreated();


            Country[] countries = db.Countries.ToArray();

            Occupation occupation = new() { Title = "Стройка" };

            db.Occupations.Add(occupation);
            db.SaveChanges();

            Person person = new() { CountryId = 1, OccupationId = occupation.Id, FullName = "Alex" };

            db.People.Add(person);
            db.SaveChanges();

            Occupation occupation1 = new() { Title = "Перевозка" };

            db.Occupations.Add(occupation1);
            db.SaveChanges();

        }
    }
}
