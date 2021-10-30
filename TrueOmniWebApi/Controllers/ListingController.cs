using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TrueOmniWebApi.Models;

namespace TrueOmniWebApi.Controllers
{
    [ApiController]
    [Route("listing")]
    public class ListingController : ControllerBase
    {
        private readonly ILogger<ListingController> _logger;

        public ListingController(ILogger<ListingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<ListingDTO>> Get()
        {
            try
            {
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync("https://webservice.trueomni.com/json.aspx?domainid=2248&fn=listings");

                var listingDTO1 = JsonConvert.DeserializeObject<List<Listing>>(json)
                                            .GroupBy(x => new { x.ListingID, x.Company, x.Image_List, x.CategoryID })
                                            .Select(y =>
                                               new ListingDTO(y.First().ListingID, y.First().Company, y.First().Image_List?.Split("|").ToList())
                                            ).OrderBy(x => x.ListingID).ToList();

                var listingDTO2 = new List<ListingDTO>();

                listingDTO1.ForEach((item) =>
                {
                    listingDTO2.Add(new ListingDTO(item.ListingID, $"{item.Company} 2", item.Image_List));
                });

                listingDTO1.ForEach(c => { c.Company = $"{c.Company} 1"; });

                return listingDTO1.Concat(listingDTO2).OrderBy(x => x.ListingID).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }
    }
}
