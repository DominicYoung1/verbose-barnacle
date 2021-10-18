using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MUD
{
   public class WorldLoader
    {
        string path;

        public WorldLoader(string p)
        {
            path = p;
        }

        //create a funtion that reads in all item files and creates one big list of them.

        public ArrayList<Item>

        public ArrayList<Room> GetRooms()
        {
            ArrayList<Room> rooms = new ArrayList<Room>(10);
            StreamReader file = File.OpenText(path);
            ArrayList<string> lines = new ArrayList<string>(10);
            //create a dict of all the items 
            while (!file.EndOfStream)
            {
                lines.Push(file.ReadLine());
            }
            int roomIndex = 0;
            for (int i = 0; i < lines.Length(); i += 4)
            {
                string nameAndDesc = lines[i];
                string[] nameDescPair = nameAndDesc.Split(':');
                string roomName = nameDescPair[0];
                string roomDesc = nameDescPair[1];
                Room room = new Room(roomName, roomDesc);
                rooms.Push(room);
                roomIndex++;
            }
            int counter = 0;
            for (int i = 1; i < lines.Length(); i += 4)
            {
                string items = lines[i];
                string[] itemStrings = items.Split(',');
                for (int j = 0; j < itemStrings.Length; j++)
                {
                    string itemName = itemStrings[j];
                    Item item = new Item(itemName);
                    rooms[counter].AddItem(item);
                }
                counter++;
            }
            counter = 0;
            for (int i = 2; i < lines.Length(); i += 4)
            {
                string doors = lines[i];
                string[] doorKeyValues = doors.Split(',');
                for (int j = 0; j < doorKeyValues.Length; j++)
                {
                    string[] kvp = doorKeyValues[j].Split(':');
                    string key = kvp[0];
                    string value = kvp[1];
                    Room myRoom = rooms[counter];
                    Room destinationRoom = null;
                    for (int k = 0; k < rooms.Length(); k++)
                    {
                        if (rooms[k].name == value)
                        {
                            destinationRoom = rooms[k];
                        }
                    }
                    if (destinationRoom != null)
                    {
                        myRoom.doors.Add(key, destinationRoom);
                    } else
                    {
                        Console.WriteLine("Error! Could not find the room {0} through the door {1}", value, key);
                    }
                }

                counter++;
            }
            

            return rooms;
        }
    }
}
