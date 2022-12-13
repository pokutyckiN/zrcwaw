using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using System.IO;
using System.Threading.Tasks;
using zrcwaw_l2.Models;

namespace zrcwaw_l2
{
    public class Polly
    {
        private static readonly IAmazonPolly _client;

        static Polly()
        {
            _client = new AmazonPollyClient(RegionEndpoint.USEast1);
        }

        public static async Task<Stream> Speak(SpeechData data)
        {
            var voice = await GetVoice(data.LanguageCode);

            SynthesizeSpeechRequest request = new()
            {
                Text = data.Text,
                VoiceId = voice.Id,
                TextType = TextType.Text,
                OutputFormat = OutputFormat.Mp3
            };

            SynthesizeSpeechResponse response = await _client.SynthesizeSpeechAsync(request);
            return response.AudioStream;
        }

        private static async Task<Voice> GetVoice(string languageCode)
        {
            DescribeVoicesRequest request = new()
            {
                LanguageCode = languageCode
            };

            DescribeVoicesResponse response = await _client.DescribeVoicesAsync(request);
            return response.Voices[0];
        }

    }
}
