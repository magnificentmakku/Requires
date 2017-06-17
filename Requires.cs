namespace Requires
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class Requires
    {
        public static NameValueCombinationContinuation<T> AndLessThan<T>(this NameValueCombinationContinuation<T> nameValueCombination, Expression<Func<T>> compareValueExpression)
        {
            if (nameValueCombination == null)
                throw new ArgumentNullException("nameValueCombination");
            if (compareValueExpression == null)
                throw new ArgumentNullException("compareValueExpression");

            var compareName = GetName(compareValueExpression);
            var compareValue = GetValue(compareValueExpression);

            var comparer = Comparer<T>.Default;

            if (!(comparer.Compare(nameValueCombination.Value, compareValue) < 0))
                throw new ArgumentOutOfRangeException(nameValueCombination.Name, nameValueCombination.Value, string.Format("\"{0}\" (actual value: {1}) must be less than \"{2}\" (actual value: {3}).", nameValueCombination.Name, nameValueCombination.Value, compareName, compareValue));
            
            return new NameValueCombinationContinuation<T>(nameValueCombination.Name, nameValueCombination.Value);
        }

        public static NameValueCombinationContinuation<T> GreaterThan<T>(this NameValueCombinationInitial<T> nameValueCombination, Expression<Func<T>> compareValueExpression)
        {
            if (nameValueCombination == null)
                throw new ArgumentNullException("nameValueCombination");
            if (compareValueExpression == null)
                throw new ArgumentNullException("compareValueExpression");

            var compareName = GetName(compareValueExpression);
            var compareValue = GetValue(compareValueExpression);

            var comparer = Comparer<T>.Default;

            if (!(comparer.Compare(nameValueCombination.Value, compareValue) > 0))
                throw new ArgumentOutOfRangeException(nameValueCombination.Name, nameValueCombination.Value, string.Format("\"{0}\" (actual value: {1}) must be greater than \"{2}\" (actual value: {3}).", nameValueCombination.Name, nameValueCombination.Value, compareName, compareValue));

            return new NameValueCombinationContinuation<T>(nameValueCombination.Name, nameValueCombination.Value);
        }

        public static void NotNull<T>(Expression<Func<T>> valueExpression)
            where T : class
        {
            if (valueExpression == null)
                throw new ArgumentNullException("valueExpression");

            var name = GetName(valueExpression);
            var value = GetValue(valueExpression);

            if (value == default(T))
                throw new ArgumentNullException(name);
        }

        public static void NotNullOrEmpty(Expression<Func<string>> stringExpression)
        {
            if (stringExpression == null)
                throw new ArgumentNullException("stringExpression");

            var name = GetName(stringExpression);
            var value = GetValue(stringExpression);

            if (value == null)
                throw new ArgumentNullException(name);
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Must not be String.Empty.", name);
        }

        public static NameValueCombinationInitial<T> Require<T>(Expression<Func<T>> valueExpression)
        {
            if (valueExpression == null)
                throw new ArgumentNullException("valueExpression");

            var name = GetName(valueExpression);
            var value = GetValue(valueExpression);

            return new NameValueCombinationInitial<T>(name, value);
        }

        private static string GetName<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return ((FieldInfo)((MemberExpression)expression.Body).Member).Name;
        }

        private static T GetValue<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return (expression.Compile())();
        }

    }
}