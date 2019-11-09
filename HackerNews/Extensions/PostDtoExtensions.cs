using HackerNews.Models;

namespace HackerNews.Extensions
{
    public static class PostDtoExtensions
    {
        /// <summary>
        /// Convert a <see cref="PostDto"/> to a <see cref="Post"/>
        /// </summary>
        /// <param name="dto">Dto to be converted</param>
        /// <returns>
        /// New Object of type <see cref="Post"/>.
        /// <remarks>Can return null if Dto is null</remarks>
        /// </returns>
        public static Post MapToPost(this PostDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new Post
            {
                Author = dto.By,
                Comments = dto.Descendants,
                Points = dto.Score,
                Title = dto.Title,
                Uri = dto.Url
            };
        }
    }
}
