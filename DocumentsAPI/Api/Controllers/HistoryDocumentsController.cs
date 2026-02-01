using AutoMapper;
using DocumentsAPI.Api.DTO.Documents;
using DocumentsAPI.Api.Extensions;
using DocumentsAPI.Core.Documents.Interfaces;
using DocumentsAPI.Core.Documents.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryDocumentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentService _documentService;

        public HistoryDocumentsController(IMapper mapper, IDocumentService documentService)
        {
            _mapper = mapper;
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentListItemDTO>>> GetUserDocuments()
        {
            var userId = User.GetUserId();
            var documents = await _documentService.GetDocumentsAsync(userId, DocumentStatus.Expired);

            var result = _mapper.Map<List<DocumentListItemDTO>>(documents);
            return Ok(result);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DocumentDetailsDTO>> GetById(Guid id)
        {
            var userId = User.GetUserId();
            var document = await _documentService.GetDocumentAsync(id, userId);

            if (document is null)
                return NotFound();

            return Ok(_mapper.Map<DocumentDetailsDTO>(document));
        }
    }
}
