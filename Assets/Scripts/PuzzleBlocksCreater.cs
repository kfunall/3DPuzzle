using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class PuzzleBlocksCreater : MonoBehaviour
{
    // * References
    [SerializeField] GameObject gridCubePrefab;
    [SerializeField] GameObject puzzleBlockParent;
    [SerializeField] Transform cam;
    GameManager gameManager;
    // * Variables
    int puzzleBlokcCount, yellowCubeCount, blueCubeCount;
    GameObject redPuzzleBlock, greenPuzzleBlock;
    // * Collections
    List<Vector3> spawnedPuzzleBlocksPositions = new List<Vector3>();
    [SerializeField] GameObject[] puzzleBlocks; // 0 -> green, 1 -> red, 2 -> blue, 3-> yellow
    // * Props
    public GameObject GreenPuzzleBlock { get { return greenPuzzleBlock; } private set { } }
    public GameObject RedPuzzleBlock { get { return redPuzzleBlock; } private set { } }
    public int BlueCubeCount { get { return blueCubeCount; } private set { } }
    public int YellowCubeCount { get { return yellowCubeCount; } private set { } }

    private void Awake()
    {
        gameManager = GameManager.Instance;
        CreateGrid();
        CreatePuzzleBlocks();
        cam.position = new Vector3((gameManager.GridSize - 1) / 2f, gameManager.GridSize * 2, -2.5f);
    }

    void CreateGrid()
    {
        gridCubePrefab.transform.localScale = new Vector3(gameManager.GridSize, 1, gameManager.GridSize);
        gridCubePrefab.transform.position += new Vector3((gameManager.GridSize - 1) / 2f, 0, (gameManager.GridSize - 1) / 2f);
    }
    void CreatePuzzleBlocks()
    {
        puzzleBlokcCount = gameManager.GridSize * gameManager.GridSize - gameManager.GridSize - 3;
        greenPuzzleBlock = Instantiate(puzzleBlocks[0], puzzleBlockPosition(), Quaternion.identity, puzzleBlockParent.transform);
        redPuzzleBlock = Instantiate(puzzleBlocks[1], puzzleBlockPosition(), Quaternion.identity, puzzleBlockParent.transform);
        for (int i = 0; i < puzzleBlokcCount; i++)
        {
            int randomIndex = Random.Range(2, puzzleBlocks.Length);
            GameObject block = Instantiate(puzzleBlocks[randomIndex], puzzleBlockPosition(), Quaternion.identity, puzzleBlockParent.transform);
            if (block.GetComponent<Renderer>().material.color == new Color(50 / 255f, 50 / 255f, 1, 1))
                blueCubeCount++;
            else
                yellowCubeCount++;
        }
    }
    Vector3 puzzleBlockPosition()
    {
        int randomX = Random.Range(0, gameManager.GridSize);
        int randomZ = Random.Range(0, gameManager.GridSize);
        Vector3 randomPos = new Vector3(randomX, 1f, randomZ);
        while (spawnedPuzzleBlocksPositions.Contains(randomPos))
        {
            randomX = Random.Range(0, gameManager.GridSize);
            randomZ = Random.Range(0, gameManager.GridSize);
            randomPos = new Vector3(randomX, 1f, randomZ);
        }
        spawnedPuzzleBlocksPositions.Add(randomPos);
        return randomPos;
    }
}