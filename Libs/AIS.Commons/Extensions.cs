using Hero;
using Hero.Core.Commons;
using Hero.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AIS
{
    public static class Extensions
    {
        public static T GetAs<T>(this IConfiguration configuration, string key, T defaultValue = default)
        {
            var theT = typeof(T);
            var unType = Nullable.GetUnderlyingType(theT);
            if (!theT.IsEnum && !theT.IsPrimitive && !theT.Equals(typeof(string)))
            {
                if (unType.IsNull())
                    throw new NotSupportedException($"Type of '{theT.Name}' is not support");
                else if (!unType.IsEnum && !unType.IsPrimitive && !unType.Equals(typeof(string)))
                    throw new NotSupportedException($"Type of '{theT.Name}' is not support");
            }
            var value = configuration[key];
            if (value == null)
                return defaultValue;
            if (!unType.IsNull())
                return (T)Convert.ChangeType(value, unType);

            return (T)Convert.ChangeType(value, theT);
        }

        public static string ToResultJson<TResult>(this ICommandResult<TResult> result)
        {
            if (result.IsNull()) return null;
            return HeroSerializer.Serializer.Serialize(result);
        }

        public static async Task<string> ToResultJson<TResult>(this Task<ICommandResult<TResult>> result)
        {
            var res = await result;
            if (res.IsNull()) return null;
            return HeroSerializer.Serializer.Serialize(res);
        }

        public static string ToJson<T>(this T obj)
        {
            if (obj.IsNull()) return null;
            return HeroSerializer.Serializer.Serialize(obj);
        }

        public static T GetAs<T>(this IDictionary<string, object> dictionary, string key, T defaultValue = default)
        {
            if (!dictionary.ContainsKey(key)) return defaultValue;
            var obj = dictionary[key];
            if (obj == null) return defaultValue;
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static string ToJsonFromRebuildCommandResult<T>(this ICommandResult<T> result, string rawData)
        {
            ICommandResult<object> commandResult;

            if (result.Success)
            {
                commandResult = new CommandResult<object>(result.Success)
                {
                    AdditionalInfo = result.AdditionalInfo,
                    Result = HeroSerializer.Serializer.Deserialize(rawData ?? "")
                };
            }
            else
            {
                commandResult = new CommandResult<object>(result.Ex)
                {
                    AdditionalInfo = result.AdditionalInfo
                };
            }

            return commandResult.ToJson();
        }

        public static int IntervalDays(this DateTime date1, DateTime date2)
        {
            return (int)(date2.Date - date1.Date).TotalDays;
        }

        public static IEnumerable<Exception> FlattenExceptions(this Exception ex)
        {
            IEnumerable<Exception> exceptions;
            if (ex is AggregateException exception)
            {
                exceptions = exception.InnerExceptions.ToArray();
            }
            else
            {
                var col = new HashSet<Exception>
                {
                    new Exception(ex.Message)
                };

                Action<Exception> fn = null;

                fn = (e) =>
                {
                    col.Add(e);
                    if (!e.InnerException.IsNull())
                        fn(e.InnerException);
                };

                fn(ex.InnerException);

                exceptions = col.ToArray();

                col.Clear();
            }

            return exceptions;
        }

        public static decimal ParseToDecimal(this string s, decimal @default = default)
        {
            if (s.IsNullOrEmptyOrWhitespace()) return 0;
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal dec))
            {
                return dec;
            }
            return @default;
        }

        public static bool IsValidTime(this string s)
        {
            return TimeSpan.TryParse(s, CultureInfo.InvariantCulture, out _);
        }

        public static TimeSpan ToTimeFormat(this string str, TimeSpan? @default = default)
        {
            if (TimeSpan.TryParse(str, out var ts))
            {
                return ts;
            }
            return (@default ?? TimeSpan.Zero);
        }
    }
}