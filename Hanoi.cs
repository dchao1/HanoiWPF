using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Hanoi
    {
        private Stack<int>[] _towers;
        public Stack<int>[] Towers
        {
            get { return _towers; }
            set { _towers = value; }
        }
        //keep track of latest move
        //for example tower 1 -> tower 2
        //we cannot move the same disk twice in a row
        private int _latestMovedIndex = -1;

        //find the biggest MOVABLE disk D (where movement does not include the reverse of the last move)
        //Towers.Max(x => x.Peek());
        //TODO might have to move these 3 into for loop
        private int _originStackIndex = -1; //this is the [i] of the stack we are moving
        private int _destinationStackIndex = -1; //this is the [index] of the stack we want


        public Hanoi(int x, bool isEnd = false)
        {
            Towers = new Stack<int>[] { new Stack<int>(), new Stack<int>(), new Stack<int>() };
            for (int i = x; i > 0; i--)
            {
                Towers[isEnd ? 2 : 0].Push(i);

            }
        }

        public void DisplayTowers()
        {
            Console.WriteLine("We Are DisplayingTowers()");
            if (Towers[0].Count != 0) Console.WriteLine("Tower 0 has top Ele = " + Towers[0].Peek());
            if (Towers[1].Count != 0) Console.WriteLine("Tower 1 has top Ele = " + Towers[1].Peek());
            if (Towers[2].Count != 0) Console.WriteLine("Tower 2 has top Ele = " + Towers[2].Peek());
        }

        //Check if game is solved
        public bool IsGameSolved()
        {
            return (Towers[0].Count == 0 && Towers[1].Count() == 0);
        }

        //Moves ONE disk; the next disk that can move
        public void MoveDisks()
        {
            int biggestDiskFound = 0; //the size of the biggest disk, found at the top of Towers[originStackIndex]

            for (int i = 0; i < 3; i++)
            {
                int topDisk = -1;
                if (Towers[i].Count != 0)
                {
                    topDisk = Towers[i].Peek(); //get the top element
                }
                //for each Tower, we check the following:
                //  if the tower has contents (Towers[i].Count != 0)
                //  if we did not just move from that tower (i != lastMovedIndex)
                //  if the tower contains the biggest disk available for movement (the last two conditions)
                //if true, then we know what we have to do. Set the origin and destination, then prepare for disk movement
                int findMove = FindMove(Towers[i], i);
                if (Towers[i].Count != 0
                    && i != _latestMovedIndex //if did not just move
                    && topDisk > biggestDiskFound //and is biggest
                    && findMove != -1) //and it has a move
                {
                    _originStackIndex = i;
                    biggestDiskFound = Towers[i].Peek();
                    _destinationStackIndex = findMove;
                }
            }

            //Now we know what our origin and destination are. Time to move the disk
            //find height H of stack containg D
            //move D to correct tower (where correct tower is left other tower if H is even, right if H is odd)
            int movedDisk = Towers[_originStackIndex].Pop();
            Towers[_destinationStackIndex].Push(movedDisk);
            _latestMovedIndex = _destinationStackIndex; //here
        }

        //takes a stack and the Tower[index] of the stack, and returns the position of the tower the stack will move to
        public int FindMove(Stack<int> currentStack, int i)
        {
            Console.WriteLine($"starting FindMove() with int i = {i}");
            //just a quick check to make sure we are not checking an empty array
            if (currentStack.Count() == 0) return -1; //if there is no content return empty

            //if 0, even, return 1
            //if 0, odd, return 2
            //if 1, even, return 0
            //if 1, odd, return 2
            //if 2, even, return 0
            //if 2, odd, return 1
            int[] remainingStackIndexes = new int[2];
            if (i == 0) remainingStackIndexes = new int[] { 1, 2 };
            if (i == 1) remainingStackIndexes = new int[] { 0, 2 };
            if (i == 2) remainingStackIndexes = new int[] { 0, 1 };

            //if whichTower is 0, tower A
            //if whichTower is 1, tower B, else -1 = no move possible
            int whichTower = -1;
            
            //if height of current Stack is even, move to tower A (first in remainingStackIndexes)
            //at this point, currentStack can either move to tower B (last in newMoves) or it cannot move at all
            //we check if it can move to tower B, else return null;
            int Height = currentStack.Count();
            Console.WriteLine("  Height = " + Height);
            if (Height % 2 == 0) whichTower = 0;
            if (Height % 2 == 1) whichTower = 1;
            Console.WriteLine("  whichTower = " + whichTower);
            int towerIndex = remainingStackIndexes[whichTower];
            Console.WriteLine("  towerIndex = " + towerIndex);

            //if the tower we are moving to is empty, or bigger than the moving disk
            //  then we have a valid move; return the destination
            if (Towers[towerIndex].Count == 0
                    || Towers[towerIndex].Peek() > Towers[i].Peek())
            {
                Console.WriteLine("  We are returning a valid dest of " + towerIndex);
                return towerIndex;
            }

            //we should only get here if the tower cannot move
            Console.WriteLine("  We are returning a -1 dest");
            return -1;
        }
    }
}
