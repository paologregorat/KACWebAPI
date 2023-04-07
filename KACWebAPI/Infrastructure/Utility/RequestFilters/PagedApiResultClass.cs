using KVMWebAPI.Infrastructure.Utility.RequestFilters;
using System.Dynamic;

namespace KVMWebAPI.Infrastructure.Utility.RequestFilters;

public class GetSortPaged
{
    public string? Sort { get; set; }
    public int? Limit { get; set; }
    public int? OffSet { get; set; }
    public string? SearchString { get; set; }
}
public class ApiResults<T> where T : class
{
    public class PagedResultClass
    {
        public int? Count { get; set; }
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public string? Sort { get; set; }
        public string? SearchString { get; set; }
    }

    public class ResultsetClass
    {
        public dynamic Resultset { get; set; }
    }
    public class PagedApiResultClass
    {
        public ResultsetClass Metadata { get; set; }
        public IEnumerable<T> Results { get; set; }
    }

    public static PagedApiResultClass GetPagedApiResult(IEnumerable<T> result, int? count, int? offset, int? limit, string? sort, string? searchString)
    {

        var paged = new PagedResultClass()
        {
            Count = count,
            Offset = offset,
            Limit = limit,
            Sort = sort,
            SearchString = searchString
        };

        var resultset = new ResultsetClass()
        {
            Resultset = paged
        };

        var res = new PagedApiResultClass()
        {
            Metadata = resultset,
            Results = result
        };

        return res;
    }

