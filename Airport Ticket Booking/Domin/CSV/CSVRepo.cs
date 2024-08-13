using CsvHelper;
using System.Globalization;

namespace Airport_Ticket_Booking.Domin.CSV
{
    public static class CSVRepo
    {
        public static IEnumerable<T> LoadDataFromCSVFile<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader ,CultureInfo.InvariantCulture);
            return csv.GetRecords<T>().ToList();
        }

    }
}
