using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    Button resetButton;
    GameManager gameManager;

    private void Start()
    {
        GetReferences();
        resetButton.onClick.AddListener(gameManager.Reload);
    }
    void GetReferences()
    {
        gameManager = GameManager.Instance;
        resetButton = transform.GetChild(1).GetComponent<Button>();
    }
}