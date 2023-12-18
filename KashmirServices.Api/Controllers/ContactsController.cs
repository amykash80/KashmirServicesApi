using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ApiController
    {
        private readonly IContactService service;

        public ContactsController(IContactService service)
        {
            this.service = service;
        }


        [HttpPost]
        public async Task<IResult> Post(ContactRequest model) => this.APIResult(await service.Add(model));


        [HttpGet]
        public async Task<IResult> GetAll() => this.APIResult(await service.GetAll());

       
        [HttpGet("{id:guid}")]
        public async Task<IResult> GetById([FromRoute] Guid id) => this.APIResult(await service.GetById(id));



        [HttpDelete("{id:guid}")]
        public async Task<IResult> Delete([FromRoute] Guid id) => this.APIResult(await service.Delete(id));
    }
}
