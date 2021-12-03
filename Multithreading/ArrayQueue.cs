﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MUD.Multithreading
{
    public class ArrayQueue : IThreadedQueue
    {
        ArrayList<IEvent> data;

        public ArrayQueue()
        {
            data = new ArrayList<IEvent>(10);
        }
        /**
        *  Enqueue pushes a new event to the back of the queue.
        *  
        *  This method should lock the queue to prevent race conditions from others pushing
        *  or from events popping off the queue.
        */
        public void Enqueue(IEvent evt)
        {
            lock (data)
            {
            data.Push(evt);
            }
        }

        /**
         * Dequeue pops an event off the front of the queue, if the there is an event present.
         * If the queue is empty, this should return null.
         * This method LOCKS the queue to prevent race conditions with others enqueueing/dequeueing at the same time.
         * 
         */
        public IEvent Dequeue()
        {
            lock (data)
            {
                if (!IsEmpty())
                {
                    return data.Remove(0);
                }
                else
                {
                    return null;
                }
            }
        }

        /*
         * This method returns a boolean indicating if the queue is empty or not.
         * This method does not need to lock, since it does not mutate the queue in any way.
         */
        public bool IsEmpty()
        {
            return data.Length() == 0;
        }

    }
}
