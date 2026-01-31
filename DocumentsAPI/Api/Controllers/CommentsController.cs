using AutoMapper;
using DocumentsAPI.Api.DTO.Comments;
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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet("{documentId:guid}")]
        public async Task<ActionResult<List<CommentDTO>>> GetUserDocuments(Guid documentId)
        {
            var comments = await _commentService.GetCommentsAsync(documentId);

            var result = _mapper.Map<List<CommentDTO>>(comments);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCommentDTO dto)
        {
            var userId = User.GetUserId();
            var model = new CreateCommentModel(dto.Text, dto.DocumentId, userId);

            var newComment = await _commentService.CreateCommentAsync(model);

            var newCommentDTO = _mapper.Map<CommentDTO>(newComment);
            return Ok(newCommentDTO);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();
            await _commentService.DeleteCommentAsync(id, userId);
            return NoContent();
        }
    }
}
