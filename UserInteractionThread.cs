using MUD.Multithreading;
using System;
using System.Threading;

namespace MUD
{
    public class UserInteractionThread : IActor
    {
        ListPriorityQueue inbox;
        Thread readLineThread;

        public UserInteractionThread()
        {
            inbox = new ListPriorityQueue();
            readLineThread = new Thread(new ThreadStart(ReadLineHelper));
            Initialize();
            readLineThread.Start();
            SetWaitTime(100);
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

        override protected IThreadedPrioQueue Inbox()
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
                //SendMessage("GameLoop", new CommandEvent(Console.ReadLine()));
            }
        }

        void ReadLineHelper()
        {
            // this needs to constantly be looking for user input to pass to the game loop, without disrupting the comms chain.
            bool isLooking = false;
            while (isLooking == false)
            {
                SendMessage("GameLoop", new CommandEvent(Console.ReadLine()), 0);
            }
        }
    }
}
