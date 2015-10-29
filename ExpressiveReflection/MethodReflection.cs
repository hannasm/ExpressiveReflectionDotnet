using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveReflection
{
    public class MethodReflection
    {
        public MethodInfo From<T>(Expression<Func<T>> methodExpression)
        {
            var mthExpr = methodExpression.Body as MethodCallExpression;
            if (mthExpr != null) {
                return mthExpr.Method;
            }
            
            var bexp = methodExpression.Body as BinaryExpression;
            if (bexp != null && bexp.Method != null) {
                return bexp.Method;
            }

            var uexp = methodExpression.Body as UnaryExpression;
            if (uexp != null && uexp.Method != null) {
                return uexp.Method;
            }

            throw new InvalidExpressionException(
                "method reflection",
                methodExpression.Body,
                typeof(MethodCallExpression),
                typeof(BinaryExpression),
                typeof(UnaryExpression)
            );
        }

        public string NameOf<T>(Expression<Func<T>> methodExpression)
        {
            return From(methodExpression).Name;
        }
    }
}
