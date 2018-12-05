using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{

    public enum TileType
    {
        Wall, Floor,
    }

    public int columns = 100;
    public int rows = 100;
    public IntRange numRooms = new IntRange(15, 20);
    public IntRange roomWidth = new IntRange(3, 10);
    public IntRange roomHeight = new IntRange(3, 10);
    public IntRange corridorLengths = new IntRange(6, 10);

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;

    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;
    private GameObject boardHolder;

    // Use this for initialization
    void Start () {
        boardHolder = new GameObject("BoardHolder");
	}

    void SetupTilesArray()
    {
        tiles = new TileType[columns][];
        for(int i = 0; i < tiles.Length; i++)
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

        for(int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();
            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            if(i < corridors.Length)
            {
                corridors[i] = new Corridor();
                corridors[i].SetupCorridor(rooms[i], corridorLengths, roomWidth, roomHeight, columns, rows, false);
            }
        }
    }


    void SetTilesValuesForRooms()
    {
        for(int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            for(int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;
                for(int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }


    void SetTilesValuesForCorridors()
    {
        for(int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            for(int j = 0; j < currentCorridor.corridorLength; j++)
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
                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }



    void InstantiateTiles()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            for(int j = 0; j< tiles[i].Length; j++)
            {
                InstantiateFromArray(floorTiles, i, j);
                {
                    if(tiles[i][j] == TileType.Wall)
                    {
                        intstantiateFromArray(wallTiles, i, j);
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
        InstantiateVerticalOuterWall(reftEdgeX, bottomEdgeY, topEdgeY);

        InstantiateHorizontalOuterWall(leftEdgeX +1f, rightEdgeX -1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX +1f, rightEdgeX - 1f, topEdgeY);


    }
}
