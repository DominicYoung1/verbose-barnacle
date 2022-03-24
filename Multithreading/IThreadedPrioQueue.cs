namespace MUD.Multithreading
{

    public class PriorityQueueElement
    {
        public IEvent evt;
        public long time;

        public PriorityQueueElement(IEvent e, long t)
        {
            evt = e;
            time = t;
        }
    }

    public interface IThreadedPrioQueue
    {
        /*
            
         */
        public void Enqueue(IEvent evt, long time); // time is when the event should happen measured in ms..

        public PriorityQueueElement Dequeue();

        public PriorityQueueElement Peek(); // This gives back the element at the top of the queue, but does not remove it.

        public bool IsEmpty();
    }
}
