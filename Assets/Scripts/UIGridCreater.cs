using UnityEngine;
using UnityEngine.UI;

public class UIGridCreater : MonoBehaviour
{
    [SerializeField] GridLayoutGroup UIGrid;
    RectTransform UIGridRectTransform;
    Image[] uiGridBlocks;
    Color[] colors = new Color[] { new Color(50 / 255f, 1, 50 / 255f, 1), new Color(1, 25 / 255f, 25 / 255f, 1), new Color(50 / 255f, 50 / 255f, 1, 1), new Color(1, 1, 0, 1) };
    GameManager gameManager;
    PuzzleBlocksCreater puzzle;
    public Image[] UIGridBlock { get { return uiGridBlocks; } private set { } }
    private void Awake()
    {
        puzzle = GetComponent<PuzzleBlocksCreater>();
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        UIGridRectTransform = UIGrid.gameObject.GetComponent<RectTransform>();
        uiGridBlocks = new Image[gameManager.GridSize * gameManager.GridSize];
        CreateUIGrid();
    }
    void CreateUIGrid()
    {
        UIGrid.constraintCount = gameManager.GridSize;
        UIGrid.cellSize = new Vector2(UIGridRectTransform.rect.width / gameManager.GridSize, UIGridRectTransform.rect.height / gameManager.GridSize);
        for (int i = 0; i < gameManager.GridSize * gameManager.GridSize; i++)
        {
            GameObject block = new GameObject();
            block.name = "Image" + i;
            Image blockImage = block.AddComponent<Image>();
            blockImage.color = new Color(65 / 255f, 65 / 255f, 65 / 255f, 1);
            block.transform.SetParent(UIGrid.gameObject.transform);
            uiGridBlocks[i] = blockImage;
        }
        ColorUIGrid();
    }
    void ColorUIGrid()
    {
        uiGridBlocks[Mathf.FloorToInt(puzzle.GreenPuzzleBlock.transform.position.x * gameManager.GridSize) + Mathf.FloorToInt(puzzle.GreenPuzzleBlock.transform.position.z)].color = colors[0];
        uiGridBlocks[Mathf.FloorToInt(puzzle.RedPuzzleBlock.transform.position.x * gameManager.GridSize) + Mathf.FloorToInt(puzzle.RedPuzzleBlock.transform.position.z)].color = colors[1];
        for (int i = 0; i < puzzle.BlueCubeCount; i++)
        {
            int random = Random.Range(0, uiGridBlocks.Length);
            while (uiGridBlocks[random].color != new Color(65 / 255f, 65 / 255f, 65 / 255f, 1))
            {
                random = Random.Range(0, uiGridBlocks.Length);
            }
            uiGridBlocks[random].color = colors[2];
        }
        for (int i = 0; i < puzzle.YellowCubeCount; i++)
        {
            int random = Random.Range(0, uiGridBlocks.Length);
            while (uiGridBlocks[random].color != new Color(65 / 255f, 65 / 255f, 65 / 255f, 1) || uiGridBlocks[random].color == colors[2])
            {
                random = Random.Range(0, uiGridBlocks.Length);
            }
            uiGridBlocks[random].color = colors[3];
        }
    }
}