    public static PagedApiResultClass GetPagedApiResult(IEnumerable<T> result, int? count, dynamic request)
    {

        Type type = request.GetType();

        dynamic myobject = new ExpandoObject();

        IDictionary<string, object> metadataResultset = myobject;
        metadataResultset.Add("Count", count);
        metadataResultset.Add("Offset", request.OffSet);
        metadataResultset.Add("Limit", request.Limit);
        metadataResultset.Add("Sort", request.Sort);
        metadataResultset.Add("SearchString", request.SearchString);

        foreach (var field in type.GetProperties())
        {
            object[] attribute = field.GetCustomAttributes(typeof(FilterAttribute), true);
            if (attribute.Length > 0)
            {
                FilterAttribute myAttribute = (FilterAttribute)attribute[0];
                FilterTypeEnum propertyValue = myAttribute.Type;

                switch (propertyValue)
                {
                    case FilterTypeEnum.StringEqual:
                        string? valueString = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueString != null)
                        {
                            metadataResultset.Add(field.Name, valueString);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }
                        break;
                    case FilterTypeEnum.IntEqual:
                        int? valueInt = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueInt != null)
                        {
                            metadataResultset.Add(field.Name, valueInt);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }
                        break;
                    case FilterTypeEnum.DateEqual:
                        string? varStrDE = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        DateTime? valueDate = null;
                        if (!String.IsNullOrEmpty(varStrDE))
                        {
                            valueDate = Convert.ToDateTime(varStrDE);
                        }

                        if (valueDate != null)
                        {
                            metadataResultset.Add(field.Name, valueDate);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }
                        break;
                    case FilterTypeEnum.GuidEqual:
                        var value = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        //Guid? valueGuid = null;
                        //if (!String.IsNullOrEmpty(varStrG))
                        //{
                        //    valueGuid = new Guid(varStrG);
                        //}

                        if (value != null)
                        {
                            metadataResultset.Add(field.Name, value);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.StringArrayEqual:
                        string? valueStringArray = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueStringArray != null)
                        {
                            metadataResultset.Add(field.Name, valueStringArray);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.IntArrayEqual:
                        int? valueIntArray = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueIntArray != null)
                        {
                            metadataResultset.Add(field.Name, valueIntArray);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.GuidArrayEqual:
                        //string? varStrGA = request.GetType().GetProperty(field.Name).GetValue(request, null).ToString();
                        //Guid? valueGuidArray = null;
                        //if (!String.IsNullOrEmpty(varStrGA))
                        //{
                        //    valueGuid = new Guid(varStrGA);
                        //}
                        //
                        //if (valueGuidArray != null)
                        //{
                        //    metadataResultset.Add(field.Name, valueGuidArray);
                        //}
                        //else
                        //{
                        //    metadataResultset.Add(field.Name, null);
                        //}

                        break;
                    case FilterTypeEnum.StringStartWith:
                        string? valueStringStartWith = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueStringStartWith != null)
                        {
                            metadataResultset.Add(field.Name, valueStringStartWith);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.StringEndWith:
                        string? valueStringEndWith = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueStringEndWith != null)
                        {
                            metadataResultset.Add(field.Name, valueStringEndWith);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.StringContains:
                        string? valueStringContains = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueStringContains != null)
                        {
                            metadataResultset.Add(field.Name, valueStringContains);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.DateGreaterEqualThan:
                        string? varStrDGEQ = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        DateTime? valueDateGreaterEqualThan = null;
                        if (!String.IsNullOrEmpty(varStrDGEQ))
                        {
                            valueDateGreaterEqualThan = Convert.ToDateTime(varStrDGEQ);
                        }
                        if (valueDateGreaterEqualThan != null)
                        {
                            metadataResultset.Add(field.Name, valueDateGreaterEqualThan);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.DateLessEqualThan:
                        string? varStrDLEQ = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        DateTime? valueDateLessEqualThan = null;
                        if (!String.IsNullOrEmpty(varStrDLEQ))
                        {
                            valueDateLessEqualThan = Convert.ToDateTime(varStrDLEQ);
                        }
                        if (valueDateLessEqualThan != null)
                        {
                            metadataResultset.Add(field.Name, valueDateLessEqualThan);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.DateGreaterThan:
                        string? varStrGQ = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        DateTime? valueDateGreaterThan = null;
                        if (!String.IsNullOrEmpty(varStrGQ))
                        {
                            valueDateGreaterThan = Convert.ToDateTime(varStrGQ);
                        }

                        if (valueDateGreaterThan != null)
                        {
                            metadataResultset.Add(field.Name, valueDateGreaterThan);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.DateLessThan:
                        string? varStrL = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        DateTime? valueDateLessThan = null;
                        if (!String.IsNullOrEmpty(varStrL))
                        {
                            valueDateLessThan = Convert.ToDateTime(varStrL);
                        }

                        if (valueDateLessThan != null)
                        {
                            metadataResultset.Add(field.Name, valueDateLessThan);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.IntGreaterEqualThan:
                        int? valueIntGreaterEqualThan = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueIntGreaterEqualThan != null)
                        {
                            metadataResultset.Add(field.Name, valueIntGreaterEqualThan);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.IntLessEqualThan:
                        int? intDateLessEqualThan = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (intDateLessEqualThan != null)
                        {
                            metadataResultset.Add(field.Name, intDateLessEqualThan);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.IntGreaterThan:
                        int? valueIntGreaterThan = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueIntGreaterThan != null)
                        {
                            metadataResultset.Add(field.Name, valueIntGreaterThan);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.IntLessThan:
                        int? valueIntLessThan = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueIntLessThan != null)
                        {
                            metadataResultset.Add(field.Name, valueIntLessThan);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    case FilterTypeEnum.BoolEqual:
                        string? varStr = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        bool? valueBoolEqual = null;
                        if (!String.IsNullOrEmpty(varStr))
                        {
                            valueBoolEqual = Convert.ToBoolean(varStr);
                        }
                        if (valueBoolEqual != null)
                        {
                            metadataResultset.Add(field.Name, valueBoolEqual);
                        }
                        else
                        {
                            metadataResultset.Add(field.Name, null);
                        }

                        break;
                    default:
                        break;
                }
            }
        }

        var resultset = new ResultsetClass()
        {
            Resultset = metadataResultset
        };

        var res = new PagedApiResultClass()
        {
            Metadata = resultset,
            Results = result
        };

        return res;
    }

}