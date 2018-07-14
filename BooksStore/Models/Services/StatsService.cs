using BooksStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models.Services
{
    public class StatsService
    {
        private BooksStoreDBContext db = new BooksStoreDBContext();

        public ChartsViewModel GetChartsData()
        {
            ChartsViewModel chartsVM = new ChartsViewModel();
            GetCategoryChartsData(chartsVM);
            GetTopVisitedChartsData(chartsVM);
            GetBestsellers(chartsVM);
            return chartsVM;
        }

        private void GetCategoryChartsData(ChartsViewModel chartsVM)
        {
            chartsVM.CategoryData = new List<ChartData>();
            foreach(Category c in db.Categories)
            {
                ChartData data = new ChartData
                {
                    Name = c.Name,
                    Count = c.Books.Count
                };
                chartsVM.CategoryData.Add(data);
            }
        }

        private void GetTopVisitedChartsData(ChartsViewModel chartsVM)
        {
            chartsVM.TopVisitedData = new List<ChartData>();
            foreach(Book b in db.Books.OrderByDescending(b=>b.VisitCounter).Take(5))
            {
                ChartData data = new ChartData
                {
                    Name = b.Title,
                    Count = b.VisitCounter
                };
                chartsVM.TopVisitedData.Add(data);
            }
        }

        private void GetBestsellers(ChartsViewModel chartsVM)
        {
            chartsVM.BestsellersData = new List<ChartData>();
            var items = db.OrdersDetails.GroupBy(od => od.BookId)
                .Select(odg => new { Name = odg.FirstOrDefault().Book.Title, Count = odg.Sum(od => od.Count)})
                .OrderByDescending(i => i.Count).Take(5);
            foreach (var item in items)
            {
                ChartData data = new ChartData
                {
                    Name = item.Name,
                    Count = item.Count
                };
                chartsVM.BestsellersData.Add(data);
            }
        }
    }
}
