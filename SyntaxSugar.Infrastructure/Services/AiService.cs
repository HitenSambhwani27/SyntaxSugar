using Microsoft.Extensions.Configuration;
using SyntaxSugar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SyntaxSugar.Infrastructure.Services
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string GeminiApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent";
        public AiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["AiService:ApiKey"];
        }

        public Task<string> ClassifyProblem(string problemName, string problemDescription, string userDoubt)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetHintsAsync(string problemName, string problemDescription)
        {
            var prompt = $@"
                You are a helpful assistant for a competitive programming platform.
                Your task is to generate 3-4 progressive hints for a coding problem.
                The hints should guide the user towards the solution without giving it away directly.
                The hints should be ordered from a small nudge to a more direct suggestion.
                Return ONLY a JSON array of strings. Do not include any other text or markdown.
                Example format: [""Hint 1 text"", ""Hint 2 text"", ""Hint 3 text""]

                Problem Title: {problemName}
                Problem Description: {problemDescription}
            ";

            var responseText = await CallGeminiApiAsync(prompt);
            if (string.IsNullOrEmpty(responseText))
            {
                return new List<string> { "Sorry, hints could not be generated at this time." };
            }

            try
            {
                var hints = JsonSerializer.Deserialize<List<string>>(responseText);
                return hints ?? new List<string>();
            }
            catch (JsonException)
            {
                return new List<string> { responseText };
            }
        }

        private async Task<string?> CallGeminiApiAsync(string prompt)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("API key is not set.");
            }
            if (string.IsNullOrEmpty(prompt))
            {
                throw new ArgumentException("Prompt cannot be null or empty.", nameof(prompt));
            }
            var requestUrl = $"{GeminiApiUrl}?key={_apiKey}";

            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync(requestUrl, payload);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadFromJsonAsync<GeminiResponse>();
                return responseBody?.candidates?.FirstOrDefault()?.content?.parts?.FirstOrDefault()?.text;
            }
            catch (HttpRequestException e)
            {
                return null;
            }

        }





        public class GeminiResponse
        {
            public List<Candidate> candidates { get; set; }
        }

        public class Candidate
        {
            public Content content { get; set; }
        }

        public class Content
        {
            public List<Part> parts { get; set; }
        }

        public class Part
        {
            public string text { get; set; }
        }
    }
}
