using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public static class Program
    {
        //just a quick generic function that checks the top element of each stack
        //if the top element of A is smaller than B or B is empty
        public static bool CanMoveTo(this Stack<int> currentStack, Stack<int> otherStack)
        {
            return otherStack.Count() == 0 || otherStack.Peek() > currentStack.Peek();
        }


        
        //old main; uncomment out for a text ver
        /*private static void Main(string[] args)
        {
            //Towers of Hanoi
            Console.WriteLine("WE STARTED MAIN");
            Hanoi HanoiObj = new Hanoi(3);
            Console.WriteLine("Starting to Move Disks");
            while(!HanoiObj.isGameSolved())
            {
                HanoiObj.DisplayTowers();
                HanoiObj.MoveDisks();
                HanoiObj.DisplayTowers();
                Console.ReadLine();
            }
            Console.WriteLine("We should be done");
            HanoiObj.DisplayTowers();
        }*/
    }
}
