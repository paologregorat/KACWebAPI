using KVMWebAPI.Infrastructure.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace KVMWebAPI.Infrastructure.Utility.RequestFilters;

public enum FilterTypeEnum
{
    StringEqual,
    StringArrayEqual,
    IntEqual,
    IntArrayEqual,
    DateEqual,
    GuidEqual,
    GuidArrayEqual,
    StringStartWith,
    StringEndWith,
    StringContains,
    DateGreaterEqualThan,
    DateLessEqualThan,
    DateGreaterThan,
    DateLessThan,
    IntGreaterEqualThan,
    IntLessEqualThan,
    IntGreaterThan,
    IntLessThan,
    BoolEqual,
    EnumEqual
}
public class FilterAttribute : Attribute
{
    public FilterTypeEnum Type { get; set; }
}
public static class LinqExtensions
{

    private static PropertyInfo GetPropertyInfo(Type objType, string name)
    {
        var properties = objType.GetProperties();
        var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
        if (matchedProperty == null)
        {
            var textError = "GetPropertyInfo error, property not found: " + name;
            throw new Exception(textError, new Exception(textError));
        }

        return matchedProperty;
    }
    private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
    {
        var paramExpr = Expression.Parameter(objType);
        var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
        var expr = Expression.Lambda(propAccess, paramExpr);
        return expr;
    }


    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return query;
        }

        PropertyInfo propInfo = null;
        MethodInfo method = null;
        LambdaExpression expr = null;

        if (name.StartsWith("-"))
        {
            name = name.Substring(1);
            propInfo = GetPropertyInfo(typeof(T), name);
            expr = GetOrderExpression(typeof(T), propInfo);
            method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
        }
        else
        {
            propInfo = GetPropertyInfo(typeof(T), name);
            expr = GetOrderExpression(typeof(T), propInfo);
            method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
        }

        var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
        return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
    }



    public static IEnumerable<T> TakeNullable<T>(this IEnumerable<T> query, int? limit)
    {
        if (limit != default && limit.Value > 0)
        {
            return query.Take(limit.Value);
        }
        return query;
    }

    public static IEnumerable<T> SkipNullable<T>(this IEnumerable<T> query, int? offset)
    {
        if (offset != default && offset.Value > 0)
        {
            return query.Skip(offset.Value);
        }
        return query;
    }

    public static IEnumerable<T> Filter<T>(this IEnumerable<T> query, dynamic request)
    {
        if (request == null)
        {
            return query;
        }

        Type type = request.GetType();

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
                            query = query.Where(r => r.GetType().GetProperty(field.Name).GetValue(r).ToString().ToUpper() == valueString.ToUpper()).ToList();
                        }
                        break;
                    case FilterTypeEnum.IntEqual:
                        string? valueStr = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueStr != null)
                        {
                            query = query.Where(r => (int?)r.GetType().GetProperty(field.Name).GetValue(r) == Convert.ToInt32(valueStr)).ToList();
                        }
                        break;
                    case FilterTypeEnum.DateEqual:
                        string? varStrDE = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        DateTime? valueDate = null;
                        if (!string.IsNullOrEmpty(varStrDE))
                        {
                            valueDate = Convert.ToDateTime(varStrDE);
                        }
                        if (valueDate != null)
                        {
                            query = query.Where(r => (DateTime?)r.GetType().GetProperty(field.Name).GetValue(r) == valueDate).ToList();
                        }
                        break;
                    case FilterTypeEnum.GuidEqual:
                        //string? varStrG = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        //Guid? valueGuid = null;
                        //if (!string.IsNullOrEmpty(varStrG))
                        //{
                        //    valueGuid = new Guid(varStrG);
                        //}
                        //if (valueGuid != null)
                        var value = (request.GetType().GetProperty(field.Name).GetValue(request, null));
                        if  (value != null)
                        {
                            query = query.Where(r => (Guid?)r.GetType().GetProperty(field.Name).GetValue(r) == (Guid)value).ToList();
                        }
                        break;
                    case FilterTypeEnum.StringArrayEqual:
                        string? valueStringArray = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueStringArray != null)
                        {
                            query = query.Where(r => ((string[])r.GetType().GetProperty(field.Name).GetValue(r)).Contains(valueStringArray)).ToList();
                        }
                        break;
                    case FilterTypeEnum.IntArrayEqual:
                        int? valueIntArray = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueIntArray != null)
                        {
                            query = query.Where(r => ((int?[])r.GetType().GetProperty(field.Name).GetValue(r)).Contains(valueIntArray)).ToList();
                        }
                        break;
                    case FilterTypeEnum.GuidArrayEqual:
                        //string? varStrGA = request.GetType().GetProperty(field.Name).GetValue(request, null).ToString();
                        //Guid? valueGuidArray = null;
                        //if (!string.IsNullOrEmpty(varStrGA))
                        //{
                        //    valueGuid = new Guid(varStrGA);
                        //}
                        //if (valueGuidArray != null)
                        //{
                        //    query = query.Where(r => ((Guid?[])r.GetType().GetProperty(field.Name).GetValue(r)).Contains(valueGuidArray)).ToList();
                        //}
                        break;
                    case FilterTypeEnum.StringStartWith:
                        string? valueStringStartWith = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueStringStartWith != null)
                        {
                            query = query.Where(r => r.GetType().GetProperty(field.Name).GetValue(r).ToString().ToUpper().StartsWith(valueStringStartWith.ToUpper())).ToList();
                        }
                        break;
                    case FilterTypeEnum.StringEndWith:
                        string? valueStringEndWith = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueStringEndWith != null)
                        {
                            query = query.Where(r => r.GetType().GetProperty(field.Name).GetValue(r).ToString().ToUpper().EndsWith(valueStringEndWith.ToUpper())).ToList();
                        }
                        break;
                    case FilterTypeEnum.StringContains:
                        string? valueStringContains = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueStringContains != null)
                        {
                            query = query.Where(r => r.GetType().GetProperty(field.Name).GetValue(r).ToString().ToUpper().Contains(valueStringContains.ToUpper())).ToList();
                        }
                        break;
                    case FilterTypeEnum.DateGreaterEqualThan:
                        string? varStrDGEQ = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        DateTime? valueDateGreaterEqualThan = null;
                        if (!string.IsNullOrEmpty(varStrDGEQ))
                        {
                            valueDateGreaterEqualThan = Convert.ToDateTime(varStrDGEQ);
                        }
                        if (valueDateGreaterEqualThan != null)
                        {
                            var filedName = field.Name.Replace("MaggioreUgualeDi", "");
                            query = query.Where(r => (DateTime?)r.GetType().GetProperty(filedName).GetValue(r) >= valueDateGreaterEqualThan).ToList();
                        }
                        break;
                    case FilterTypeEnum.DateLessEqualThan:
                        string? varStrDLEQ = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        DateTime? valueDateLessEqualThan = null;
                        if (!string.IsNullOrEmpty(varStrDLEQ))
                        {
                            valueDateLessEqualThan = Convert.ToDateTime(varStrDLEQ);
                        }
                        if (valueDateLessEqualThan != null)
                        {
                            var filedName = field.Name.Replace("MinoreUgualeDi", "");
                            query = query.Where(r => (DateTime?)r.GetType().GetProperty(filedName).GetValue(r) <= valueDateLessEqualThan).ToList();
                        }
                        break;
                    case FilterTypeEnum.DateGreaterThan:
                        string? varStrGQ = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        DateTime? valueDateGreaterThan = null;
                        if (!string.IsNullOrEmpty(varStrGQ))
                        {
                            valueDateGreaterThan = Convert.ToDateTime(varStrGQ);
                        }
                        if (valueDateGreaterThan != null)
                        {
                            var filedName = field.Name.Replace("MaggioreDi", "");
                            query = query.Where(r => (DateTime?)r.GetType().GetProperty(filedName).GetValue(r) > valueDateGreaterThan).ToList();
                        }
                        break;
                    case FilterTypeEnum.DateLessThan:
                        string? varStrL = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        DateTime? valueDateLessThan = null;
                        if (!string.IsNullOrEmpty(varStrL))
                        {
                            valueDateLessThan = Convert.ToDateTime(varStrL);
                        }
                        if (valueDateLessThan != null)
                        {
                            var filedName = field.Name.Replace("MinoreDi", "");
                            query = query.Where(r => (DateTime?)r.GetType().GetProperty(filedName).GetValue(r) < valueDateLessThan).ToList();
                        }
                        break;
                    case FilterTypeEnum.IntGreaterEqualThan:
                        int? valueIntGreaterEqualThan = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueIntGreaterEqualThan != null)
                        {
                            query = query.Where(r => (int?)r.GetType().GetProperty(field.Name).GetValue(r) >= valueIntGreaterEqualThan).ToList();
                        }
                        break;
                    case FilterTypeEnum.IntLessEqualThan:
                        int? intDateLessEqualThan = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (intDateLessEqualThan != null)
                        {
                            query = query.Where(r => (int?)r.GetType().GetProperty(field.Name).GetValue(r) <= intDateLessEqualThan).ToList();
                        }
                        break;
                    case FilterTypeEnum.IntGreaterThan:
                        int? valueIntGreaterThan = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueIntGreaterThan != null)
                        {
                            query = query.Where(r => (int?)r.GetType().GetProperty(field.Name).GetValue(r) > valueIntGreaterThan).ToList();
                        }
                        break;
                    case FilterTypeEnum.IntLessThan:
                        int? valueIntLessThan = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (valueIntLessThan != null)
                        {
                            query = query.Where(r => (int?)r.GetType().GetProperty(field.Name).GetValue(r) < valueIntLessThan).ToList();
                        }
                        break;
                    case FilterTypeEnum.BoolEqual:
                        string? varStr = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        bool? valueBoolEqual = null;
                        if (!string.IsNullOrEmpty(varStr))
                        {
                            valueBoolEqual = Convert.ToBoolean(varStr);
                        }
                        if (valueBoolEqual != null)
                        {
                            query = query.Where(r => (bool?)r.GetType().GetProperty(field.Name).GetValue(r) == valueBoolEqual).ToList();
                        }
                        break;
                    case FilterTypeEnum.EnumEqual:
                        var val = request.GetType().GetProperty(field.Name).GetValue(request, null);
                        if (val != null)
                        {
                            query = query.Where(r => (int)r.GetType().GetProperty(field.Name).GetValue(r) == Convert.ToInt32(val));
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        return query;
    }

    public static IEnumerable<T> StringSearch<T>(this IEnumerable<T> query, dynamic request)
    {
        if (request == null)
        {
            return query;
        }

        Type type = typeof(T);

        var searchCriteria = new List<Expression<Func<T, bool>>>();
        foreach (var field in type.GetProperties())
        {
            object[] attribute = field.GetCustomAttributes(typeof(FilterSearchStringAttribute), true);
            if (attribute.Length > 0)
            {
                FilterSearchStringAttribute myAttribute = (FilterSearchStringAttribute)attribute[0];

                if (myAttribute.IsFilterActive)
                {
                    string? valueString = request.GetType().GetProperty("SearchString").GetValue(request, null);
                    if (!string.IsNullOrEmpty(valueString))
                    {
                        searchCriteria.Add(r => r.GetType().GetProperty(field.Name).GetValue(r).ToString().ToUpper().Contains(valueString.ToUpper()));
                    }
                }
            }
        }
        if (searchCriteria.Any())
        {
            Expression<Func<T, bool>> filterOfOrs = searchCriteria.OrTheseFiltersTogether();

            query = query.Where(filterOfOrs.Compile());
        }
        return query;
    }

    public static Expression<Func<T, bool>> OrTheseFiltersTogether<T>(
        this IEnumerable<Expression<Func<T, bool>>> filters)
    {
        Expression<Func<T, bool>> firstFilter = filters.FirstOrDefault();
        if (firstFilter == null)
        {
            Expression<Func<T, bool>> alwaysTrue = x => true;
            return alwaysTrue;
        }

        var body = firstFilter.Body;
        var param = firstFilter.Parameters.ToArray();
        foreach (var nextFilter in filters.Skip(1))
        {
            var nextBody = Expression.Invoke(nextFilter, param);
            body = Expression.OrElse(body, nextBody);
        }
        Expression<Func<T, bool>> result = Expression.Lambda<Func<T, bool>>(body, param);
        return result;
    }

    public static IQueryable<T> IncludeMany<T>(this DbSet<T> model, params string[] includes)  where T : class
    {
        var modelQuerable = model.AsQueryable();
        foreach (var value in includes)
        {
            modelQuerable = modelQuerable.Include(value);
        }
        return modelQuerable;
    }
}