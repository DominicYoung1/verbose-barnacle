using System.Collections.Generic;


namespace MUD.Multithreading
{
    public class ListPriorityQueue : IThreadedPrioQueue
    {
        // we use a linkedlist becasue they have faster instert, adds, removes ect.. than arraylists.
        LinkedList<PriorityQueueElement> data;

        public ListPriorityQueue()
        {
            data = new LinkedList<PriorityQueueElement>();
        }

        // time is when the event should happen measured in ms..
        public void Enqueue(IEvent evt, long time)
        {
            // we need to look at our input time, and then compare that to the times on the events in list, find the appropriate spot and insert there.
            // smaller ints (times) go first.
            // loop through our list to until we find an element with a time greater than or equal to our input time,
            // then we insert our new even before that element.
            lock (data)
            {

                LinkedListNode<PriorityQueueElement> rover = data.First;
                if (IsEmpty())
                {
                    data.AddFirst(new PriorityQueueElement(evt, time));
                }
                else if (data.Last.Value.time <= time)
                {
                    data.AddLast(new PriorityQueueElement(evt, time));
                }
                else
                {
                    while (time >= rover.Value.time)
                    {
                        rover = rover.Next;
                    }
                    data.AddBefore(rover, new PriorityQueueElement(evt, time));
                }
            }
        }

        public PriorityQueueElement Dequeue()
        {
            lock (data)
            {
                if (!IsEmpty())
                {
                    PriorityQueueElement ret = Peek();
                    data.RemoveFirst();
                    return ret;
                }
                else
                {
                    return null;
                }
            }
        }


        // This gives back the element at the top of the queue, but does not remove it.
        public PriorityQueueElement Peek()
        {
            return data.First.Value;
        }

        public bool IsEmpty()
        {
            return data.Count == 0;
        }


    }
}