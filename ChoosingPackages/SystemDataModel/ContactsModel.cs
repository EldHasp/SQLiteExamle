using System;
using System.Linq;
using System.Threading.Tasks;

namespace SystemDataModel
{
    public class ContactsModel : Model<ContactDto, ContactEntity>
    {
        public ContactsModel(string source) : base(source)
        {
        }
    }
}
