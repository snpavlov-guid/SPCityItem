using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityServiceClient.Workers
{
    public static class CsvExportWorker
    {
         public static void ExportCsvFile<T>(TextWriter writer, IList<T> items, Func<T, string[]> getValues, Func<string[]> getHeader, string separ = ";")
        {
          
            var colls = 0;

            if (getHeader != null)
            {
                var headcols = getHeader();
                writer.WriteLine(GetCSVLine(headcols, separ));

                colls = headcols.Length;
            }

            foreach (var item in items)
            {
                var valuecols = getValues(item);

                if (colls == 0) colls = valuecols.Length;

                if (colls != valuecols.Length) throw new ApplicationException("The number of provided values dont' match!");

                writer.WriteLine(GetCSVLine(valuecols, separ));

            }

        }

        public static string GetCSVLine(string[] values, string separ)
        {
            const string quote = "\"";
            const string escapedQuote = "\"\"";

            Func<string, string> escapeValue = value =>
            {
                if (string.IsNullOrEmpty(value)) return string.Empty;
                if (value.Contains(quote)) value = value.Replace(quote, escapedQuote);
                if (value.IndexOf(separ) > 0) value = $"\"{value}\"";
                return value;
            };

            return string.Join(separ, values.Select(p => escapeValue(p)));
        }

    }
}
