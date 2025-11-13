namespace TheCozyCup
{
    //Implemented as a Singleton for easy access to reporting functionality.
    public class SalesReportingService
    {
        private static readonly SalesReportingService _instance = new SalesReportingService();
        public static SalesReportingService Instance => _instance;

        private SalesReportingService() { }
        public DailySummaryReport GenerateSummary(IReadOnlyList<SalesLogEntry> allSales)
        {
            var report = new DailySummaryReport
            {
                ReportDate = DateTime.Today
            };

            if (allSales == null || !allSales.Any())
            {
                return report;
            }

            report.TotalRevenue = allSales.Sum(s => s.FinalTotal);

            report.TotalTransactions = allSales.Count();

            report.TotalDiscountsApplied = allSales.Sum(s => (s.Subtotal * s.DiscountPercentage));

            List<KeyValuePair<string, int>> allItemsSoldTemp = new List<KeyValuePair<string, int>>();

            foreach (var sale in allSales)
            {
                foreach (var item in sale.ItemsSold)
                {
                    allItemsSoldTemp.Add(item);
                }
            }

            var bestSellersGroup = allItemsSoldTemp
                .GroupBy(item => item.Key)
                .Select(group => new DailySummaryReport.PopularItem
                {
                    ItemName = group.Key,
                    TotalQuantitySold = group.Sum(item => item.Value)
                })
                .OrderByDescending(item => item.TotalQuantitySold)
                .Take(3);

            report.BestSellers = bestSellersGroup.ToList();

            return report;
        }
    }
}