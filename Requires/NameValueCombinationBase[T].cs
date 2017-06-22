namespace Requires
{
    using System;

    public abstract class NameValueCombinationBase<T>
    {
        public NameValueCombinationBase(string name, T value)
        {
            _name = name ?? throw new ArgumentNullException("name");
            _value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public T Value
        {
            get { return _value; }
        }

        private readonly string _name;
        private readonly T _value;

    }
}