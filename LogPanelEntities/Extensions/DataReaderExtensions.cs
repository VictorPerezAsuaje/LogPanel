using System.Data;

namespace LogPanelEntities.Extensions;

internal static class DataReaderExtensions
{
    public static T GetValueType<T>(this IDataReader reader, string colName)
        => (T)reader[colName];
    public static T? TypeOrNull<T>(this IDataReader reader, string colName)
    {
        if (reader[colName] == DBNull.Value)
            return default(T);

        return (T)reader[colName];
    }

    public static DateTime? IfDefaultThenNull(this DateTime dateTime)
        => dateTime == default(DateTime) ? null : dateTime;

    public static DateTime? IfDefaultThenNull(this DateTime? dateTime)
        => dateTime == default(DateTime) ? null : dateTime;
}

