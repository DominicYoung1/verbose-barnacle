using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD
{
    class ShortestPath
    {
        public int FindShortestPath(Room startingRoom, Room destinationRoom)
        {
            int counter = 0;
            Dictionary<int, ArrayList<Room>> dic = new Dictionary<int, ArrayList<Room>>();
            ArrayList<Room> list = new ArrayList<Room>(10);
            for (int i = 0; i < startingRoom.doors.Count(); i++)
            {
                Room of nextDoor 
                string nextDoor = startingRoom.GetRandomDoorName();
                if (nextDoor != destinationRoom.name)
                {
                    counter++;
                    list.Push(nextDoor);
                }
            }
        }
    }
}
