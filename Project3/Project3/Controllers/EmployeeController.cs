using Deparamtes;
using Microsoft.AspNetCore.Mvc;
using Project1.Dtos;
using Project1.Models;
using Project1.Repositories;

namespace Project1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository depratmentsRepository;

        public EmployeeController(IEmployeeRepository depratmentsRepository)
        {
            this.depratmentsRepository = depratmentsRepository;
        }
        [HttpGet]
        public IEnumerable<EmployeeModel> GetItemsAsync()
        {
            var items = depratmentsRepository.GetItems();
            return items;
        }
        [HttpGet("{id}")]
        public ActionResult<EmployeeDto> GetItemAsync(int id)
        {
            var item = depratmentsRepository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }
            return item.AsEmployeeDto();
        }
        [HttpPost]
        public ActionResult<EmployeeDto> CreateItemAsync(CreateEmplyeeDto departmentDto)
        {
            EmployeeModel item = new()
            {
                EmployeeName = departmentDto.EmployeeName,
                Department = departmentDto.Department,

                DateOfJoining = departmentDto.DateOfJoining,

                PhotoFileName = departmentDto.PhotoFileName,
            };

            depratmentsRepository.CreateItem(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsEmployeeDto());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(int id, CreateEmplyeeDto itemDto)
        {
            var existingItem = depratmentsRepository.GetItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            EmployeeModel updatedItem = existingItem with
            {
                EmployeeName = itemDto.EmployeeName,
                Department = itemDto.Department,

                DateOfJoining = itemDto.DateOfJoining,

                PhotoFileName = itemDto.PhotoFileName,
            };

            depratmentsRepository.UpdateItem(updatedItem);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var existingItem = depratmentsRepository.GetItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            depratmentsRepository.DeleteItem(id);

            return NoContent();
        }
    }
}
