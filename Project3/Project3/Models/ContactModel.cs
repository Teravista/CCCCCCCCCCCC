namespace Project3
{
    public record ContactModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }
        public string Email { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Other { get; set; }

        public int PhoneNumber { get; set; }

        public string Date { get; set; }

    }
}
