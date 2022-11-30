using System.Text.Json.Serialization;
using testeLeadSoft.Models;

namespace testeLeadSoft.Dto.Comment
{
    public class CreateCommentDto
    {
        public string Text { get; set; }
        public Guid ArticleId { get; set; }
    }
}
