using System;
using System.Collections.Generic;
using System.Text;

namespace MUD.Multithreading
{
    public interface IEvent
    {
    }

    public class EventWithReceiver
    {
        public IEvent message;
        public string actorName;

        public EventWithReceiver(IEvent e, string s)
        {
            message = e;
            actorName = s;
        }
    }
}
