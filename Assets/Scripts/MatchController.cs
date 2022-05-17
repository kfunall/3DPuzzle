using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    GameManager gameManager;
    UIGridCreater uiGrid;
    List<Transform> cubes = new List<Transform>();
    Cube[] cubesOnScene;
    int cubeCount = 0;

    private void Start()
    {
        gameManager = GameManager.Instance;
        uiGrid = GetComponent<UIGridCreater>();
        cubesOnScene = FindObjectsOfType<Cube>();
        cubeCount = gameManager.GridSize * gameManager.GridSize - gameManager.GridSize - 1;
    }
    private void Update()
    {
        if (cubesOnScene != null && uiGrid.UIGridBlock.Length == gameManager.GridSize * gameManager.GridSize)
        {
            foreach (Cube cube in cubesOnScene)
            {
                CheckTheCubeMatches(cube.gameObject.transform);
            }
        }
        if (cubes.Count == cubeCount)
            gameManager.Win();
    }
    public void CheckTheCubeMatches(Transform cube)
    {
        int gridIndex = Mathf.FloorToInt(cube.position.x * gameManager.GridSize + cube.position.z);
        Renderer rend = cube.gameObject.GetComponent<Renderer>();
        if (rend.material.color == uiGrid.UIGridBlock[gridIndex].color && !cubes.Contains(cube))
            cubes.Add(cube);
        else if (rend.material.color != uiGrid.UIGridBlock[gridIndex].color)
            cubes.Remove(cube);
    }
}