using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2 {
    public class DateUtils {
        public static DateTime ConvertDate(double date) {
            DateTime convertedDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            convertedDate = convertedDate.AddSeconds(date).ToLocalTime();
            return convertedDate;
        }
    }
}
