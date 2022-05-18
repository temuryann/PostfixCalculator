using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostfixCalculator
{
    internal class CustomStack<T> : IEnumerable<T>
    {
        private LinkedList<T> list = new LinkedList<T>();
        private int size;
        public int Size { get => size; }

        public void Push(T item)
        {
            if (item == null)
                throw new InvalidOperationException("Item is null !");

            list.AddLast(item);
            size++;
        }

        public T Pop()
        {
            if (size == 0)
                throw new InvalidOperationException("The stack is empty");

            T item = list.Last.Value;
            list.RemoveLast();
            size--;

            return item;
        }

        public T Peek()
        {
            if (size == 0)
                throw new InvalidOperationException("The stack is empty");

            return list.Last.Value;
        }

        public void Clear()
        {
            list.Clear();
            size = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator(); 
        }
    }
}
