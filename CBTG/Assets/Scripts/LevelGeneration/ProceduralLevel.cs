using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ProceduralLevel : MonoBehaviour
{
    public Chunk[] levelPrefabs;
    public Chunk startingPoint;
    //this 2d array essentially describes whether or not a space is occupied. coordinates of chunks can be used as indecies to determine if a space is available
    public bool[][] spaceAvailability;
    public int gridSize;
    //this one is for performance and to avoid an infinite loop. the generator will give this many attempts to find a suitable chunk before throwing in the towel
    public int attemptLimit = 10;

    private void Awake()
    {
        MakeNewGrid();
        MakeNewConnections(startingPoint, startingPoint.outPorts.Length);
        //DEBUG ABSOLUTELY DELETE
        PrintAvailability();
    }

    public void MakeNewGrid(int startposx = 0, int startposy = 0)
    {
        spaceAvailability = new bool[gridSize][];
        for (int i = 0; i < gridSize; i++)
            spaceAvailability[i] = new bool[gridSize];
        spaceAvailability[startposx][startposy] = true;
    }

    public bool MakeNewConnections(Chunk portFrom, int ports = 1, bool cascade = false)
    {
        for (int i = 0; i < ports; i++)
        {
            //origin = (1, 0)
            Vector2 origin = portFrom.gridPos + portFrom.outPorts[portFrom.RandomOutPort];
            Chunk attemptMesh = levelPrefabs[Random.Range(0, levelPrefabs.Length)];
            if (!CheckAvailability(origin))
            {
                bool go = true;
                for (int j = 0; j < attemptMesh.inPorts.Length; j++)
                {
                    Vector2 subOrigin = origin + attemptMesh.inPorts[i];
                    foreach (Vector2 space in attemptMesh.activeSpace)
                    {
                        if (CheckAvailability(subOrigin + space))
                            go = false;
                    }
                    if (go)
                        origin = subOrigin;
                }
                if (!go)
                    continue;

            }
            Chunk recurseTest = PlaceNewChunk(attemptMesh, origin);
            //Chunk recurseTest = PlaceNewChunk(attemptMesh, origin, cascade);
            //if (cascade)
            //    MakeNewConnections(recurseTest, recurseTest.RanomOutPort(), ports, true);
            return true;
        }
        Debug.LogWarning($"Failed to find suitable connection from port \"{portFrom.name}\"");
        return false;
    }

    public Chunk PlaceNewChunk(Chunk prefab, Vector2 coords, bool cascade = false)
    {
        //ew
        GameObject newbie = Instantiate(prefab.gameObject);
        newbie.transform.position = coords;
        newbie.SetActive(true);
        print(coords);

        for (int i = 0; i < prefab.activeSpace.Length; i++)
            spaceAvailability[(int)coords.x + (int)prefab.activeSpace[i].x][(int)coords.y + (int)prefab.activeSpace[i].y] = true;
        //ew
        Chunk dupeChunk = newbie.GetComponent<Chunk>();
        dupeChunk.gridPos = coords;
        return dupeChunk;
    }

    //EXCLUSIVELY FOR DEBUGGING
    public void PrintAvailability()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < spaceAvailability.Length; i++)
        {
            for (int j = 0; j < spaceAvailability.Length; j++)
            {
                char append = spaceAvailability[i][j] == false ? '0' : '1';
                sb.Append(append);
                sb.Append(' ');
            }
            sb.Append('\n');
        }
        print(sb);
    }


    public bool CheckAvailability(int x, int y)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize)
            return true;
        return spaceAvailability[x][y];
    }
    public bool CheckAvailability(Vector2 coords)
    {
        return CheckAvailability((int)coords.x, (int)coords.y);
    }
}
