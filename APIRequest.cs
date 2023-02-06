using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleAppOpenAIChat
{
    public class APIRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "text-davinci-003";

        [JsonPropertyName("prompt")]
        public string Question { get; set; } = "";

        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; } = 4000;

        [JsonPropertyName("temperature")]
        public float Accuracy { get; set; } = 0.6F;    // 0 = very accurate, 1 = not very accurate

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class APIResponse
    {
        [JsonPropertyName("choices")]
        public List<Answer>? Answers { get; set; }

        public override string ToString()
        {
            string answer = string.Empty;

            if (Answers != null && Answers.Any())
            {
                answer = string.Join(Environment.NewLine, Answers.Select(s => s.Text.Replace("\n\n", "\n")));
                if (answer.StartsWith("\n"))
                {
                    return answer.Substring(1);
                }
            }

            return answer;
        }

        public class Answer
        {
            [JsonPropertyName("text")]
            public string Text { get; set; } = string.Empty;
        }
    }


}
