using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.CodeAnalysis.CSharp;
using TrackAll_BackEnd.HelperModels;
using TrackAll_BackEnd.Models;
using TrackAll_BackEnd.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackAll_BackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }
        // GET: api/<InventoryController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
           var result = await inventoryRepository.GetItem();
            if(result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/<InventoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemModel model)
        {
            if (model is null)
            {
                return BadRequest();
            }
            var result = await inventoryRepository.AddItem(model);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
            
        }

        // PUT api/<InventoryController>
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] ItemModel model)
        {
            if (model is null)
                return BadRequest();
            var result = await inventoryRepository.UpdateItem(model);
            if (result is null)
                return BadRequest();
            if(result.Succeeded)
                return Ok(result);
            return Ok(result.Errors);

        }

        // DELETE api/<InventoryController>
        [HttpDelete()]
        public async Task<IActionResult> Delete(ItemModel model)
        {
            if (model is null)
                return BadRequest();

            var result = await inventoryRepository.DeleteItem(model);
            
            if (result is null)
                return BadRequest();
            if (result.Succeeded)
                return Ok(result);
            return Ok(result.Errors);

        }
    }
}
