using System;
using System.Collections.Generic;
using System.Text;
using MUD.Multithreading;

namespace MUD
{
    public class UserInteractionThread : IActor
    {
        ArrayQueue inbox;

        public UserInteractionThread()
        {
            inbox = new ArrayQueue();
            Initialize();
        }
        /**
         * Our actor represents a thread/other unit of computation running somewherer.
         * 
         * Going to our analogy, an actor would be a person who checks their inbox, processes events, and sends messages back out.
         * 
         * The goal for the interface is for the interface to store all of the logic needed to manage queues.
         * 
         * All the imlementor has to do is write code that processes events.
         */

       override protected IThreadedQueue Inbox()
        {
            return inbox;
        }

       override protected void ProcessEvent(IEvent evt)
        {
            // Currently Console.Readline is blocking, we need the ability to print to the screen "Process" even without user interation.

            if (evt is PrintEvent pEvt)
            {
                Console.WriteLine(pEvt.GetString());
                //Console.WriteLine("Hello from our UserInterationThread!");
                SendMessage("GameLoop", new CommandEvent(Console.ReadLine()));
            }
        }
    }
}
