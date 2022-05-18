using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostfixCalculator
{
    internal class CustomQueue<T> : IEnumerable<T>
    {
        private LinkedList<T> list = new LinkedList<T>();
        private int size = 0;
        public int Size { get => size; }


        public void Enqueue(T item)
        {
            if (item == null)
                throw new InvalidOperationException("Item is null !");

            list.AddLast(item);
            size++;
        }

        public T Dequeue()
        {
            if (size == 0)
                throw new InvalidOperationException("The queue is null");

            T item = list.First.Value;
            list.RemoveFirst();
            size--;

            return item;
        }

        public T Peek()
        {
            if (size == 0)
                throw new InvalidOperationException("The queue is null");

            return list.First.Value;
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
