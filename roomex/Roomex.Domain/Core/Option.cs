using System;

namespace Roomex.Domain.Core
{
    /// <summary>
    /// Option idiom
    /// </summary>
    /// <typeparam name="T">Reference type</typeparam>
    public struct Option<T> where T:class
    {
        private readonly T _value;

        public Option(T value)
        {
            _value = value;
        }

        public bool IsSome { get { return _value != null; } }
        public bool IsNone { get { return _value == null; } }

        public static Option<T> None()
        {
            return new Option<T>(default(T));
        }

        public static Option<T> Some(T value)
        {
            if(value == null) throw new ArgumentNullException("value");

            return new Option<T>(value);
        }

        public T Value
        {
            get
            {
                if(IsNone) throw new NullReferenceException("Value is null");

                return _value;
            }
        }
    }
}
