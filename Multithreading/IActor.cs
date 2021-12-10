using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MUD.Multithreading
{
    public abstract class IActor
    {

        Dictionary<String, IActor> listeners;
        /**
         * Our actor represents a thread/other unit of computation running somewherer.
         * 
         * Going to our analogy, an actor would be a person who checks their inbox, processes events, and sends messages back out.
         * 
         * The goal for the interface is for the interface to store all of the logic needed to manage queues.
         * 
         * All the imlementor has to do is write code that processes events.
         */

        abstract protected IThreadedQueue Inbox();

        abstract protected void ProcessEvent(IEvent evt);

        abstract protected void Update();

        public void SendMessage(String listenerName, IEvent message)
        {
            // Find the listener in the Dic 
            // Send them the message using the RecieveEvent Method on the listener.
            if (listeners.ContainsKey(listenerName))
            {
                listeners[listenerName].RecieveEvent(message);
            }
            else
            {
                Console.WriteLine("ERROR!: could not find listener {0}", listenerName);
            }
        }

        protected void Initialize()
        {
            listeners = new Dictionary<string, IActor>();
        }

        private void CheckAndProcessQueue()
        {
            while (true)
            {
                if (!Inbox().IsEmpty())
                {
                    IEvent eventToProcess = Inbox().Dequeue();
                    ProcessEvent(eventToProcess);
                }
                    //There was nothing on the queue. Wait a bit and try again.
                   Thread.Sleep(1000);
                Update();
            }
        }

        public void RecieveEvent(IEvent evt)
        {
            Inbox().Enqueue(evt);
        }

        public void Start()
        {
            CheckAndProcessQueue();
        }

        public void RegisterListener(String name, IActor listener)
        {
            listeners.Add(name, listener);
        }
    }
}
