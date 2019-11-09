using System.Text.Json.Serialization;

namespace HackerNews.Models
{
    public class PostDto
    {
        /// <summary>
        /// true if the item is deleted.
        /// </summary>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

        /// <summary>
        /// The username of the item's author.
        /// </summary>
        [JsonPropertyName("by")]
        public string By { get; set; }

        /// <summary>
        /// true if the item is dead.
        /// </summary>
        [JsonPropertyName("dead")]
        public bool Dead { get; set; }

        /// <summary>
        /// The URL of the story.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// The story's score, or the votes for a pollopt.
        /// </summary>
        [JsonPropertyName("score")]
        public int Score { get; set; }
        
        /// <summary>
        /// The title of the story, poll or job.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// In the case of stories or polls, the total comment count.
        /// </summary>
        [JsonPropertyName("descendants")]
        public int Descendants { get; set; }
    }
}
