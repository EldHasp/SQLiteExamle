namespace SystemDataModel
{
    public class ContactDto : IdDto
    {
        public string Name { get; }

        public ContactDto(string name)
        {
            Name = name;
        }

        internal ContactDto(int id, string name) : base(id)
        {
            Name = name;
        }
    }

}
