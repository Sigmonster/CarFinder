using CarFinder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CarFinder.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*" )]
    [RoutePrefix("api/Cars")]
    public class CarController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        /////////////// CALLING SQL STORED PROCEDURES /////////////////
         
        ///<summary>
        ///Get list of all years in car database.
        ///</summary>
        ///<returns></returns>
        [Route("Years")]
        public async Task<List<string>> GetYears()
        {
            return await db.GetYears();
        }

        [Route("Makes")]
        public async Task<List<string>> GetMakes(string year)
        {
            return await db.GetMakes(year);
        }

        /// <summary>
        /// Get all models for a specified year and car make.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <returns></returns>
        [Route("Models")]
        public async Task<List<string>> GetModels(string year, string make)
        {
            return await db.GetModels(year, make);
        }

        [Route("Trims")]
        public async Task<List<string>> GetTrims(string year, string make, string model)
        {
            return await db.GetTrims(year, make, model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public async Task<List<Car>> GetCars(string year, string make, string model, string trim)
        {
            return await db.GetCars(year, make, model, trim);
        }


        //Code FA
        //##########
        [Route("Car")]
        public async Task<IHttpActionResult> getCarData(string year = "", string make = "", string model = "", string trim = "")
        {
            HttpResponseMessage response;
            //var content = new carRecall();
            //var singleCar = GetaCar(year, make, model, trim);
            var carsList = await GetCars(year, make, model, trim);
            var car = new carViewModel
            {
                Car = carsList.FirstOrDefault(),
                Recalls = "",
                Image = ""

            };


            //Get recall Data
            string result1 = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.nhtsa.gov/");
                try
                {
                    response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/" + year + "/make/"
                        + make + "/model/" + model.ToLower() + "?format=json");
                    result1 = await response.Content.ReadAsStringAsync();
                    car.Recalls = JsonConvert.DeserializeObject(result1);
                }
                catch (Exception)
                {
                    car.Recalls = null;
                }
            }



            //////////////////////////////   My Bing Search   //////////////////////////////////////////////////////////

            string query = year + " " + make + " " + model + " " + trim;

            string rootUri = "https://api.datamarket.azure.com/Bing/Search";

            var bingContainer = new Bing.BingSearchContainer(new Uri(rootUri));

            var accountKey = ConfigurationManager.AppSettings["searchKey"];

            bingContainer.Credentials = new NetworkCredential(accountKey, accountKey);


            var imageQuery = bingContainer.Image(query, null, null, null, null, null, null);

            var imageResults = imageQuery.Execute().ToList();


            car.Image = imageResults.First().MediaUrl;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            return Ok(car);

        }

    }
}
