using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zonk.Models
{
    public class LoopedQueue<T> : IEnumerable<T>
    {
        private Node<T>? head; // головной/первый элемент
        Node<T>? tail;
        int count;  // количество элементов в списке
        public Node<T>? Current { get; private set; }   
        // добавление элемента
        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);

            if (head == null)
            {
                head = node;
                Current = head;
            }

            else
                tail!.Next = node;
            tail = node;
            tail.Next = head;

            count++;
        }
        // удаление элемента


        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
        // очистка списка
        public void Clear()
        {
            head = null;
            count = 0;
        }
        // содержит ли список элемент
        public bool Contains(T data)
        {
            Node<T>? current = head;
            while (current != null && current.Data != null)
            {
                if (current.Data.Equals(data)) return true;
                current = current.Next;
            }
            return false;
        }
        public Node<T>? Next()
        {
            if (Current == null)
            {
                throw new ArgumentNullException("Очередь пуста");
            }
            else
            {
                Current = Current?.Next;
                return Current;
            }
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T>? current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        // реализация интерфейса IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }
    public class Node<T>
    {
        public Node(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public Node<T>? Next { get; set; }
        public override string ToString()
        {
            return Data?.ToString() ?? "";
        }
    }
}
