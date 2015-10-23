using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveReflection
{
    public class InvalidExpressionException : Exception
    {
        public InvalidExpressionException(string message, Expression expr, params Type[] options)
            : base(message +
                  (expr == null ? " received null expression " : " received expression of type " + expr.GetType().Name) +
                  (options == null ? "" : " Valid options include: " + string.Join(",", options.Select(o=>o.Name)))
               )
        {
        }
    }
}
