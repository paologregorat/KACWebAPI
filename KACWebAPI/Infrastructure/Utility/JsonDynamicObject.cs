using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json;

namespace CQRSSAmple.Domain.Utility
{
    public class JsonDynamicObject : DynamicObject
    {
        public object RealObject { get; set; }

        static readonly JsonValueKind[] directObjectValues = new[] {
            JsonValueKind.False,
                JsonValueKind.True,
                JsonValueKind.Null,
                JsonValueKind.Number,
                JsonValueKind.String,
                JsonValueKind.Undefined
        };
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // Get the property value
            JsonElement srcData;

            if (RealObject == null ||
                !(RealObject is JsonElement) ||
                !((JsonElement)RealObject).TryGetProperty(binder.Name, out srcData))
            {
                result = null;
                return true;
            }

            result = GetValue(srcData);

            if (!(result is JsonDynamicObject))
            {
                result = new JsonDynamicObject
                {
                    RealObject = result
                };
            }

            // Always return true; other exceptions may have already been thrown if needed
            return true;
        }

        static object GetValue(object src)
        {

            object result = null;

            if (src is JsonElement)
            {
                var srcData = (JsonElement)src;
                switch (srcData.ValueKind)
                {
                    case JsonValueKind.Null:
                        result = null;
                        break;
                    case JsonValueKind.Number:
                        result = srcData.GetDouble();
                        break;
                    case JsonValueKind.False:
                        result = false;
                        break;
                    case JsonValueKind.True:
                        result = true;
                        break;
                    case JsonValueKind.Undefined:
                        result = null;
                        break;
                    case JsonValueKind.String:
                        result = srcData.GetString();
                        break;
                    case JsonValueKind.Object:
                        result = new JsonDynamicObject
                        {
                            RealObject = srcData
                        };
                        break;
                    case JsonValueKind.Array:
                        result = srcData.EnumerateArray()
                            .Select(o => directObjectValues.Contains(o.ValueKind) ?
                                (object)o :
                                (object)new JsonDynamicObject { RealObject = o })
                            .ToArray();
                        break;
                }
            }
            else
            {
                result = src;
            }

            return result;
        }

        public static explicit operator JsonDynamicObject[](JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value == null)
            {
                return null;
            }
            else if (value.GetType().IsArray)
            {
                return value is object[]?
                    ((object[])value).Select(s => (JsonDynamicObject)s).ToArray() :
                    (JsonDynamicObject[])value;
            }
            else
            {
                throw new InvalidOperationException($"Cannot convert value of type {value.GetType()} to array.");
            }
        }

        public static explicit operator Dictionary<string, string>(JsonDynamicObject src)
        {
            //Valid only for JsonElement RealObjects
            if (src == null || src.RealObject == null)
            {
                return null;
            }
            else if (src.RealObject is JsonElement && ((JsonElement)src.RealObject).ValueKind == JsonValueKind.Object)
            {
                return ((JsonElement)src.RealObject).EnumerateObject()
                    .ToDictionary(p => p.Name, p => GetValue(p.Value)?.ToString());
            }
            else
            {
                throw new InvalidOperationException($"Cannot convert value of type {src.GetType()} to dictionary.");
            }
        }

        public static explicit operator object[](JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value == null)
            {
                return null;
            }
            else if (value.GetType().IsArray)
            {
                return (object[])value;
            }
            else
            {
                throw new InvalidOperationException($"Cannot convert value of type {value.GetType()} to array.");
            }
        }

        public static explicit operator int[](JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value == null)
            {
                return null;
            }
            else if (value.GetType().IsArray)
            {
                return ((object[])value)
                    .Select(i => i is JsonElement ? Convert.ToInt32(GetValue(i)?.ToString()) : ((int)(double)i))
                    .ToArray();
            }
            else
            {
                throw new InvalidOperationException($"Cannot convert value of type {value.GetType()} to array.");
            }
        }

        public static explicit operator string[](JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value == null)
            {
                return null;
            }
            else if (value.GetType().IsArray)
            {
                return ((object[])value)
                    .Select(i => i is JsonElement ? GetValue(i)?.ToString() : (string)i)
                    .ToArray();
            }
            else
            {
                throw new InvalidOperationException($"Cannot convert value of type {value.GetType()} to array.");
            }
        }

        public static explicit operator Guid[](JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value == null)
            {
                return null;
            }
            else if (value.GetType().IsArray)
            {
                return ((object[])value)
                    .Select(i => new Guid(i is JsonElement ? GetValue(i)?.ToString() : (string)i))
                    .ToArray();
            }
            else
            {
                throw new InvalidOperationException($"Cannot convert value of type {value.GetType()} to array.");
            }
        }

        public static explicit operator int(JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value != null)
            {
                if (value is int)
                {
                    return (int)value;
                }
                else if (value is double)
                {
                    return Convert.ToInt32((double)value);
                }
                else if (value is string)
                {
                    return Convert.ToInt32(value);
                }
            }

            throw new ArgumentNullException("Value");
        }

        public static explicit operator int?(JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value != null)
            {
                if (value is int)
                {
                    return (int)value;
                }
                else if (value is double)
                {
                    return Convert.ToInt32((double)value);
                }
                else if (value is string)
                {
                    if (string.IsNullOrEmpty((string)value))
                    {
                        return null;
                    }
                    return Convert.ToInt32((string)value);
                }
            }

            return null;
        }

        public static explicit operator double(JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value != null)
            {
                if (value is int || value is double)
                {
                    return (double)value;
                }
                else if (value is string)
                {
                    return Convert.ToDouble(value);
                }
            }

            throw new ArgumentNullException("Value");
        }

        public static explicit operator double?(JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value != null)
            {
                if (value is double)
                {
                    return (double)value;
                }
                else if (value is string)
                {
                    if (string.IsNullOrEmpty((string)value))
                    {
                        return null;
                    }
                    return Convert.ToDouble((string)value);
                }
            }

            return null;
        }

        public static explicit operator DateTime(JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value != null && value is string)
            {
                return DateTime.Parse((string)value);
            }

            throw new ArgumentNullException("Value");
        }

        public static explicit operator DateTime?(JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value != null && value is string)
            {
                return DateTime.Parse((string)value);
            }

            return null;
        }

        public static explicit operator bool(JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value != null)
            {
                if (value is string)
                {
                    return bool.Parse((string)value);
                }
                else if (value is bool)
                {
                    return (bool)value;
                }
            }

            throw new ArgumentNullException("Value");
        }

        public static explicit operator bool?(JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value != null)
            {
                if (value is string)
                {
                    return bool.Parse((string)value);
                }
                else if (value is bool)
                {
                    return (bool)value;
                }
            }

            return null;
        }

        public static explicit operator Guid(JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value != null && value is string)
            {
                return Guid.Parse((string)value);
            }

            throw new ArgumentNullException("Value");
        }

        public static explicit operator Guid?(JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value != null && value is string)
            {
                return Guid.Parse((string)value);
            }

            return null;
        }

        public static explicit operator string(JsonDynamicObject src)
        {
            var value = GetValue(src.RealObject);
            if (value != null &&
                value is double &&
                Math.Abs(((double)value) % 1) <= (Double.Epsilon * 100))
            {
                value = Convert.ToInt32((double)value);
            }

            return value?.ToString();
        }
    }
}