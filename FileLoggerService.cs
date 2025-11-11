using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace TheCozyCup
{
    internal class FileLoggerService
    {
        // Handles asynchronous persistence of sales data to a file.
        // Uses async/await to prevent the UI from freezing during disk I/O.

        private const string LogFilePath = "saleslog.json";
        private static readonly FileLoggerService ins = new FileLoggerService();

        public static FileLoggerService Instance
        {
            get { return ins; }
        }

        private FileLoggerService()
        {
            // Private constructor to enforce singleton pattern
            if (!File.Exists(LogFilePath))
            {
                // Create the log file if it doesn't exist
                using (var fs = File.Create(LogFilePath))
                {
                    File.WriteAllText(LogFilePath, "[]");
                }
            }
        }

        //Appends a single sales log entry to the log file asynchronously
        public async Task AppendTransactionAsync(SalesLogEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry), "Sales log entry cannot be null.");
            }
            try
            {
                var jsonLine = JsonSerializer.Serialize(entry) + Environment.NewLine;
                await File.AppendAllTextAsync(LogFilePath, jsonLine);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new IOException("Failed to append transaction to log file.", ex);
            }
        }

        //Reads all sales log entries from the log file asynchronously
        public async Task<List<SalesLogEntry>> ReadAllTransactionsAsync()
        {
            var salesList = new List<SalesLogEntry>();
            if (!File.Exists(LogFilePath))
            {
                return salesList;
            }
            try
            {
                var logContent = await File.ReadAllTextAsync(LogFilePath);
                var jsonLines = logContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in jsonLines)
                {
                    try
                    {
                        var entry = JsonSerializer.Deserialize<SalesLogEntry>(line);
                        if (entry != null)
                        {
                            salesList.Add(entry);
                        }

                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"JSON DESERIALIZATION ERROR on line: {line}. {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new IOException("Failed to read transactions from log file.", ex);
            }
            return salesList.AsReadOnly().ToList();
        }
    }
}