using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private Block startBlock;
    [SerializeField]
    private List<GameObject> blockObjects;
    [SerializeField]
    private List<GameObject> enemies;
    private List<Vector2> takenPositions;

    private List<Block> blocks;

    private bool done = false;
    private List<OpenPosition> openPositions;

    private int seed;

    public int Seed
    {
        get { return seed; }
    }

	// Use this for initialization
	void Start () {
        // Create lists
        takenPositions = new List<Vector2>();
        blocks = new List<Block>();
        openPositions = new List<OpenPosition>();

        // Add new open position at start point.

        takenPositions.Add(new Vector2(0, 0));
        AddOpenPosition(new Vector2(), Side.Right, startBlock);
        AddOpenPosition(new Vector2(), Side.Left, startBlock);
        
        Random.seed = PlayerPrefs.GetInt("CurrentSeed");
        Debug.Log(Random.seed.ToString());
        seed = Random.seed;

        for (int i = 0; i < blockObjects.Count; i++)
        {
            blocks.Add(blockObjects[i].GetComponent<Block>());
        }

        int blockCounter = 0;
        int blocksPlacedWithoutMonsters = 1;

	    while(!done)
        {
            List<Block> allowedBlocks = new List<Block>();
            bool placedBlock = false;

            // Per open position
            for (int j = 0; j < openPositions.Count; j++)
            {
                // Per block
                for (int i = 0; i < blocks.Count; i++)
                {
                    // If the block is compatible
                    if (blocks[i].AllowSide(openPositions[j].openSide))
                    {
                        // If the position also has room for the block
                        if (IsPositionAvailable(openPositions[j].position + SideMath.Side2Direction(openPositions[j].openSide) + blocks[i].GetArrOffset(blocks[i].LastRequestedSide), blocks[i].Size))
                        {
                            allowedBlocks.Add(blocks[i]);
                        }
                    }
                }

                // If we can place a block
                if (allowedBlocks.Count > 0)
                {
                    float random = Random.Range(0, GetTotalWeight(allowedBlocks, blockCounter));
                    Block block = GetBlockFromWeight(allowedBlocks, random, blockCounter);
                   
                    Vector3 position = new Vector3();
                    position += openPositions[j].adjacentBlock.transform.position;
                    position += openPositions[j].adjacentBlock.GetOffset(openPositions[j].openSide);
                    position -= block.GetOffset(block.LastRequestedSide);

                    GameObject go = (GameObject)Instantiate(block.gameObject, position, new Quaternion());
                    go.transform.parent = this.transform;

                    List<Side> newSides = block.GetSides();

                    foreach (Side side in newSides)
                    {
                        if (side != block.LastRequestedSide)
                        {
                            Vector2 newPosition = openPositions[j].position + SideMath.Side2Direction(openPositions[j].openSide) + block.GetArrOffset(side);
                            AddOpenPosition(newPosition, side, go.GetComponent<Block>());
                        }
                    }

                    ClosePosition(openPositions[j].position, block.Size, openPositions[j].openSide);

                    List<Vector3> monsterPositions = new List<Vector3>();
                    for (int i = 0; i < block.AllowedMonsterLocations.Length; i++)
                    {
                        monsterPositions.Add(block.AllowedMonsterLocations[i] + go.transform.position);
                    }
                    
                    bool placedMonster = false;

                    for (int i = 0; i < monsterPositions.Count; i++)
                    {
                        if (Random.Range(0, 100) < Mathf.Pow(2.5f, blocksPlacedWithoutMonsters))
                        {
                            int monsterPosition = Random.Range(0, monsterPositions.Count);
                            Vector3 monsterLocation = monsterPositions[monsterPosition];
                            
                            GameObject monster = enemies[Random.Range(0, enemies.Count)];
                            monsterLocation.y += monster.GetComponent<Enemy>().GetYOffset();
                            monster = Instantiate(monster);
                            monster.transform.position = monsterLocation;
                            monster.transform.SetParent(go.transform);

                            placedMonster = true;
                        }
                    }

                    if(placedMonster)
                    {
                        blocksPlacedWithoutMonsters = 0;
                    }

                    blocksPlacedWithoutMonsters++;

                    placedBlock = true;
                    break; // Because we just removed things from openPositions and that might cause nasty effects if we continue this loop.
                }
            }

            blockCounter++;

            if (!placedBlock || blockCounter == 2000) // Exit strategy
            {
                done = true;
            }
        }
	}

    void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1;
    }
	
    private bool IsPositionAvailable(Vector2 position, Vector2 size)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Vector2 adjacentPosition = position + new Vector2(i, j);

                if(takenPositions.Contains(adjacentPosition))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void ClosePosition(Vector2 position, Vector2 size, Side side)
    {
        openPositions.RemoveAll(x => x.position == position && x.openSide == side);

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Vector2 adjacentPosition = SideMath.Side2Direction(side) + position + new Vector2(i, j);
                takenPositions.Add(adjacentPosition);
            }
        }
    }

    private void AddOpenPosition(Vector2 position, Side side, Block block)
    {
        openPositions.Add(new OpenPosition(position + block.GetArrOffset(side), side, block));
    }

    private float GetTotalWeight(List<Block> blocks, int blockCount)
    {
        float weight = 0;

        foreach (Block b in blocks)
        {
            weight += b.Weight * (1 + (b.WeightAddition * blockCount));
        }

        return weight;
    }

    private Block GetBlockFromWeight(List<Block> blocks, float weight, int blockCount)
    {
        foreach (Block b in blocks)
        {
            weight -= b.Weight * (1 + (b.WeightAddition * blockCount));

            if(weight <= 0)
            {
                return b;
            }
        }

        // Should never happen
        Debug.Log("Couldn't find block from weight! w:" + weight + " tw:" + GetTotalWeight(blocks, blockCount));
        return null;
    }
}
