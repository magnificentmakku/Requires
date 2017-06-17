namespace Requires
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class Requires
    {
        public static NameValueCombinationContinuation<int> AndLessThan(this NameValueCombinationContinuation<int> nameIntValueCombination, Expression<Func<int>> compareValueExpression)
        {
            if (nameIntValueCombination == null)
                throw new ArgumentNullException("nameIntValueCombination");
            if (compareValueExpression == null)
                throw new ArgumentNullException("compareValueExpression");

            var compareName = GetName(compareValueExpression);
            var compareValue = GetValue(compareValueExpression);

            if (!(nameIntValueCombination.Value < compareValue))
                throw new ArgumentOutOfRangeException(nameIntValueCombination.Name, nameIntValueCombination.Value, string.Format("\"{0}\" (actual value: {1}) must be less than \"{2}\" (actual value: {3}).", nameIntValueCombination.Name, nameIntValueCombination.Value, compareName, compareValue));

            return new NameValueCombinationContinuation<int>(nameIntValueCombination.Name, nameIntValueCombination.Value);
        }

        public static NameValueCombinationContinuation<int> GreaterThan(this NameValueCombinationInitial<int> nameIntValueCombination, Expression<Func<int>> compareValueExpression)
        {
            if (nameIntValueCombination == null)
                throw new ArgumentNullException("nameIntValueCombination");
            if (compareValueExpression == null)
                throw new ArgumentNullException("compareValueExpression");

            var compareName = GetName(compareValueExpression);
            var compareValue = GetValue(compareValueExpression);

            if (!(nameIntValueCombination.Value > compareValue))
                throw new ArgumentOutOfRangeException(nameIntValueCombination.Name, nameIntValueCombination.Value, string.Format("\"{0}\" (actual value: {1}) must be greater than \"{2}\" (actual value: {3}).", nameIntValueCombination.Name, nameIntValueCombination.Value, compareName, compareValue));

            return new NameValueCombinationContinuation<int>(nameIntValueCombination.Name, nameIntValueCombination.Value);
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