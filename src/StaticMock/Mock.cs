﻿using System;
using System.Linq.Expressions;
using StaticMock.Helpers;
using StaticMock.Services;
using StaticMock.Services.Injection.Implementation;

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

            if (methodToReplace.ReturnType == typeof(void))
            {
                throw new Exception($"Can't use some features of this setup for void return. To Setup void method us {nameof(SetupVoid)} setup");
            }

            return new MockService(new InjectionServiceFactory(), methodToReplace, action);
        }

        public static IVoidMockService SetupVoid(Type type, string methodName, Action action)
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

            return new MockService(new InjectionServiceFactory(), methodToReplace, action);
        }

        public static void SetupDefault(Type type, string methodName, Action action)
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

            if (methodToReplace.ReturnType != typeof(void))
            {
                throw new Exception("Default setup supported only for void methods");
            }

            MockHelper.SetupDefault(methodToReplace, action);
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

            if (!(methodGetExpression.Body is MethodCallExpression methodExpression))
            {
                throw new Exception("Get expression not contains method to setup");
            }

            return new MockService(new InjectionServiceFactory(), methodExpression.Method, action);
        }

        public static IVoidMockService Setup(Expression<Action> methodGetExpression, Action action)
        {
            if (methodGetExpression == null)
            {
                throw new ArgumentNullException(nameof(methodGetExpression));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (!(methodGetExpression.Body is MethodCallExpression methodExpression))
            {
                throw new Exception("Get expression not contains method to setup");
            }

            return new MockService(new InjectionServiceFactory(), methodExpression.Method, action);
        }

        public static void SetupDefault(Expression<Action> methodGetExpression, Action action)
        {
            if (methodGetExpression == null)
            {
                throw new ArgumentNullException(nameof(methodGetExpression));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (!(methodGetExpression.Body is MethodCallExpression methodExpression))
            {
                throw new Exception("Get expression not contains method to setup");
            }

            var methodToReplace = methodExpression.Method;
            if (methodToReplace.ReturnType != typeof(void))
            {
                throw new Exception("Default setup supported only for void methods");
            }

            MockHelper.SetupDefault(methodExpression.Method, action);
        }
    }
}
