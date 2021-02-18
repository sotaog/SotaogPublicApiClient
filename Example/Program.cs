using System;
using SOTAOG.PublicApi;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string clientId = args[0];
            string clientSecret = args[1];
            Client client = new Client("https://api.sotaog.com");
            await client.Authenticate(clientId, clientSecret);

            // Fetch and print list of wells and their ids
            List<Well> wells = await client.GetWells();
            Console.WriteLine("Wells:");
            foreach (Well well in wells) {
                Console.WriteLine($"  {well.Id}\t {well.Label}");
            }

            string wellId = wells[0].Id;
            Dictionary<string, List<Datapoint>> datapoints = new Dictionary<string, List<Datapoint>>();
            List<Datapoint> tubingPressure = new List<Datapoint>();
            tubingPressure.Add(new Datapoint(1601377201000, 200));
            datapoints.Add("tubing-pressure-transmitter", tubingPressure);
            Console.WriteLine($"Uploading example datapoints for {wellId}");
            await client.PostDatapoints(wellId, datapoints);
        }
    }
}
