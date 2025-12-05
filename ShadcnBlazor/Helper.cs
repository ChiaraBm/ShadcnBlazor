using System.Linq.Expressions;

namespace ShadcnBlazor;

internal static class Helper
{
    internal static string GetPropertyName<TGridItem>(Expression<Func<TGridItem, object>> field)
    {
        MemberExpression memberExpression = null;

        if (field.Body is MemberExpression member)
        {
            memberExpression = member;
        }
        
        else if (field.Body is UnaryExpression { NodeType: ExpressionType.Convert } unary)
        {
            memberExpression = (unary.Operand as MemberExpression)!;
        }

        if (memberExpression == null)
            throw new ArgumentException("Expression must be a member access", nameof(field));

        return memberExpression.Member.Name;
    }
}