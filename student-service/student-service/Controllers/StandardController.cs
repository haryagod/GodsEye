using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using student_service.Models;
using student_service.Services;

namespace Standard_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandardController : ControllerBase
    {
      
            private readonly StandardService _standardService;

            public StandardController(StandardService standardService) =>
                _standardService = standardService;

            [HttpGet]
            public async Task<List<Standard>> Get() =>
                await _standardService.GetAsync();

            [HttpGet("{id:length(24)}")]
            public async Task<ActionResult<Standard>> Get(string id)
            {
                var Standard = await _standardService.GetAsync(id);

                if (Standard is null)
                {
                    return NotFound();
                }

                return Standard;
            }
            [HttpPost]
            public async Task<IActionResult> Post(Standard newStandard)
            {
                await _standardService.CreateAsync(newStandard);

                return CreatedAtAction(nameof(Get), new { id = newStandard.Id }, newStandard);
            }

            [HttpPut("{id:length(24)}")]
            public async Task<IActionResult> Update(string id, Standard updatedStandard)
            {
                var Standard = await _standardService.GetAsync(id);

                if (Standard is null)
                {
                    return NotFound();
                }

                updatedStandard.Id = Standard.Id;

                await _standardService.UpdateAsync(id, updatedStandard);

                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public async Task<IActionResult> Delete(string id)
            {
                var Standard = await _standardService.GetAsync(id);

                if (Standard is null || Standard.Id is null)
                {
                    return NotFound();
                }

                await _standardService.RemoveAsync(Standard.Id);

                return NoContent();
            }
        

    }
}
