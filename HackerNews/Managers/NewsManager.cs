using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Extensions;
using HackerNews.Models;
using HackerNews.Services;

namespace HackerNews.Managers
{
    /// <summary>
    /// Responsible to Retrieve and build the list of posts. 
    /// </summary>
    public class NewsManager
    {
        private readonly NewsApiService apiService;

        public NewsManager()
        {
            apiService = new NewsApiService();
        }

        /// <summary>
        /// Responsible to retrieve the correct amount of posts from the news.
        /// Even if a few doesn't meet the requirement, we will continuous getting posts
        /// until we get the correct amount.
        /// </summary>
        /// <param name="qtdPosts">Quantity of Posts to retrieve</param>
        /// <returns>List of Posts</returns>
        public async Task<List<Post>> GetTopStoriesAsync(int qtdPosts)
        {
            var bestPosts = await apiService.GetBestPostsIdsAsync();
            if (bestPosts == null || bestPosts.Count == 0)
            {
                return null;
            }

            var posts = GetAllPosts(bestPosts, qtdPosts);
            return ConvertDtosToPosts(posts);
        }

        private IList<PostDto> GetAllPosts(List<int> bestPosts, int qtdPosts)
        {
            var bag = new ConcurrentBag<PostDto>();
            var position = 0;
            while (bag.Count < qtdPosts)
            {
                var qtdItems = qtdPosts - bag.Count;
                var posts = bestPosts.Skip(position).Take(qtdItems).ToList();
                position += qtdItems;

                posts.AsParallel()
                     .WithDegreeOfParallelism(10)
                     .ForAll(x => GetPost(x, bag));
            }
            
            return bag.ToArray();
        }

        private static List<Post> ConvertDtosToPosts(IList<PostDto> posts)
        {
            var postsToReturn = new List<Post>();
            var rank = 1;
            foreach (var post in posts)
            {
                var item = post.MapToPost();
                item.Rank = rank;
                rank++;
                postsToReturn.Add(item);
            }

            return postsToReturn;
        }
        
        /// <summary>
        /// Sync Method which is responsible to retrieve in data from 1 DTO.
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <param name="bag">Bag with the results</param>
        private void GetPost(int postId, ConcurrentBag<PostDto> bag)
        {
            var dto = apiService.GetStoryAsync(postId).Result;
            if (IsInvalid(dto))
            {
                return;
            }

            bag.Add(dto);
        }

        /// <summary>
        /// Do the validation for the requirements from the documentation.
        /// I Also added a validation if it's not Dead or Deleted
        /// </summary>
        /// <param name="dto"><see cref="PostDto"/> which will validated</param>
        /// <returns>True if is INVALID</returns>
        public bool IsInvalid(PostDto dto)
        {
            return dto == null ||
                   dto.Dead ||
                   dto.Deleted ||
                   !Uri.TryCreate(dto.Url, UriKind.Absolute, out _) || // the output should be discard.
                   string.IsNullOrWhiteSpace(dto.Title) ||
                   dto.Title.Length > 256 ||
                   string.IsNullOrWhiteSpace(dto.By) ||
                   dto.By.Length > 256 ||
                   dto.Descendants < 0 ||
                   dto.Score < 0;
        }
    }
}
