using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Refit;


namespace TotalTechApp.Services.ApiConsumer
{
    public class ParameterFormatter : IUrlParameterFormatter
    {
        static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, EnumMemberAttribute>> enumMemberCache
            = new ConcurrentDictionary<Type, ConcurrentDictionary<string, EnumMemberAttribute>>();

        public virtual string Format(object parameterValue, ParameterInfo parameterInfo)
        {
            var formatString = parameterInfo.GetCustomAttribute<QueryAttribute>(true)?.Format;
            EnumMemberAttribute enummember = null;
            if (parameterValue != null && parameterInfo.ParameterType.GetTypeInfo().IsEnum)
            {
                var cached = enumMemberCache.GetOrAdd(parameterInfo.ParameterType, t => new ConcurrentDictionary<string, EnumMemberAttribute>());
                enummember = cached.GetOrAdd(parameterValue.ToString(), val => parameterInfo.ParameterType.GetMember(val).First().GetCustomAttribute<EnumMemberAttribute>());
            }

            var formattedValue = parameterValue == null
                       ? null
                       : string.Format(CultureInfo.InvariantCulture,
                                       string.IsNullOrWhiteSpace(formatString)
                                           ? "{0}"
                                           : $"{{0:{formatString}}}",
                                       enummember?.Value ?? parameterValue);
            if (string.IsNullOrEmpty(formattedValue))
                return formattedValue;
            return char.ToLowerInvariant(formattedValue[0]) + formattedValue.Substring(1);
        }
    }
}

