using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Linq.Expressions;
using global::System.Reflection;
using global::System.Text;
using global::System.Threading.Tasks;

namespace ExpressiveReflection
{
#if EXPRESSIVE_REFLECTION_ASSEMBLY
    public
#endif
    class ConstructorReflection
    {
        public ConstructorInfo Transmute(ConstructorInfo other, params Type[] newGenericArgs)
        {
            // you can't transmute a non-generic type's constructor because there are no generic arguments
            // to change
            if (!other.DeclaringType.IsGenericType)
            {
                return other;
            }


            var index = other.DeclaringType.GetConstructors().Select((c, i) => new { c, i }).Where(d => d.c == other).Select(d => d.i).Single();
            var type = other.DeclaringType.GetGenericTypeDefinition().MakeGenericType(newGenericArgs);
            var transmuted = type.GetConstructors()[index];
            return transmuted;
        }
        
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
