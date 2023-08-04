using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unirest_net;
using unirest_net.request;
using unirest_net.http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Autoservisas_P3PL1_API
{
    class Operations
    {
        private static string ApiUrl = "http://localhost/api";
        public static async Task<bool> CheckLogin(string user, string password)
        {
            try
            {
                var userObj = new User() { UserName = user, Password = password };
                //get response from server
                var response = await Unirest.post($"{ApiUrl}/User/login.php")
                    .header("Accept", "application/json")
                    .body(JsonConvert.SerializeObject(userObj))
                    .asJsonAsync<string>();
                Console.WriteLine("=========================");
                Console.WriteLine(response.Body);
                Console.WriteLine("=========================");

                if (response.Code == 200)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Error: " + ex.StackTrace);
                return false;
            }
        }

        public static async Task<List<Client>> GetClientList()
        {
            try
            {
                //get response from server
                var response = await Unirest.get($"{ApiUrl}/Client/allclients.php")     //!!!
                    .header("Accept", "application/json")
                    .asJsonAsync<string>();
                Console.WriteLine("=========================");
                Console.WriteLine(response.Body);
                Console.WriteLine("=========================");

                if (response.Code == 200)
                    return JsonConvert.DeserializeObject<List<Client>>(response.Body);
                return new List<Client>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get Clients Error: " + ex.StackTrace);
                return new List<Client>();
            }
        }

        public static async Task<List<ClientCar>> GetClientCars(ulong clientID)
        {
            try
            {
                //get response from server
                var response = await Unirest.get($"{ApiUrl}/Client/clientcars.php?clientID={clientID}")     //!!!
                    .header("Accept", "application/json")
                    .asJsonAsync<string>();
                Console.WriteLine("=========================");
                Console.WriteLine(response.Body);
                Console.WriteLine("=========================");

                if (response.Code == 200)
                    return JsonConvert.DeserializeObject<List<ClientCar>>(response.Body);
                return new List<ClientCar>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get Clients Error: " + ex.StackTrace);
                return new List<ClientCar>();
            }
        }

        public static async Task<bool> DeleteClientCar(string license) 
        {
            try
            {
                //creating new json object \o/
                JObject obj = new JObject()
                {
                    new JProperty("licence",license)
                };
                //get response from server
                var response = await Unirest.post($"{ApiUrl}/Car/delete.php")
                    .header("Accept", "application/json")
                    .body(obj.ToString())
                    .asJsonAsync<string>();
                Console.WriteLine("=========================");
                Console.WriteLine(response.Body);
                Console.WriteLine("=========================");

                if (response.Code == 200)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Error: " + ex.StackTrace);
                return false;
            }
        }

        public static async Task<bool> CreateNewClientCar(ClientCar car, ulong clientID)
        {
            try
            {
                //creating new json object \o/
                JObject obj = new JObject()
                {
                    new JProperty("Name", car.Name),
                    new JProperty("Model", car.Model),
                    new JProperty("Year", car.Year),
                    new JProperty("Licence", car.Licence),
                    new JProperty("Engine", car.Engine),
                    new JProperty("Fuel", car.Fuel),
                    new JProperty("Power", car.Power),
                    new JProperty("ClientID", clientID)
                };
                //get response from server
                var response = await Unirest.post($"{ApiUrl}/Car/newcar.php")
                    .header("Accept", "application/json")
                    .body(obj.ToString())
                    .asJsonAsync<string>();
                Console.WriteLine("=========================");
                Console.WriteLine(response.Body);
                Console.WriteLine("=========================");

                if (response.Code == 200)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Error: " + ex.StackTrace);
                return false;
            }
        }
    }
}
