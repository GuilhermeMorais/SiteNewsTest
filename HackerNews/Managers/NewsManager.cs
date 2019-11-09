using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<List<PostDto>> GetTopStoriesAsync(int qtdPosts)
        {
            var bestPosts = await apiService.GetBestPostsIdsAsync();
            if (bestPosts == null || bestPosts.Count == 0)
            {
                return null;
            }

            var posts = await GetPostsAsync(bestPosts, qtdPosts);
            return posts;
        }

        private async Task<List<PostDto>> GetPostsAsync(List<int> bestPosts, int qtdPosts)
        {
            var postIds = bestPosts.Take(qtdPosts);
            var result = new List<PostDto>();
            foreach (var id in postIds)
            {
                var post = await apiService.GetStoryAsync(id);
                result.Add(post);
            }

            return result;
        }
    }
}
