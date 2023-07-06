using Microsoft.AspNetCore.Mvc;

namespace Project1.Models
{
    public record EmployeeModel 
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}
