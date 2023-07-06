namespace Project1.Dtos
{
    public record EmployeeDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}
