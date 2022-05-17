using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] float raycastDistance = 0.6f;
    bool canMove = false;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void MoveTheCube(Vector3 direction)
    {
        CheckAroundTheCube(direction);
        if (canMove)
        {
            Vector3 pos = transform.position + direction;
            pos.x = Mathf.Clamp(pos.x, 0, gameManager.GridSize - 1);
            pos.z = Mathf.Clamp(pos.z, 0, gameManager.GridSize - 1);
            transform.position = pos;
            canMove = false;
        }
    }
    void CheckAroundTheCube(Vector3 raycastDirection)
    {
        Ray ray = new Ray(transform.position, raycastDirection);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, raycastDistance))
            canMove = true;
    }
}