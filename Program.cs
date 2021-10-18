using System;
using System.IO;

namespace MUD
{
    class Program
    {
        static void Main(string[] args)
        {
            WorldLoader world = new WorldLoader("ROOMS.txt");
            ArrayList<Room> rooms = world.GetRooms();
            Entity me = new Entity(rooms[1], "Harold");
            EntityController npcs = new EntityController();
            Entity jim = EntityFactory.CreateEntity(EntityType.Jim, "Jim", rooms[0]);
            npcs.RegisterEntity(jim);
            //npcs.SpawnEntity(rooms[0], "Jim");
            PlayerController controller = new PlayerController(me, rooms);
            Console.WriteLine("Everything started!");
            Welcome();
            bool running = true;
            while (running)
            {
                string input = PromptUser();
                npcs.UpdateEntities();
                controller.processCommand(input);

                if (input == "quit")
                {
                    running = false;
                }
            }
            Console.WriteLine("Bye!");
        }

        public static string PromptUser()
        {
            Console.Write("\n> ");
            string userInput = Console.ReadLine();
            return userInput;
        }

        public static void  Welcome()
        {
            Console.WriteLine("Welcome to Bambleburg.");
            Console.WriteLine("This place was once a thriving estate that used to be the seat of power for the local governing body.");
            Console.WriteLine("But that was a long time ago....for now just expolore!");

        }
    }
}
