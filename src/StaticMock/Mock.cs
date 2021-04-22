using System;
using System.Linq.Expressions;
using StaticMock.Services;

namespace StaticMock
{
    public static class Mock
    {
        public static IMockService Setup(Type type, string methodName, Action action)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var methodToReplace = type.GetMethod(methodName);

            if (methodToReplace == null)
            {
                throw new Exception($"Can't find methodGetExpression {methodName} of type {type.FullName}");
            }

            return new MockService(methodToReplace, action);
        }

        public static IMockService Setup<TValue>(Expression<Func<TValue>> methodGetExpression, Action action)
        {
            if (methodGetExpression == null)
            {
                throw new ArgumentNullException(nameof(methodGetExpression));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var methodExpression = methodGetExpression.Body;
            if (methodExpression.NodeType != ExpressionType.Call)
            {
                throw new Exception("Get expression not contains method to setup");
            }

            return new MockService(((MethodCallExpression) methodExpression).Method, action);
        }
    }
}
