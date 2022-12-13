using Amazon;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace zrcwaw_l2
{
    public class Comprehend
    {
        private static readonly IAmazonComprehend _client;

        static Comprehend()
        {
            _client = new AmazonComprehendClient(RegionEndpoint.USEast1);
        }

        public static async Task<ICollection<DominantLanguage>> DetectLanguage(string text)
        {
            DetectDominantLanguageRequest request = new()
            {
                Text = text
            };

            DetectDominantLanguageResponse response = await _client.DetectDominantLanguageAsync(request);

            List<DominantLanguage> languages = new();
            response.Languages.ForEach(language => languages.Add(language));

            return languages;
        }
    }
}
