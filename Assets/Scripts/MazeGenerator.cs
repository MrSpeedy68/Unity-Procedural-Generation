using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum WallState
{
    //The wall state is stored in a nibble (4 bits) while the visited walls are stored in a byte (8 bits)
    //0000 = No Walls
    //1111 = LEFT,RIGHT,UP,DOWN
    LEFT = 1, //0001
    RIGHT = 2, //0010
    UP = 4, //0100
    DOWN = 8, //1000
    
    VISITED = 128, // 1000 0000
}

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Position Position;
    public WallState SharedWall;
}

public static class MazeGenerator
{
    //Returns the opposite wall
    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall) 
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }
    }


    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height)
    {
        var rng = new System.Random((int)Time.time);
        var positionStack = new Stack<Position>();
        var position = new Position { X = rng.Next(0, width), Y = rng.Next(0, height) }; //Getting a new random position with the rng

        maze[position.X, position.Y] |= WallState.VISITED; // 1000 1111 Applying Bitwise OR generating the new byte which gives a visited node 1000 with all walls present 1111
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze, width, height); //This will return the neighbours that are unvisited from the current position

            if (neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.Position;
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall; //Removing the bits for the wall that the neighbour touches
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall); // Removing the opposite wall from the neighbour

                maze[nPosition.X, nPosition.Y] |= WallState.VISITED; // Adding on the 1000 0000 state of this position being visited
                
                positionStack.Push(nPosition);
            }
        }
        
        return maze;
    }
    
    //Returns a List of Neighbours that were not yet visited e.g dont have 1000 0000
    private static List<Neighbour> GetUnvisitedNeighbours(Position p, WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbour>();

        if (p.X > 0) //left
        {
            if (!maze[p.X - 1, p.Y].HasFlag(WallState.VISITED)) //Check to see if the wall was not visited
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.LEFT
                });
            }
        }
        
        if (p.Y > 0) //bottom
        {
            if (!maze[p.X, p.Y - 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y - 1
                    },
                    SharedWall = WallState.DOWN
                });
            }
        }
        
        if (p.Y < height - 1) //up
        {
            if (!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    SharedWall = WallState.UP
                });
            }
        }
        
        if (p.X < width - 1) //right
        {
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.RIGHT
                });
            }
        }

        return list;
    }
    
    //This function Generates a maze grid where all 4 walls are present in a 2D array. Then the recursive back tracker is applied to the initially generated grid.
    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i, j] = initial;
            }
        }
        
        return ApplyRecursiveBacktracker(maze, width, height);
    }
}
