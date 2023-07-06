using System.ComponentModel.DataAnnotations;

namespace Deparamtes
{
    public record ContactsModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }
        [Key]
        public string email { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public int PhoneNumber { get; set; }

        public string Date { get; set; }

    }
}
