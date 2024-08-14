using CsvHelper;
using System.Globalization;

namespace Airport_Ticket_Booking.Domin.CSV
{
    public class CSVRepo
    {
        public static List<T> LoadDataFromCSVFile<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader ,CultureInfo.InvariantCulture);
            return csv.GetRecords<T>().ToList();
        }

        public static void SaveDataToCSVFile<T>(string filePath, IEnumerable<T> records)
        {
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(records);
        }
    }
}
