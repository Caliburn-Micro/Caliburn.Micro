using System.Linq.Expressions;
using System.Reflection;

namespace Caliburn.Micro
{
    /// <summary>
    /// Extension for <see cref="Expression"/>.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Converts an expression into a <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="expression">The expression to convert.</param>
        /// <returns>The member info.</returns>
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression = (MemberExpression)(
                lambda.Body is UnaryExpression unaryExpression
                    ? unaryExpression.Operand
                    : lambda.Body);

            return memberExpression.Member;
        }
    }
}
