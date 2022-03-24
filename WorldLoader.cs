using System;
using System.Collections.Generic;
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

        public Dictionary<string, Item> GetItems()
        {
            Dictionary<string, Item> dic = new Dictionary<string, Item>();
            StreamReader file = File.OpenText("Weapons.txt");
            ArrayList<string> lines = new ArrayList<string>(10);
            while (!file.EndOfStream)
            {
                lines.Push(file.ReadLine());
            }
            for (int i = 0; i < lines.Length(); i += 3)
            {
                string nameAndDesc = lines[i];
                string[] nameDescPair = nameAndDesc.Split(':');
                string itemName = nameDescPair[0];
                string itemDesc = nameDescPair[1];
                string damageAndFiller = lines[i + 1];
                string[] damageAndFillerPair = damageAndFiller.Split(':');
                double damage = Convert.ToDouble(damageAndFillerPair[1]);
                Weapon wep = new Weapon(itemName, itemDesc, damage);
                dic.Add(wep.name, wep);
            }
            StreamReader file2 = File.OpenText("Armor.txt");
            ArrayList<string> lines2 = new ArrayList<string>(10);
            while (!file2.EndOfStream)
            {
                lines2.Push(file2.ReadLine());
            }
            for (int i = 0; i < lines2.Length(); i += 3)
            {
                string nameAndDesc = lines2[i];
                string[] nameDescPair = nameAndDesc.Split(':');
                string itemName = nameDescPair[0];
                string itemDesc = nameDescPair[1];
                string armorAndFiller = lines2[i + 1];
                string[] armorAndFillerPair = armorAndFiller.Split(':');
                double armor = Convert.ToDouble(armorAndFillerPair[1]);
                Armor arm = new Armor(itemName, itemDesc, armor);
                dic.Add(arm.name, arm);
            }
            return dic;
        }
        //public Dictionary<string, Armor> GetArmor()
        //{
        //    Dictionary<string, Item> dic = new Dictionary<string, Item>();
        //    StreamReader file = File.OpenText("Armor.txt");
        //    ArrayList<string> lines = new ArrayList<string>(10);
        //    while (!file.EndOfStream)
        //    {
        //        lines.Push(file.ReadLine());
        //    }
        //    int itemIndex = 0;
        //    for (int i = 0; i < lines.Length(); i += 4)
        //    {
        //        string nameAndDesc = lines[i];
        //        string[] nameDescPair = nameAndDesc.Split(':');
        //        string itemName = nameDescPair[0];
        //        string itemDesc = nameDescPair[1];
        //        for (int j = 1; j < lines.Length(); j += 4)
        //        {
        //            string armorAndFiller = lines[j];
        //            string[] armorAndFillerPair = armorAndFiller.Split(':');
        //            double armor = Convert.ToDouble(armorAndFillerPair[1]);
        //            Armor arm = new Armor(itemName, itemDesc, armor);
        //            dic.Add(arm.name, arm);
        //        }
        //    }
        //    return dic

        //}

        public ArrayList<Room> GetRooms(Dictionary<string, Item> itemLookup)
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
                if (items != "")
                {
                    for (int j = 0; j < itemStrings.Length; j++)
                    {
                        string itemName = itemStrings[j];
                        Item item = itemLookup[itemName];
                        rooms[counter].AddItem(item);
                    }
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
                    }
                    else
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
