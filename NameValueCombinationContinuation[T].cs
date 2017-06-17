namespace Requires
{
    using System;

    public class NameValueCombinationContinuation<T> : NameValueCombinationBase<T>
    {
        public NameValueCombinationContinuation(string name, T value)
            : base(name, value)
        {            
        }
        
    }
}