using System.Data.Entity;

namespace SystemDataModel
{
    public class ContactsDB : DbContext
    {
        public ContactsDB(string pathAndNameDB)
            : base(pathAndNameDB)
        {

            //PathAndNameDB = pathAndNameDB;
            //if (!File.Exists(pathAndNameDB))
            //{
            //    //string folder = Path.GetDirectoryName(pathAndNameDB);
            //    //if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
            //    //{
            //    //    DirectoryInfo directory = Directory.CreateDirectory(folder);
            //    //    pathAndNameDB = Path.Combine(directory.FullName, Path.GetFileName(pathAndNameDB));
            //    //    PathAndNameDB = pathAndNameDB;
            //    //}
            //    Database.EnsureCreated();
            //}
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlite($"Data Source={PathAndNameDB}");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ContactEntity>().HasData(GetDefaultContacts());
        //    modelBuilder.Entity<PhoneEntity>().HasData(GetDefaultPhones());

        //    base.OnModelCreating(modelBuilder);
        //}

        //private ContactEntity[] GetDefaultContacts()
        //{
        //    return new ContactEntity[]
        //    {
        //        new ContactEntity() {Id = 123, Name="Сергей (Exampl)"},
        //        new ContactEntity() {Id = 567, Name="Алексей (Exampl)"}
        //    };
        //}
        //private PhoneEntity[] GetDefaultPhones()
        //{
        //    return new PhoneEntity[]
        //    {
        //        new PhoneEntity() {Id = 9, ContactId = 123,  Title="Пожарная", Number="01"},
        //        new PhoneEntity() {Id = 23, ContactId = 567,  Title="Полиция", Number="02"},
        //        new PhoneEntity() {Id = 87, ContactId = 123,  Title="Скорая", Number="03"},
        //        new PhoneEntity() {Id = 54, ContactId = 567,  Title="МЧС", Number="112"}
        //    };
        //}

        //public string PathAndNameDB { get; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<PhoneEntity> Phones { get; set; }

    }

}
