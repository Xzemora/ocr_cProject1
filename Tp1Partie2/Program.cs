using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Partie2
{
    class Program
    {
        static void Main(string[] args)
        {
            GenList<int> liste = new GenList<int>();
            liste.AddElt(5);
            liste.AddElt(10);
            liste.AddElt(4);
            Console.WriteLine(liste.First.Value);
            Console.WriteLine(liste.First.Next.Value);
            Console.WriteLine(liste.First.Next.Next.Value);
            Console.WriteLine("*************");
            Console.WriteLine(liste.GetElt(0).Value);
            Console.WriteLine(liste.GetElt(1).Value);
            Console.WriteLine(liste.GetElt(2).Value);
            Console.WriteLine("*************");
            liste.Insert(99, 0);
            liste.Insert(33, 2);
            liste.Insert(30, 2);
            Console.WriteLine(liste.GetElt(0).Value);
            Console.WriteLine(liste.GetElt(1).Value);
            Console.WriteLine(liste.GetElt(2).Value);
            Console.WriteLine(liste.GetElt(3).Value);
            Console.WriteLine(liste.GetElt(4).Value);
            Console.WriteLine(liste.GetElt(5).Value);
        }
    }

    public class GenChain<T>
    {
        public T Value { get; set; }
        public GenChain<T> Previous { get; set; }
        public GenChain<T> Next { get; set; }
    }

    public class GenList<T>
    {
        public GenChain<T> First { get; private set; }

        public GenChain<T> Last {
            get {
                if(First == null)
                {
                    return null;
                }
                GenChain<T> last = First;
                while (last.Next != null)
                {
                    last = last.Next;
                }
                return last;
            }
        }

        public void AddElt(T element)
        {
            if( First == null)
            {
                First = new GenChain<T> { Value = element };
            }
            else
            {
                GenChain<T> last = Last;
                last.Next = new GenChain<T> { Value = element, Previous = last };
            }
        }

        public GenChain<T> GetElt(int subscript)
        {
            GenChain<T> elt = First;
            for(int i=0; i< subscript; i++)
            {
                if(elt == null)
                {
                    return null;
                }
                elt = elt.Next;
            }
            return elt;
        }

        public void Insert (T elt, int subscript)
        {
            if(subscript == 0)
            {
                if (First == null)
                {
                    First = new GenChain<T> { Value = elt };
                }
                else
                {
                    GenChain<T> temp = First;
                    First = new GenChain<T> { Value = elt, Next = temp };
                    temp.Previous = First;
                }
            }
            else
            {
                GenChain<T> temp = GetElt(subscript);
                if (temp == null)
                {
                    AddElt(elt);
                }
                else
                {
                    GenChain<T> previous = temp.Previous;
                    GenChain<T> previousNext = previous.Next;
                    previous.Next = new GenChain<T> { Value = elt, Next = previousNext, Previous = previous};
                    temp.Previous = previous.Next;
                }
            }
        }
        
    }
    
}
