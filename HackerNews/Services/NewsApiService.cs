using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HackerNews.Models;

namespace HackerNews.Services
{
    /// <summary>
    /// Service responsible to retrieve all the information from Hacker News
    /// </summary>
    public class NewsApiService
    {
        private readonly HttpClient httpClient;
        private readonly string bestPostsUrl = "https://hacker-news.firebaseio.com/v0/beststories.json?print=pretty";
        private readonly string storyDetailUrl = "https://hacker-news.firebaseio.com/v0/item/{0}.json?print=pretty";

        public NewsApiService()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Retrieve all the 200 best Stories from Hacker News.
        /// </summary>
        /// <returns>List of Ids from Hacker news.</returns>
        public async Task<List<int>> GetBestPostsIdsAsync()
        {
            var message = await GetBodyMessageAsync(bestPostsUrl);
            if (string.IsNullOrWhiteSpace(message))
            {
                return new List<int>();
            }

            var result = JsonSerializer.Deserialize<List<int>>(message);
            return result;
        }
        
        /// <summary>
        /// Retrieve a post from Hacker News.
        /// </summary>
        /// <param name="id">Id of the post</param>
        /// <returns>DTO with the minimal necessary information</returns>
        public async Task<PostDto> GetStoryAsync(int id)
        {
            var message = await GetBodyMessageAsync(string.Format(storyDetailUrl, id));
            if (string.IsNullOrWhiteSpace(message))
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<PostDto>(message);
            return result;
        }

        private async Task<string> GetBodyMessageAsync(string url)
        {
            try
            {
                var responseMessage = await httpClient.GetAsync(url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return await responseMessage.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Problem with the request to {url} => {ex.GetBaseException().Message}");
            }

            return string.Empty;
        }
    }
}
