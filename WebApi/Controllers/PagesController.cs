using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using System.Diagnostics;
using WebApi.Documents;
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
            await _pageService.FetchPages();
            return Ok();
        }

        [HttpGet("Document")]
        public async Task<IActionResult> GetAttributesDocument() {
            
            var pages = await _pageService.GetPages();
            var filePath = "pages.pdf";

            var document = new PagesDocument(pages);
            document.GeneratePdf(filePath);

            Process.Start("explorer.exe", filePath);
            return Ok();
        }

    }
}
