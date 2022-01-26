using System;
using System.Collections.Generic;
using System.Text;
using MUD.Multithreading;

namespace MUD
{
    public class PrintEvent : IEvent
    {
        string contents;

        public PrintEvent(string s)
        {
            contents = s;
        }

        public string GetString()
        {
            return contents;
        }
    }

    public class CommandEvent: IEvent
    {
        string contents;

        public CommandEvent(string s)
        {
            contents = s;
        }

        public string GetString()
        {
            return contents;
        }
    }

    public class MoveEvent : IEvent
    {
        string nameOfMovingThing;
        string nameOfSelectedDoor;

        public MoveEvent(string n, string d)
        {
            nameOfMovingThing = n;
            nameOfSelectedDoor = d;
        }

        public string GetNameOfMovingThing()
        {
            return nameOfMovingThing;
        }

        public string GetNameOfSelectedDoor()
        {
            return nameOfSelectedDoor;
        }
    }


    public class KickOffEvent: IEvent
    {
        // Example of "placeholder" utility. Doesnt need to consist of anything to spark a reaction. 
    }
}
