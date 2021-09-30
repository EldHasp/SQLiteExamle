using System;
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

            var contactsModel = new ContactsModel(dbName);

            // Получение всех контактов. 
            var contacts = contactsModel.GetAll();

            Console.WriteLine(string.Join(Environment.NewLine, contacts.Select(c => $"{c.Id}: {c.Name}")));

            // Добавление одного контакта
            Console.WriteLine();
            Console.Write("Введите имя для добавляемого контакта: ");
            var contact = new ContactDto(Console.ReadLine());
            contact = contactsModel.Add(contact);
            contacts = contactsModel.GetAll();
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
            else
            {
                var cnt = contactsModel.Get(id);
                if (cnt == null)
                {
                    Console.WriteLine("Контакта с таким Id нет.");
                    goto inputId;
                }
                contact = cnt;
            }

            Console.Write("Введите имя для изменяемого контакта: ");
            contactsModel.Change(contact, new ContactDto(Console.ReadLine()));
            contacts = contactsModel.GetAll();
            Console.WriteLine(string.Join(Environment.NewLine, contacts.Select(c => $"{c.Id}: {c.Name}")));

            // Вывод телефонов с фильтрацией
            Console.WriteLine();
            var phonesModel = new PhonesModel(dbName);
            var phones = phonesModel.GetAll(567);

            Console.WriteLine("Номера контакта 567.");
            Console.WriteLine(string.Join(Environment.NewLine, phones.Select(ph => $"{ph.ContactId}-{ph.Id} {ph.Title}: {ph.Number}")));
            Console.Write("Пауза перед завершением....");
            Console.ReadLine();

        }
    }
}
