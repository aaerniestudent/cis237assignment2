//Anthony Aernie
//CIS237 MW 6:00
//Feb 8, 2017
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237assignment2
{
    /// <summary>
    /// This class is used for solving a char array maze.
    /// You might want to add other methods to help you out.
    /// A print maze method would be very useful, and probably neccessary to print the solution.
    /// If you are real ambitious, you could make a seperate class to handle that.
    /// </summary>
    class MazeSolver
    {
        /// <summary>
        /// Class level memeber variable for the mazesolver class
        /// </summary>
        char[,] maze;
        int xStart;
        int yStart;
        bool solved;

        /// <summary>
        /// Default Constuctor to setup a new maze solver.
        /// </summary>
        public MazeSolver()
        {}


        /// <summary>
        /// This is the public method that will allow someone to use this class to solve the maze.
        /// Feel free to change the return type, or add more parameters if you like, but it can be done
        /// exactly as it is here without adding anything other than code in the body.
        /// </summary>
        public void SolveMaze(char[,] maze, int xStart, int yStart)
        {
            //Assign passed in variables to the class level ones. It was not done in the constuctor so that
            //a new maze could be passed in to this solve method without having to create a new instance.
            //The variables are assigned so they can be used anywhere they are needed within this class. 
            this.maze = maze;
            this.xStart = xStart;
            this.yStart = yStart;

            //the starting spot
            this.maze[yStart, xStart] = 'X';
            //solved flag set to false
            solved = false;
            //maze with starting point
            printMaze(this.maze);                      
            
            //start recursion, the first step
            mazeTraversal(this.maze,this.yStart,this.xStart);

            //after recursion
            Console.WriteLine("The maze is solved, press any key to continue.");
            //final maze print: solution is 'X', checked spaces are 'O', not checked spaces are '.'
            printMaze(this.maze);            
            Console.ReadKey();
        }


        /// <summary>
        /// This should be the recursive method that gets called to solve the maze.
        /// Feel free to change the return type if you like, or pass in parameters that you might need.
        /// This is only a very small starting point.
        /// </summary>
        private void mazeTraversal(char[,] maze, int y, int x)
        {            
            //Win condition: Case that we are looking for.
            //if it reaches the end then it is over, so the boolean solved variable is a flag
            //the end is any edge, top, bottom, left or right
            //any sized rectangular maze with an exit at the edge works with this
            //top edge | left edge
            if (y == 0 | x == 0 
                //right edge
                | x == maze.GetLength(1) - 1 
                //bottom edge
                | y == maze.GetLength(0) - 1 )
            {
                //flag so no more actions can be taken in the previous mazeTraversal
                solved = true;
                //returns because it doesn't need to check for dirrections, it is over.
                //If it did check dirrections, it will create an error when it checked for a dirrection off of the map.                                             
                return;
            }            
            
            //dirrections, none of these can occur after it is solved
            //the first one is the only one that doesn't check the flag becuase it doesn't need to be checked, it was checked above
            //if it can go up            
            if (maze[y - 1,x] == '.')
            {
                //all moves do the same steps in a different dirrection
                //place an X where the move will occur
                maze[y - 1, x] = 'X';
                //displays the move
                printMaze(maze);
                //recursive call for the move after this move
                mazeTraversal(maze, y - 1, x);
            }
            //all conditions after the first move check will check to see if the win condition has been triggered, so they do not activate.
            //if it can go right
            if (maze[y, x + 1] == '.' & !solved)
            {
                maze[y, x + 1] = 'X';
                printMaze(maze);
                mazeTraversal(maze, y, x + 1);
            }
            //if it can go down
            if (maze[y + 1, x] == '.' & !solved)
            {
                maze[y + 1, x] = 'X';
                printMaze(maze);
                mazeTraversal(maze, y + 1, x);
            }
            
            //if it can go left
            if (maze[y, x - 1] == '.' & !solved)
            {
                maze[y, x - 1] = 'X';
                printMaze(maze);
                mazeTraversal(maze, y, x - 1);
            }
            //Fail condition: Base Case for failure to find what we are looking for
            //Fail to find the exit or new dirrection to go. Doesn't occur after solving       
            if (!solved)
            {
                //marks a backtrack on the current space, it will return to the previous move after printing the maze
                maze[y, x] = 'O';
                printMaze(maze);
            }
            //when it returns it will return to where it was called previously.
            //Then it will check another dirrection or fail to find a new dirrection and act as if it has found a dead end         
        }

        //displays the maze.
        private void printMaze(char[,] maze)
        {
            //works for any sized rectangle maze
            for(int i=0; i < maze.GetLength(0); i++)
            {
                for (int o=0; o < maze.GetLength(1); o++)
                {
                    Console.Write(maze[i, o] + " ");                                       
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }       
    }
}
