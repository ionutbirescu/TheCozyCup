namespace TheCozyCup
{
    // Represents the aggregated financial and product data for a given period (e.g., a day).
    public class DailySummaryReport
    {
        // Simple DTO to hold results for the SalesReportingService to return.

        public decimal TotalRevenue { get; set; }
        public int TotalTransactions { get; set; }
        public decimal TotalDiscountsApplied { get; set; }
        public DateTime ReportDate { get; set; }

        // List of the most popular items
        public List<PopularItem> BestSellers { get; set; } = new List<PopularItem>();

        // Inner class to define the structure for a popular item entry
        public class PopularItem
        {
            public string ItemName { get; set; }
            public int TotalQuantitySold { get; set; }
        }
    }
}
