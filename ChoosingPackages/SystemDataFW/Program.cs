using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemDataFW
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbName = @"C:\SQLite\contacs.db";

            //Console.Write("Удалить базу и папку? (Y + Enter): ");
            //if (Console.ReadLine().ToUpper() == "Y")
            //{
            //    // Удаление  бызы и папки если они есть
            //    if (File.Exists(dbName))
            //        File.Delete(dbName);

            //    var folder = Path.GetDirectoryName(dbName);
            //    if (Directory.Exists(folder))
            //        Directory.Delete(folder);
            //}

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
            Console.Write("Введите имя для добавляемого контакта: ");
            var contact = new ContactEntity() { Name = Console.ReadLine() };
            using (var db = new ContactsDB(dbName))
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                contacts = db.Contacts.ToList();
            }
            Console.WriteLine(string.Join(Environment.NewLine, contacts.Select(c => $"{c.Id}: {c.Name}")));


        inputId:
            // Изменение одной записи
            Console.WriteLine();
            Console.Write("Введите id изменяемого контакта (Enter - будет изменён последний): ");
            int id;
            string idStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                id = -1;
            }
            else if (!int.TryParse(idStr, out id))
            {
                Console.WriteLine("Это не номер.");
                goto inputId;
            }

            using (var db = new ContactsDB(dbName))
            {
                if (id == -1)
                    contact = db.Contacts.Last();
                else
                {
                    contact = db.Contacts.Find(id);
                    if (contact == null)
                    {
                        Console.WriteLine("Контакта с таким Id нет.");
                        goto inputId;
                    }
                }
                Console.Write("Введите имя для изменяемого контакта: ");
                contact.Name = Console.ReadLine();
                db.SaveChanges();
                contacts = db.Contacts.ToList();
            };
            Console.WriteLine(string.Join(Environment.NewLine, contacts.Select(c => $"{c.Id}: {c.Name}")));

            // Вывод с фильтрацией
            Console.WriteLine();
            List<PhoneEntity> phones;
            using (var db = new ContactsDB(dbName))
            {
                phones = db.Phones.Where(ph => ph.ContactId == 567).ToList();
                Console.Write("Пауза во время открытого ContactsDB....");
                Console.ReadLine();
            }

            Console.WriteLine(string.Join(Environment.NewLine, phones.Select(ph => $"{ph.ContactId}-{ph.Id} {ph.Title}: {ph.Number}")));
            Console.Write("Пауза перед завершением....");
            Console.ReadLine();

        }
    }
}
