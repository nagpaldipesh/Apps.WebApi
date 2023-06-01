using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using System.Diagnostics;
using WebApi.Documents;
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
            var attributes = await _attributeService.GetAttributesAsync();
            var filePath = "attributes.pdf";

            var document = new AttributeDocument(attributes);
            document.GeneratePdf(filePath);

            Process.Start("explorer.exe", filePath);

            return Ok();
        }
    }
}
