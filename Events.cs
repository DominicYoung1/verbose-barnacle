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

    public class CommandEvent : IEvent
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


    public class KickOffEvent : IEvent
    {
        // Example of "placeholder" utility. Doesnt need to consist of anything to spark a reaction. 
    }

    public class AttackEvent : IEvent
    {
        string attacker;
        string defender;

        public AttackEvent(string a, string d)
        {
            attacker = a;
            defender = d;
        }

        public string GetAttacker()
        {
            return attacker;
        }

        public string GetDefender()
        {
            return defender;
        }
    }

    public class CombatEvent : IEvent
    {
        string activeMember;
        string passiveMember;

        public CombatEvent(string a, string p)
        {
            activeMember = a;
            passiveMember = p;
        }

        public string GetActiveMember()
        {
            return activeMember;
        }

        public string GetPassiveMemeber()
        {
            return passiveMember;
        }
    }

    public class DeathEvent : IEvent
    {
        string nameOfDead;

        public DeathEvent(string s)
        {
            nameOfDead = s;
        }

        public string GetNameOfDead()
        {
            return nameOfDead;
        }
    }

    public class QuitEvent : IEvent
    {
        public QuitEvent()
        {

        }
    }

    public class NpcActionEvent : IEvent
    {
        string npcName;

        public NpcActionEvent(string s)
        {
            npcName = s;
        }

        public string GetNpcName()
        {
            return npcName;
        }
    }

    public class FleeEvent : IEvent
    {
        string personRunning;
        string personRanFrom;

        public FleeEvent(string s, string p)
        {
            personRunning = s;
            personRanFrom = p;
        }

        public string GetPersonRunning()
        {
            return personRunning;
        }

        public string GetPersonRanFrom()
        {
            return personRanFrom;
        }
    }

    public class EndCombatEvent : IEvent
    {
        string activeMember;
        string passiveMember;

        public EndCombatEvent(string a, string p)
        {
            activeMember = a;
            passiveMember = p;
        }

        public string GetActiveMember()
        {
            return activeMember;
        }

        public string GetPassiveMemeber()
        {
            return passiveMember;
        }
    }
}
