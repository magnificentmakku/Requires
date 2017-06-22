namespace Requires
{
    using System;

    public class NameValueCombinationInitial<T> : NameValueCombinationBase<T>
    {
        public NameValueCombinationInitial(string name, T value)
            : base(name, value)
        {            
        }
        
    }
}