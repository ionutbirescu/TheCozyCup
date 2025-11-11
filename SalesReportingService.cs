using System;
using System.Collections.Generic;
using System.Linq;

namespace TheCozyCup
{
    // Analyzes raw sales log data using LINQ to generate comprehensive reports.
    //Implemented as a Singleton for easy access to reporting functionality.
    public class SalesReportingService
    {
        private static readonly SalesReportingService _instance = new SalesReportingService();

        // Singleton pattern
        public static SalesReportingService Instance => _instance;

        private SalesReportingService() { }

        // Generates a complete financial and product summary from the raw sales data.
        public DailySummaryReport GenerateSummary(IReadOnlyList<SalesLogEntry> allSales)
        {
            var report = new DailySummaryReport
            {
                ReportDate = DateTime.Today // Can be extended to filter by date range later
            };

            if (allSales == null || !allSales.Any())
            {
                // Return an empty report if no data exists
                return report;
            }


            // Calculates total revenue from all finalized sales (FinalAmount)
            report.TotalRevenue = allSales.Sum(s => s.FinalTotal);

            // Counts the number of transactions
            report.TotalTransactions = allSales.Count();

            // Calculates the total money given away in discounts
            report.TotalDiscountsApplied = allSales.Sum(s => (s.Subtotal * s.DiscountPercentage));


            
            //temporary holding area for all items sold
            List<KeyValuePair<string, int>> allItemsSoldTemp = new List<KeyValuePair<string, int>>();

            foreach (var sale in allSales)
            {
                foreach (var item in sale.ItemsSold)
                {
                    //adding each item to the temporary list
                    allItemsSoldTemp.Add(item);
                }
            }

            //performing the group by operation on the flattened list
            var bestSellersGroup = allItemsSoldTemp
                .GroupBy(item => item.Key) // Group by the item name (the dictionary key)
                .Select(group => new DailySummaryReport.PopularItem
                {
                    ItemName = group.Key,
                    // Sum the quantities (the dictionary value) within the group
                    TotalQuantitySold = group.Sum(item => item.Value)
                })
                .OrderByDescending(item => item.TotalQuantitySold) // Sort from most to least popular
                .Take(5); // Only grab the top 5 sellers for the report

            //materializing the query result into the BestSellers list
            report.BestSellers = bestSellersGroup.ToList();

            return report;
        }
    }
}