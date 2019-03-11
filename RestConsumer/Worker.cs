using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables.Core;
using HotelModel;
using Newtonsoft.Json;

namespace RestConsumer
{
    class Worker
    {
        private const string URI = "http://localhost:2265/api/Facility";

        public void Start()
        {
            TableOfFacilities();
            TableOfOneFacility(1);

            //Console.WriteLine(DeleteFacility(7));

            //Console.WriteLine(CreateFacility(new Facility()
            //{
            //    Hotel_No = 7,
            //    SwimmingPool = 'T',
            //    Bar = 'T',
            //    TableTennis = 'T',
            //    PoolTable = 'T',
            //    Restaurant = 'T'
            //}));

            //Console.WriteLine(UpdateFacility(6, new Facility()
            //{
            //    Hotel_No = 6,
            //    SwimmingPool = 'T',
            //    Bar = 'T',
            //    TableTennis = 'T',
            //    PoolTable = 'T',
            //    Restaurant = 'T'
            //}));
        }

        private void TableOfOneFacility(int hotelNo)
        {
            Facility facility = GetOneFacility(hotelNo);

            var table = new ConsoleTable("Hotel_No", "Swimming_Pool", "Bar", "Table_Tennis", "Pool_Table", "Restaurant");
            table.AddRow(facility.Hotel_No, facility.SwimmingPool, facility.Bar, facility.TableTennis, facility.PoolTable, facility.Restaurant);
            table.Write(Format.Alternative);
        }

        private void TableOfFacilities()
        {
            List<Facility> facilities = GetAllFacilities();

            var table = new ConsoleTable("Hotel_No", "Swimming_Pool", "Bar", "Table_Tennis", "Pool_Table", "Restaurant");
            foreach (Facility facility in facilities)
            {
                table.AddRow(facility.Hotel_No, facility.SwimmingPool, facility.Bar, facility.TableTennis, facility.PoolTable, facility.Restaurant);
            }
            table.Write(Format.Alternative);
        }

        private List<Facility> GetAllFacilities()
        {
            List<Facility> facilities = new List<Facility>();
            
            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                String jsonStr = resTask.Result;

                facilities = JsonConvert.DeserializeObject<List<Facility>>(jsonStr);
            }
            return facilities;
        }

        private Facility GetOneFacility(int id)
        {
            Facility facility = new Facility();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI + "/" + id);
                String jsonStr = resTask.Result;

                facility = JsonConvert.DeserializeObject<Facility>(jsonStr);
            }
            return facility;
        }
        private bool DeleteFacility(int id)
        {
            bool ok = true;

            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(URI + "/" + id);

                HttpResponseMessage resp = deleteAsync.Result;
                if (!resp.IsSuccessStatusCode){ok = false;}
            }
            return ok;
        }

        private bool CreateFacility(Facility facility)
        {
            bool ok = true;

            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(facility);
                StringContent content = new StringContent(jsonStr, Encoding.ASCII, "application/json");

                Task<HttpResponseMessage> postAsync = client.PostAsync(URI, content);

                HttpResponseMessage resp = postAsync.Result;
                if (resp.IsSuccessStatusCode){ok = false;}
            }
            return ok;
        }

        private bool UpdateFacility(int id, Facility facility)
        {
            bool ok = true;

            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(facility);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> putAsync = client.PutAsync(URI + "/" + id, content);

                HttpResponseMessage resp = putAsync.Result;
                if (resp.IsSuccessStatusCode){ok = false;}
            }
            return ok;
        }
    }
}
