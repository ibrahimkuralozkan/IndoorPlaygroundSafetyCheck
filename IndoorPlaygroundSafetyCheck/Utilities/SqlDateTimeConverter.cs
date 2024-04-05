using System;

namespace IndoorPlaygroundSafetyCheck.Utilities
{
    public static class SqlDateTimeConverter
    {
        public static DateTime EnsureSqlDateTime(DateTime dateTime)
        {
            var minSqlDateTime = new DateTime(1753, 1, 1);
            var maxSqlDateTime = new DateTime(9999, 12, 31);

            if (dateTime < minSqlDateTime) return minSqlDateTime;
            if (dateTime > maxSqlDateTime) return maxSqlDateTime;

            return dateTime;
        }
    }
}
