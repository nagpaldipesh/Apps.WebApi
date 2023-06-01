using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase {
        private IPageService _pageService;
        public PagesController(IPageService pageService) {
            _pageService = pageService;
        }

        [HttpGet]
        public async Task<IActionResult> FetchPages() {
            await _pageService.FetchPagesAsync();
            return Ok();
        }

        [HttpGet("Document")]
        public async Task<IActionResult> GetAttributesDocument() {
            await _pageService.GeneratePagesDocumentAsync();

            return Ok();
        }

    }
}
