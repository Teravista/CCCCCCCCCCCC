namespace Project3.Models
{
    public record LoginModel
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }

    }
}
