using Deparamtes;
using Microsoft.AspNetCore.Mvc;
using Project1.Dtos;
using Project1.Models;
using Project1.Repositories;
using System.Data;
using System.Net;

namespace Project1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartamentRepository depratmentsRepository;

        public DepartmentController(IDepartamentRepository depratmentsRepository)
        {
            this.depratmentsRepository = depratmentsRepository;
        }
        [HttpGet]
        public   IEnumerable<ContactsModel> GetItemsAsync()
        {
            var items =  depratmentsRepository.GetItems();
            return items;
        }
        [HttpGet("{id}")]
        public  ActionResult<DepartmentDto> GetItemAsync(int id)
        {
            var item = depratmentsRepository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }
            return item.AsDepartmentDto();
        }
        [HttpPost]
        public ActionResult<DepartmentDto> CreateItemAsync(CreateDepartmentDto departmentDto)
        {
            ContactsModel item = new()
            {
                DepartmentName = departmentDto.DepartmentName
            };

            depratmentsRepository.CreateItem(item);

            var statusCode = HttpStatusCode.OK;
            var message = "corectly added item";
            return StatusCode((int)statusCode, message);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(int id, CreateDepartmentDto itemDto)
        {
            var existingItem = depratmentsRepository.GetItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            ContactsModel updatedItem = existingItem with
            {
                DepartmentName = itemDto.DepartmentName,
            };

            depratmentsRepository.UpdateItem(updatedItem);
            var statusCode = HttpStatusCode.OK;
            var message = "corectly updatek item";
            return StatusCode((int)statusCode, message);
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
            var statusCode = HttpStatusCode.OK;
            var message = "corectly removed";
            return StatusCode((int)statusCode, message);
        }
    }
}
