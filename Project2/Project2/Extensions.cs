using Deparamtes;
using Project1.Dtos;
using Project1.Models;

namespace Project1
{
    public static class Extensions
    {
        public static DepartmentDto AsDepartmentDto(this DepartmentModel item)
        {
            return new DepartmentDto
            {
                Id = item.Id,
                DepartmentName = item.DepartmentName
            };
        }
        public static EmployeeDto AsEmployeeDto(this EmployeeModel item)
        {
            return new EmployeeDto
            {
                Id = item.Id,
                EmployeeName = item.EmployeeName,
                Department = item.Department,
                DateOfJoining = item.DateOfJoining,
                PhotoFileName = item.PhotoFileName,
            };
        }
    }
}
