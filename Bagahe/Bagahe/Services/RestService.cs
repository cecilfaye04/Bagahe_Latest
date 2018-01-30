using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Bagahe.Entities;
using Bagahe.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using System.Reflection;
using System.Xml.Linq;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace Bagahe.Services
{
    public class RestService : IRestService
    {
        HttpClient client;

        private const string _passengerLoginURL = "http://172.26.82.69:5000/PassengerWebservice/login";
        private const string _bagTrackingURL = "http://172.26.82.69:5000/BagWebservice/tracking";
        private const string _bagGetListURL = "http://172.26.82.69:5000/BagWebservice/bags/";

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }

        public async Task<AuthenticateUserResponse> AuthenticateUser(AuthenticateUserInput input)
        {
            var items = new AuthenticateUserResponse();
            var restUrl = _passengerLoginURL;
            var uri = new Uri(string.Format(restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<AuthenticateUserResponse>(contents);
            }
                       
            return items;
        }

        public async Task<TrackResponse> TrackBagScanPoints(BagTrackUserInput input)
        {
            var items = new TrackResponse();
            var restUrl = _bagTrackingURL;
            var uri = new Uri(string.Format(restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<TrackResponse>(contents);
            }

            return items;
        }

        //public async Task<GetBagListReponse> GetBagList (BagTrackUserInput input)
        public async Task<GetBagListReponse> GetBagList(string PNR, string lastName)
        {

            var token = Convert.ToString(Application.Current.Properties["token"]);
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var items = new GetBagListReponse();
            //var restUrl = _bagGetListURL + input.LastName + "/" + input.PNR ;
            var restUrl = _bagGetListURL + lastName + "/" + PNR;
            var uri = new Uri(string.Format(restUrl, string.Empty));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<GetBagListReponse>(contents);
            }

            return items;
        }


        public async Task<string> Consume()
        {
            Uri uri = new Uri(_webConfig["actionUri"]);
            string json = JsonConvert.SerializeObject(_parameters, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, _webConfig["contentType"]);

            //to be optimized for token authorization
            //to add authorization header when calling the web service
            if (uri.ToString() != _passengerLoginURL)
            {
                var token = Convert.ToString(Application.Current.Properties["token"]);
                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }

            var response = await client.PostAsync(uri, content);
            var returnResponse = await response.Content.ReadAsStringAsync();

            return returnResponse;
        }

        private Dictionary<string, string> _parameters;
        public Dictionary<string, string> Parameters
        {
            set
            {
                _parameters = value;
            }
        }

        private Dictionary<string, string> _webConfig;

        public string WebMethod
        {
            set
            {
                _webConfig = new Dictionary<string, string>();
                Assembly assembly = typeof(CustomAppStart).GetTypeInfo().Assembly;

                using (var stream = assembly.GetManifestResourceStream("Bagahe.webapi.config"))
                using (var reader = new StreamReader(stream))
                {
                    var doc = XDocument.Parse(reader.ReadToEnd());
                    foreach (XElement xe in doc.Elements("Services").Elements("Service"))
                    {
                        if (xe.Attribute("Name").Value == value)
                        {
                            _webConfig.Add("method", xe.Element("Method").Value);
                            _webConfig.Add("methodUri", xe.Element("MethodURI").Value);
                            _webConfig.Add("actionUri", xe.Element("ActionURI").Value);
                            _webConfig.Add("contentType", xe.Element("ContentType").Value);
                        }
                    }
                    /// use the to update value of _webConfig like uri, method, content etc.
                    /// read the values from a local XML config
                }
            }
        }
    }
}
