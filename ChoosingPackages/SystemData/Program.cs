﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemDataModel;

namespace SystemData
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbName = @"C:\SQLite\contacs.db";

            Console.Write("Удалить базу и папку? (Y + Enter): ");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                // Удаление  бызы и папки если они есть
                if (File.Exists(dbName))
                    File.Delete(dbName);

                var folder = Path.GetDirectoryName(dbName);
                if (Directory.Exists(folder))
                    Directory.Delete(folder);
            }

            // Запрос к базе. При первом запросе, если нет базы,
            // то она автоматически создаётся и
            // заполняется данными для примера.
            List<ContactEntity> contacts;
            using (var db = new ContactsDB(dbName))
            {
                contacts = db.Contacts.ToList();

                Console.WriteLine(db.Database.Connection.Database);
            }

            Console.WriteLine(string.Join(Environment.NewLine, contacts.Select(c => $"{c.Id}: {c.Name}")));

            // Добавление одной записи
            Console.WriteLine();
            var contact = new ContactEntity() { Name = "Борис" };
            using (var db = new ContactsDB(dbName))
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                contacts = db.Contacts.ToList();
            }
            Console.WriteLine(string.Join(Environment.NewLine, contacts.Select(c => $"{c.Id}: {c.Name}")));

            // Изменение одной записи
            Console.WriteLine();
            using (var db = new ContactsDB(dbName))
            {
                contact = db.Contacts.Find(567);
                contact.Name = "Иванов Иван Иваныч";
                db.SaveChanges();
                contacts = db.Contacts.ToList();
            };
            Console.WriteLine(string.Join(Environment.NewLine, contacts.Select(c => $"{c.Id}: {c.Name}")));

            // Вывод с фильтрацией
            Console.WriteLine();
            List<PhoneEntity> phones;
            using (var db = new ContactsDB(dbName))
                phones = db.Phones.Where(ph => ph.ContactId == 567).ToList();

            Console.WriteLine(string.Join(Environment.NewLine, phones.Select(ph => $"{ph.ContactId}-{ph.Id} {ph.Title}: {ph.Number}")));

        }
    }
}
