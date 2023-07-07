using Microsoft.AspNetCore.Mvc;
using Project3;
using Project3.Dtos;
using Project3.Models;
using Project3.Repositories;
using System.Data;
using System.Net;

namespace Project1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository contactRep;
        private readonly ILoginRepository loginRep;

        public ContactController(IContactRepository contactRep, ILoginRepository loginRep)
        {
            this.contactRep = contactRep;
            this.loginRep = loginRep;
        }
        // GET /contact
        [HttpGet]
        public   IEnumerable<ContactDto> GetItems()
        {
            var items = contactRep.GetItems().Select(item => item.AsContactDto());
            return items;
        }
        // GET /contact/{id}
        [HttpGet("{id}")]
        public  ActionResult<ContactDto> GetItem(int id)
        {
            var item = contactRep.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }
            return item.AsContactDto();
        }
        // POST /contact
        [HttpPost]
        public ActionResult<ContactDto> CreateItem(ContactLoginDto contactDto)
        {
            var login = new LoginModel() { Login = contactDto.Login, Password = contactDto.Password };
            int results = loginRep.Login(login);
            if (results == 0)
            {
                //not logged in or wrong credentials
                var statusCodeFail = HttpStatusCode.NotFound;
                var messageFail = "Not Logged In";
                return StatusCode((int)statusCodeFail, messageFail);
            }
            ContactModel item = new()
            {
                Name = contactDto.Name,
                Surname = contactDto.Surname,

                Email = contactDto.Email,

                Category = contactDto.Category,
                Other = contactDto.Other,

                SubCategory = contactDto.SubCategory,
                PhoneNumber = contactDto.PhoneNumber,
                Date = contactDto.Date
        };

            if(contactRep.CreateItem(item))
            {
                var statusCode = HttpStatusCode.OK;
                var message = "corectly added item";
                return StatusCode((int)statusCode, message);
            }
            else
            {
                var statusCode = HttpStatusCode.NoContent;
                var message = "coudlnt add item";
                return StatusCode((int)statusCode, message);
            }
        }
        // PUT /contact/{id}
        [HttpPut("{id}")]
        public  ActionResult UpdateItem(int id, ContactLoginDto contactDto)
        {
            var login = new LoginModel() { Login = contactDto.Login, Password = contactDto.Password };
            int results = loginRep.Login(login);
            if (results == 0)
            {
                //not logged in or wrong credentials
                var statusCodeFail = HttpStatusCode.NotFound;
                var messageFail = "Not Logged In";
                return StatusCode((int)statusCodeFail, messageFail);
            }
            var existingItem = contactRep.GetItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            ContactModel updatedItem = existingItem with
            {
                Name = contactDto.Name,
                Surname = contactDto.Surname,

                Email = contactDto.Email,

                Category = contactDto.Category,
                Other = contactDto.Other,
                SubCategory = contactDto.SubCategory,
                PhoneNumber = contactDto.PhoneNumber,
                Date = contactDto.Date
            };

            contactRep.UpdateItem(updatedItem);
            var statusCode = HttpStatusCode.OK;
            var message = "corectly updatek item";
            return StatusCode((int)statusCode, message);
        }
        // DELETE /contact/{id}
        [HttpDelete("{id}")]
        public  ActionResult DeleteItem(int id,LoginDto loginDto)
        {
            var login = new LoginModel() { Login = loginDto.Login, Password = loginDto.Password };
            int results = loginRep.Login(login);
            if (results == 0)
            {
                var statusCodeFail = HttpStatusCode.NotFound;
                var messageFail = "Not Logged In";
                return StatusCode((int)statusCodeFail, messageFail);
            }
            var existingItem = contactRep.GetItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            contactRep.DeleteItem(id);
            var statusCode = HttpStatusCode.OK;
            var message = "corectly removed";
            return StatusCode((int)statusCode, message);
        }
    }
}
