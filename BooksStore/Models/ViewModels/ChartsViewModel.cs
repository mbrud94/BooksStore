using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models.ViewModels
{
    public class ChartsViewModel
    {
        public List<ChartData> CategoryData { get; set; }
        public List<ChartData> TopVisitedData { get; set; }
        public List<ChartData> BestsellersData { get; set; }
    }

    public class ChartData
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }

}
