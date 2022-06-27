//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace DY.Data.Mapping
//{
//    public class DataMapper
//    {
//        private static Lazy<DataRecordHelper> helper = new Lazy<DataRecordHelper>(() => new DataRecordHelper());

//        private DataMapper() { }

//        public static Func<IDataRecord, TDest> Map<TDest>(IDataReader dataReader)
//        {
//            var columnNames = dataReader.GetColumnNames();
//            var compiled = BuildExpression<TDest>(columnNames).Compile();
//            return compiled;
//        }

//        private static Expression<Func<IDataRecord, TDest>> BuildExpression<TDest>(string[] columns)
//        {
//            var type = typeof(TDest);
//            var parameter = Expression.Parameter(typeof(IDataRecord));
//            Expression body;
//            if (type.IsValueType || type.IsArray)
//            {
//                body = ValueExpression(parameter, Expression.Constant(0), type);
//            }
//            else
//            {
//                var destinationMembers = type.GetSettablePropList();
//                body = Expression.MemberInit(
//                    Expression.New(type),
//                    destinationMembers
//                        .Where(member => columns.Contains(member.Name))
//                        .Select(member => Expression.Bind(member, ValueExpression(parameter, Expression.Call(parameter, helper.Value.OrdinalMethod, Expression.Constant(member.Name)),
//                             member.PropertyType))
//                    ));
//            }
//            return Expression.Lambda<Func<IDataRecord, TDest>>(body, parameter);
//        }
//        public static IEnumerable<PropertyInfo> GetSettablePropList(this Type type)
//        {
//            return GetProps(type).Where(x => x.Value.Setter != null).Select(x => x.Value.Self);
//        }

//        private static ConditionalExpression ValueExpression(ParameterExpression parameter,
//            Expression ordinalExpression, Type type)
//        {
//            var isNullable = type.IsGenericType;
//            var propertType = (isNullable) ? type.GenericTypeArguments[0] : type;

//            Expression callExpression = Expression.Call(parameter, helper.Value.ValueMethod, ordinalExpression);
//            if (propertType.IsEnum)
//            {
//                callExpression = Expression.Call(
//                    typeof(Enum),
//                    "ToObject",
//                    null,
//                    Expression.Constant(propertType),
//                    callExpression);
//            }

//            return Expression.Condition(
//                                    Expression.Call(parameter, helper.Value.IsNullMethod, ordinalExpression),
//                                    Expression.Default(type),
//                                    Expression.Convert(callExpression, type),
//                                    type);
//        }
//    }
//}
