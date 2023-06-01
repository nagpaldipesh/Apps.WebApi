using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase {
        private IAttributeService _attributeService;
        public AttributeController(IAttributeService attributeService) {
            _attributeService = attributeService;
        }

        [HttpGet()]
        public async Task<IActionResult> ReadAttributes() {
            await _attributeService.FetchAttributesAsync();
            
            return Ok();
        }

        [HttpGet("Document")]
        public async Task<IActionResult> GetAttributesDocument() {
            await _attributeService.GenerateAttributeDocumentAsync();

            return Ok();
        }
    }
}
