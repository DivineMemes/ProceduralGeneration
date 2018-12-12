﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{

    public enum TileType
    {
        Wall, Floor,
    }

    public int columns;
    public int rows;
    public IntRange numRooms = new IntRange(15, 20);
    public IntRange roomWidth = new IntRange(3, 10);
    public IntRange roomHeight = new IntRange(3, 10);
    public IntRange corridorLengths = new IntRange(6, 10);

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject portalOfHeros;
    public GameObject portalOfCowards;
    public GameObject Player;


    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;
    private GameObject boardHolder;

    // Use this for initialization
    void Start()
    {
        boardHolder = new GameObject("BoardHolder");
        SetupTilesArray();
        CreateRoomsAndCorridors();
        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();
        InstantiateTiles();
        InstantiateOuterWalls();
        RemoveColliders();

    }

    void SetupTilesArray()
    {
        tiles = new TileType[columns][];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows];
        }
    }

    void CreateRoomsAndCorridors()
    {
        rooms = new Room[numRooms.Random];
        corridors = new Corridor[rooms.Length - 1];

        rooms[0] = new Room();
        corridors[0] = new Corridor();

        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
        corridors[0].SetupCorridor(rooms[0], corridorLengths, roomWidth, roomHeight, columns, rows, true);

        for (int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();
            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            if (i < corridors.Length)
            {
                corridors[i] = new Corridor();
                corridors[i].SetupCorridor(rooms[i], corridorLengths, roomWidth, roomHeight, columns, rows, false);
            }
            if(i == rooms.Length - rooms.Length + 1)
            {
                Vector3 PlayerPos = new Vector3(rooms[i].xPos, 0.5f, rooms[i].yPos);
                Instantiate(Player, PlayerPos, Quaternion.identity);
            }
            if (i == rooms.Length - 1)
            {
                Vector3 endportal = new Vector3(rooms[i].xPos, 0.5f, rooms[i].yPos);
                Instantiate(portalOfHeros, endportal, Quaternion.identity);
            }
        }
    }


    void SetTilesValuesForRooms()
    {//rooms are calulated from the lower left tile of the room
        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];
            

            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;
                    {
                        tiles[xCoord][yCoord] = TileType.Floor;
                    }
                }
            }
        }

    }


    void SetTilesValuesForCorridors()
    {
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {

                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }
                try
                {

                    tiles[xCoord][yCoord] = TileType.Floor;
                }
                catch
                {
                    Debug.Log("tried for corridors " + xCoord + ", " + yCoord);
                }
            }
        }
    }



    void InstantiateTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                InstantiateFromArray(floorTiles, i, j);
                {
                    if (tiles[i][j] == TileType.Wall)
                    {
                        InstantiateFromArray(wallTiles, i, j);
                    }
                }
            }
        }
    }
    void InstantiateOuterWalls()
    {
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;


        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);


    }

    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        float currentY = startingY;

        while (currentY <= endingY)
        {
            InstantiateFromArray(outerWallTiles, xCoord, currentY);

            currentY++;
        }
    }

    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        float currentX = startingX;

        while (currentX <= endingX)
        {
            InstantiateFromArray(outerWallTiles, currentX, yCoord);

            currentX++;
        }
    }


    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        int randomIndex = Random.Range(0, prefabs.Length);

        Vector3 position = new Vector3(xCoord, 0f, yCoord);

        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        tileInstance.transform.parent = boardHolder.transform;
    }


    void RemoveColliders()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                if (tiles[i][j] == TileType.Floor)
                {
                    
                }
            }
        }
    }
}
