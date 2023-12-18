using Dapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Data;

namespace KashmirServices.Persistence.Data
{
    public class CustomTimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
    {
        public CustomTimeOnlyConverter() : base(
                timeOnly => timeOnly.ToTimeSpan(),
                timeSpan => TimeOnly.FromTimeSpan(timeSpan))
        {
        }
    }

    public class CustomTimeOnlyComparer : ValueComparer<TimeOnly>
    {
        public CustomTimeOnlyComparer() : base(
            (t1, t2) => t1.Ticks == t2.Ticks,
            t => t.GetHashCode())
        {
        }
    }

    public class SqlTimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
    {
        public override void SetValue(IDbDataParameter parameter, TimeOnly time)
        {
            parameter.Value = time.ToString();
        }

        public override TimeOnly Parse(object value)
        {
            return TimeOnly.FromTimeSpan((TimeSpan)value);
        }
    }
}
