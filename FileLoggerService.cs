using System.Text.Json;

namespace TheCozyCup
{
    internal class FileLoggerService
    {
        private static readonly string LogFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "saleslog.json");

        public static FileLoggerService Instance { get; } = new FileLoggerService();

        private FileLoggerService()
        {
            // Ensure the file exists
            if (!File.Exists(LogFilePath))
            {
                File.WriteAllText(LogFilePath, string.Empty);
            }
        }

        // Append a new sale
        public async Task AppendTransactionAsync(SalesLogEntry entry)
        {
            if (entry == null) return;

            string jsonLine = JsonSerializer.Serialize(entry) + Environment.NewLine;
            await File.AppendAllTextAsync(LogFilePath, jsonLine);
        }

        // Read all sales
        public async Task<List<SalesLogEntry>> ReadAllTransactionsAsync()
        {
            var salesList = new List<SalesLogEntry>();

            if (!File.Exists(LogFilePath))
                return salesList;

            string[] lines = await File.ReadAllLinesAsync(LogFilePath);
            foreach (var line in lines)
            {
                try
                {
                    var entry = JsonSerializer.Deserialize<SalesLogEntry>(line);
                    if (entry != null) salesList.Add(entry);
                }
                catch
                {
                    // Ignore malformed lines
                }
            }

            return salesList;
        }
    }
}
