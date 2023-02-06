using System.Text;
using System.Text.Json;

using ConsoleAppOpenAIChat;

internal class Program
{
    private const string API_KEY = "YOUR_API_KEY"; // goto https://platform.openai.com/account/api-keys to get a valid key

    private static void Main(string[] args)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization",$"Bearer {API_KEY}");

        Console.WriteLine("ChatGPT console");

        string line = string.Empty;
        while (Continue(line))
        {
            line = GetQuestion();
            if (!string.IsNullOrEmpty(line) && Continue(line))
            {
                var response = client.PostAsync("https://api.openai.com/v1/completions",
                               new StringContent(new APIRequest(){ Question = line }.ToJson(), Encoding.UTF8, "application/json")).Result;

                ShowAnswer(response.Content.ReadAsStringAsync().Result);
            }
        }
    }

    private static bool Continue(string line)
    {
        if (line.Equals("e", StringComparison.OrdinalIgnoreCase) ||
            line.Equals("end", StringComparison.OrdinalIgnoreCase) || 
            line.Equals("exit", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return true;
    }

    private static string GetQuestion()
    {
        Console.Write("Question: ");
        return Console.ReadLine() + string.Empty;
    }

    private static void ShowAnswer(string responseData)
    {
        var response = JsonSerializer.Deserialize<APIResponse>(responseData);
        if (response != null)
        {
            Console.WriteLine("Answer:   " + response.ToString());
        }
    }
}
