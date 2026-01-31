using AutoMapper;
using DocumentsAPI.Api.DTO.Comments;
using DocumentsAPI.Api.DTO.Documents;
using DocumentsAPI.Core.Documents.Models;

namespace DocumentsAPI.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateDocumentModel, CreateDocumentDTO>();
            CreateMap<CreateDocumentDTO, CreateDocumentModel>();

            CreateMap<DocumentChangesModel, UpdateDocumentDTO>();
            CreateMap<UpdateDocumentDTO, DocumentChangesModel>();

            CreateMap<Document, DocumentListItemDTO>();
            CreateMap<Document, DocumentDetailsDTO>();

            CreateMap<Comment, CommentDTO>();
        }
    }
}
