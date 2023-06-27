using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace ContactManagementSystem.Web.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static bool TryGetQueryString<T>(this NavigationManager navManager, string key, out T value)
        {
            var uri = navManager.ToAbsoluteUri(navManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
            {
                if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
                {
                    value = (T)(object)valueAsInt;
                    return true;
                }

                if (typeof(T) == typeof(string))
                {
                    value = (T)(object)valueFromQueryString.ToString();
                    return true;
                }

                if (typeof(T) == typeof(bool?) && bool.TryParse(valueFromQueryString, out var valueAsBool))
                {
                    value = (T)(object)valueAsBool;
                    return true;
                }

                if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
                {
                    value = (T)(object)valueAsDecimal;
                    return true;
                }

                if (typeof(T) == typeof(Guid?) && Guid.TryParse(valueFromQueryString, out var valueAsGuid))
                {
                    value = (T)(object)valueAsGuid;
                    return true;
                }

                if (typeof(T) == typeof(Nullable<DateTime>))
                {
                    DateTime valueAsDateTime;
                    if (DateTime.TryParse(valueFromQueryString, out valueAsDateTime))
                    {
                        value = (T)(object)valueAsDateTime;
                    }
                    else
                    {
                        value = (T)(object)null;
                    }

                    return true;
                }
            }

            value = default;
            return false;
        }

        public static bool TryGetQueryEnum<T>(this NavigationManager navManager, string key, out T? value) where T : struct
        {
            var uri = navManager.ToAbsoluteUri(navManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
            {
                if (Enum.TryParse<T>(valueFromQueryString, true, out var valueAsEnum))
                {
                    value = (T)(object)valueAsEnum;
                    return true;
                }
            }

            value = null;
            return false;
        }
    }
}