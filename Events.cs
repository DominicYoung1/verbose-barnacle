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
}
