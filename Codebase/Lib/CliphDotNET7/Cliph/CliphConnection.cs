using System.Net.Http.Json;
using System.Text.Json;
using Cliph.Classes;
using Cliph.Classes.HTTP;

namespace Cliph
{
    public sealed class CliphConnection : IDisposable
    {
        private string? _apiKey = null;
        private bool _disposed = false;
        private HttpClient? _client = new HttpClient();

        public CliphConnection(string apiKey)
        {
            _apiKey = apiKey;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public async Task<Response<UserResponse>> CreateUser(User userData)
        {
            string apiUrl = $"{Constants.ApiUri}/api/v1/auth/account";

            var payload = new Dictionary<string, string>
            {
                {"email", userData.Email!},
                {"password", userData.Password!}
            };

            _client.DefaultRequestHeaders.Add("x-cliph-key", _apiKey);

            var content = JsonContent.Create(payload);
            var response = await _client.PostAsync(apiUrl, content);

            var responseString = await response.Content.ReadAsStringAsync();

            // TODO: fix deserialization
            // Response<UserResponse>? responseDeserializedContent = JsonSerializer.Deserialize<Response<UserResponse>>
            //     (responseString);
            //
            // if (responseDeserializedContent != null && responseDeserializedContent.Success)
            // {
            //     Console.WriteLine(responseDeserializedContent.Message);
            //     return responseDeserializedContent;
            // }

            return null;
        }

        public async Task<Response<UserResponse>> CreateSession(User userData)
        {
            string apiUrl = $"{Constants.ApiUri}/api/v1/auth/session";

            var payload = new Dictionary<string, string>
            {
                {"email", userData.Email!},
                {"password", userData.Password!}
            };

            _client.DefaultRequestHeaders.Add("x-cliph-key", _apiKey);

            var content = JsonContent.Create(payload);
            var response = await _client.PostAsync(apiUrl, content);

            var responseString = await response.Content.ReadAsStringAsync();

            // TODO: fix deserialization
            // Response<UserResponse>? responseDeserializedContent = JsonSerializer.Deserialize<Response<UserResponse>>
            //     (responseString);
            //
            // if (responseDeserializedContent != null && responseDeserializedContent.Success)
            // {
            //     Console.WriteLine(responseDeserializedContent.Message);
            //     return responseDeserializedContent;
            // }

            throw new Exception("Unable to create user account with the provided API key and user data.");
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _apiKey = null;
                _client = null;
            }

            _disposed = true;
        }

        ~CliphConnection()
        {
            Dispose(false);
        }
    }
}