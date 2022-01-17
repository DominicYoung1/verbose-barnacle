using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MUD
{
    class Program
    {
        static void Main(string[] args)
        {

            WorldLoader world = new WorldLoader("ROOMS.txt");
            Dictionary<string, Item> dic = new Dictionary<string, Item>();
            dic = world.GetItems();
            ArrayList<Room> rooms = world.GetRooms(dic);
            Entity me = new Entity(rooms[1], "Harold");
            EntityController npcs = new EntityController();
            Entity jim = EntityFactory.CreateEntity(EntityType.Jim, "Jim", rooms[0]);
            Entity frnak = EntityFactory.CreateEntity(EntityType.Frank, "Frank", rooms[2]);
            npcs.RegisterEntity(jim);
            npcs.RegisterEntity(frnak);
            //npcs.SpawnEntity(rooms[0], "Jim");
            PlayerController controller = new PlayerController(me, rooms);
            Console.WriteLine("Everything started!");
            GameLoop loop = InitializeActors(controller, npcs);
            loop.SendMessage("Player", new PrintEvent(Welcome()),0);
            loop.Start();
            //Welcome();
            //bool running = true;
            //while (running)
            //{
            //    string input = PromptUser();
            //    npcs.UpdateEntities();
            //    controller.processCommand(input);

            //    if (input == "quit")
            //    {
            //        running = false;
            //    }
            //}
            //Console.WriteLine("Bye!");
        }

        public static string PromptUser()
        {
            Console.Write("\n> ");
            string userInput = Console.ReadLine();
            return userInput;
        }

        public static string  Welcome()
        {
            return @"
Welcome to Bambleburg.
This place was once a thriving estate that used to be the seat of power for the local governing body.
But that was a long time ago....for now just expolore!";
        }

        public static GameLoop InitializeActors(PlayerController c, EntityController n)
        {
            // 1. We need to make our GameLoop instance
            // 2. We need to make our UserInteractionThread instance
            // 3. We need to connect our two actors together in an actor system.

            GameLoop gameLoop = new GameLoop(c, n);
            UserInteractionThread userInterationThread = new UserInteractionThread();
            gameLoop.RegisterListener("Player",userInterationThread);
            gameLoop.RegisterListener("Self", gameLoop);
            userInterationThread.RegisterListener("Self", userInterationThread);
            userInterationThread.RegisterListener("GameLoop",gameLoop);

            Thread uiThread = new Thread(new ThreadStart(userInterationThread.Start));
            uiThread.Start();
            return gameLoop;
        }
    }
}
