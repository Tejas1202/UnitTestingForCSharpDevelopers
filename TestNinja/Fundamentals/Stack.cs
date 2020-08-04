using System;
using System.Collections.Generic;

namespace TestNinja.Fundamentals
{
    public class Stack<T>
    {
        private readonly List<T> _list = new List<T>();

        public int Count => _list.Count;

        public void Push(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            _list.Add(obj);
        }

        public T Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException();

            var index = _list.Count - 1;
            var result = _list[index];
            _list.RemoveAt(index);

            return result;
        }

        public T Peek()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException();

            return _list[_list.Count - 1];
        }
    }
}
