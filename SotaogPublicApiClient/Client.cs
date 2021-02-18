using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SOTAOG.PublicApi
{
    public class Client
    {
        private readonly HttpClient client = new HttpClient();
        private string baseUrl;
        private JWT token;
        public Client(string url) {
            baseUrl = url.TrimEnd('/');
        }

        public async Task Authenticate(string clientId, string clientSecret) {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string authenticateUrl = $"{baseUrl}/v1/authenticate";
            Dictionary<string, string> requestContent = new Dictionary<string, string>();
            requestContent.Add("grant_type", "client_credentials");

            // basic authentication
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, authenticateUrl);
            string basicAuthHash = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));
            request.Headers.Authorization = new AuthenticationHeaderValue("BASIC", basicAuthHash);
            request.Content = new FormUrlEncodedContent(requestContent);
            var res = await client.SendAsync(request);
            string responseContent = await res.Content.ReadAsStringAsync();
            if (res.StatusCode == HttpStatusCode.OK) {
                token = JsonConvert.DeserializeObject<JWT>(responseContent);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            } else {
                throw new ClientException(responseContent);
            }
        }

        public async Task<List<Well>> GetWells() {
            string url = $"{baseUrl}/v1/wells";
            var res = await client.GetAsync(url);
            string responseContent = await res.Content.ReadAsStringAsync();
            if (res.StatusCode == HttpStatusCode.OK) {
                List<Well> wells = JsonConvert.DeserializeObject<List<Well>>(responseContent);
                return wells;
            } else {
                throw new ClientException(responseContent);
            }
        }

        public async Task PostDatapoints(string entityId, Dictionary<string, List<Datapoint>> datatypeDatapoints) {
            string url = $"{baseUrl}/v1/datapoints/{entityId}";
            string json = JsonConvert.SerializeObject(datatypeDatapoints);
            HttpResponseMessage res = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            string responseContent = await res.Content.ReadAsStringAsync();
            if (res.StatusCode != HttpStatusCode.Accepted) {
                throw new ClientException(responseContent);
            }
        }
    }
}
