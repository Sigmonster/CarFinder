using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarFinder.Models
{

    public class carViewModel
    {
        public List<Car> Car { get; set; }
        public dynamic Recalls { get; set; }
        public string Image { get; set; }
    }

    public class carRecall
    {

        public string Count { get; set; }
        public string Message { get; set; }
        public List<recallResults> Results { get; set; }
    }

    public class recallResults
    {
        public string Manufacturer { get; set; }
        public string NHTSACampaignNumber { get; set; }
        public string ReportReceiveDate { get; set; }
        public string Component { get; set; }
        public string Summary { get; set; }
        public string Conequence { get; set; }
        public string Remedy { get; set; }
        public string Notes { get; set; }
        public string ModelYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }


}