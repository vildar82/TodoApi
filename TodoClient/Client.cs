using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TodoEntities;

namespace TodoClient
{
    public class Client
    {
        private readonly HttpClient _client;
        private string url = @"api\todo";

        public Client()
        {
            _client = new HttpClient {BaseAddress = new Uri(@"https://localhost:5001")};
        }

        public async Task<List<TodoItem>> GetAll()
        {
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<TodoItem>>();
            }

            return null;
        }

        public async Task<Uri> Add(TodoItem item)
        {
            var response = await _client.PostAsJsonAsync(url, item);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }
    }
}
