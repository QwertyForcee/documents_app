using AutoMapper;
using DocumentsAPI.Api.DTO.Documents;
using DocumentsAPI.Api.Extensions;
using DocumentsAPI.Core.Documents.Interfaces;
using DocumentsAPI.Core.Documents.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsAPI.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentService _documentService;

        public DocumentsController(IMapper mapper, IDocumentService documentService)
        {
            _mapper = mapper;
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentListItemDTO>>> GetUserDocuments([FromQuery] DocumentStatus status)
        {
            var userId = User.GetUserId();
            var documents = await _documentService.GetDocumentsAsync(userId, status);

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

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateDocumentDTO dto)
        {
            var userId = User.GetUserId();
            var model = _mapper.Map<CreateDocumentModel>(dto);

            var documentId = await _documentService.CreateDocumentAsync(userId, model);

            return CreatedAtAction(
                nameof(GetById),
                new { id = documentId },
                documentId
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDocumentDTO dto)
        {
            var userId = User.GetUserId();
            var changesModel = _mapper.Map<DocumentChangesModel>(dto);

            await _documentService.UpdateDocumentAsync(id, userId, changesModel);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();
            await _documentService.DeleteDocumentAsync(id, userId);
            return NoContent();
        }

        [HttpPost("{id:guid}/copy")]
        public async Task<ActionResult<Guid?>> CopyDocument(Guid id)
        {
            var userId = User.GetUserId();
            return await _documentService.CopyAndDeleteDocument(id, userId);
        }
    }
}
