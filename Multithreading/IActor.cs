using System;
using System.Collections.Generic;
using System.Threading;

namespace MUD.Multithreading
{
    public abstract class IActor
    {

        Dictionary<String, IActor> listeners;
        int waitTime;
        bool isRunning;
        /**
         * Our Actors are units of computation that are typically built on top of threads.
         * 
         * Going to our analogy, an actor would be a person who checks their inbox, processes events, and sends messages back out.
         * 
         * The goal for the interface is for the interface to store all of the logic needed to manage queues.
         * 
         * All the imlementor has to do is write code that processes events.
         */

        abstract protected IThreadedPrioQueue Inbox();

        abstract protected void ProcessEvent(IEvent evt);

        public void SendMessage(String listenerName, IEvent message, long expectedTime)
        {
            // Find the listener in the Dic 
            // Send them the message using the RecieveEvent Method on the listener.
            if (listeners.ContainsKey(listenerName))
            {
                listeners[listenerName].RecieveEvent(message, expectedTime);
            }
            else
            {
                Console.WriteLine("ERROR!: could not find listener {0}", listenerName);
            }
        }

        protected void Initialize()
        {
            listeners = new Dictionary<string, IActor>();
            waitTime = (1000);
        }

        private void CheckAndProcessQueue()
        {
            while (isRunning)
            {
                long currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                if (!Inbox().IsEmpty())
                {
                    PriorityQueueElement nextEvt = Inbox().Peek();
                    if (nextEvt.time <= currentTime)
                    {
                        ProcessEvent(nextEvt.evt);
                        Inbox().Dequeue();
                    }
                }
                Thread.Sleep(10);
            }
            // ****** OLD VERSION*******
            //while (true)
            //{
            //    if (!Inbox().IsEmpty())
            //    {
            //        bool IsEmpty = false;
            //        while (IsEmpty == false)
            //        {
            //            PriorityQueueElement eventToProcess = Inbox().Dequeue();
            //            ProcessEvent(eventToProcess);
            //            if (Inbox().IsEmpty())
            //            {
            //                IsEmpty = true;
            //            }
            //        }
            //    }
            //    //There was nothing on the queue. Wait a bit and try again.
            //    Thread.Sleep(waitTime);
            //    Update();
            //}
            //**** OLD VERSION***
        }

        public void RecieveEvent(IEvent evt, long expectedTime)
        {
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long neededTime = expectedTime + currentTime;
            Inbox().Enqueue(evt, neededTime);
        }

        public void Start()
        {
            isRunning = true;
            CheckAndProcessQueue();
        }

        public void RegisterListener(String name, IActor listener)
        {
            listeners.Add(name, listener);
        }

        protected void SetWaitTime(int newTime)
        {
            waitTime = newTime;
        }

        protected void Stop()
        {
            isRunning = false;
        }
    }
}
