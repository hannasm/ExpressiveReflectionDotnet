using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveReflection
{
    public class ConstructorReflection
    {
        public ConstructorInfo From<T>(Expression<Func<T>> constructorExpression)
        {
            if (constructorExpression == null) {
                throw new ArgumentNullException("constructorExpression");
            }

            var newExpr = constructorExpression.Body as NewExpression;
            if (newExpr == null)
            {
                var listExpr = constructorExpression.Body as ListInitExpression;
                if (listExpr != null)
                {
                    newExpr = listExpr.NewExpression;
                }
                else
                {
                    var miExpr = constructorExpression.Body as MemberInitExpression;
                    if (miExpr != null) {
                        newExpr = miExpr.NewExpression;
                    } else {
                        var na = constructorExpression.Body as NewArrayExpression;
                        if (na != null)
                        {
                            return na.Type.GetConstructors()[0];
                        }
                    }
                }
            }
            if (newExpr != null)
            {
                return newExpr.Constructor;
            }

            throw new InvalidExpressionException(
                "constructor reflection", 
                constructorExpression.Body, 
                typeof(NewExpression),
                typeof(ListInitExpression),
                typeof(MemberInitExpression),
                typeof(NewArrayExpression)
            );            
        }
    }
}
