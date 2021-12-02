using SQLite.CodeFirst;
using System.Data.Entity;
using System.Data.SQLite;

namespace SystemDataModel
{

    public class EFConfiguration : DbConfiguration
    {
        public EFConfiguration()
        {
            SetProviderFactory("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", System.Data.SQLite.EF6.SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite", (System.Data.Entity.Core.Common.DbProviderServices)System.Data.SQLite.EF6.SQLiteProviderFactory.Instance.GetService(typeof(System.Data.Entity.Core.Common.DbProviderServices)));
        }
    }


    [DbConfigurationType(typeof(EFConfiguration))]
    public class ContactsDB : DbContext
    {
        private static SQLiteConnection SQLiteConnection => new SQLiteConnection()
        {
            ConnectionString = new SQLiteConnectionStringBuilder()
            {
                DataSource = @"C:\SQLite\contacs.db",

            }
                          .ConnectionString
        };
        private static readonly object locker = new object();
        public ContactsDB(string pathAndNameDB)
            : base(
                  SQLiteConnection,
                  true)
        {
            // Проверяется наличие базы.
            // Если базы нет,
            // то она автоматически создаётся и
            // заполняется данными для примера.
            lock (locker)
                if (!Database.Exists())
                {
                    Contacts.AddRange(new ContactEntity[]
                        {
                            new ContactEntity() {Id = 123, Name="Сергей (Exampl)"},
                            new ContactEntity() {Id = 567, Name="Алексей (Exampl)"}
                        });
                    Phones.AddRange(new PhoneEntity[]
                    {
                        new PhoneEntity() {Id = 9, ContactId = 123,  Title="Пожарная", Number="01"},
                        new PhoneEntity() {Id = 23, ContactId = 567,  Title="Полиция", Number="02"},
                        new PhoneEntity() {Id = 87, ContactId = 123,  Title="Скорая", Number="03"},
                        new PhoneEntity() {Id = 54, ContactId = 567,  Title="МЧС", Number="112"}
                    });

                    SaveChanges();
                }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ContactsDB>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<PhoneEntity> Phones { get; set; }
    }
}
