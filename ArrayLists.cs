using System;

namespace MUD
{
    public class ArrayList<T>
    {

        T[] data;
        int length;

        public ArrayList(int allocationSize)
        {
            data = new T[allocationSize];
            length = 0;
        }

        public T this[int i]
        {
            get
            {
                if (i < length && i >= 0)
                {
                    return data[i];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

            set //(int value)
            {
                if (i < length && i >= 0)
                {
                    data[i] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public int Length()
        {
            return length;
        }

        public void Push(T elem)
        {
            // Push the elem onto the end of the length (the part of the array that we have filled with values)
            data[length] = elem;
            length++;
            if (length == data.Length)
            {
                ExpandArray();
            }
        }
        //Remove the elem at an index and return that value.
        public T Remove(int index)
        {
            T returnedIndex = data[index];
            for (int i = index + 1; i < length; i++)
            {
                data[i - 1] = data[i];
            }
            length--;
            return returnedIndex;
        }

        void ExpandArray()
        {
            T[] newData = new T[data.Length * 2];
            for (int i = 0; i < data.Length; i++)
            {
                newData[i] = data[i];
            }
            data = newData;

        }
    }
}
