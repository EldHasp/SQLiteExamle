using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemDataModel
{
    public class PhonesModel : Model<PhoneDto, PhoneEntity>
    {
        public PhonesModel(string source) : base(source)
        {
        }

        public IReadOnlyList<PhoneDto> GetAll(int id)
        {
            using var db = new ContactsDB(Source);
            return db
                .Phones
                .Where(ph => ph.ContactId == id)
                .ToList()
                .Select(en => en.Create())
                .ToList()
                .AsReadOnly();
        }
    }
}
