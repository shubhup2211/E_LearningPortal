using System.Text.RegularExpressions;

namespace ELearningPortal.Models.Helpers
{
    public class YouTubeHelper
    {
        // Kisi bhi YouTube URL format (watch?v=, youtu.be/, embed/) se sirf VideoId nikalta hai
        public static string ExtractVideoId(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return string.Empty;

            var match = Regex.Match(url,
                @"(?:youtube\.com\/(?:watch\?v=|embed\/)|youtu\.be\/)([a-zA-Z0-9_-]{11})");

            return match.Success ? match.Groups[1].Value : url; // agar sirf ID already diya ho toh wahi return
        }

        // Embed URL banata hai + enablejsapi=1 (JS se video end event pakadne ke liye zaroori)
        public static string GetEmbedUrl(string url)
        {
            string videoId = ExtractVideoId(url);
            return $"https://www.youtube.com/embed/{videoId}?enablejsapi=1&rel=0";
        }
    }
}
