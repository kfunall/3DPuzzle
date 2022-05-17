using UnityEngine;

public class MovementController : MonoBehaviour
{
    Camera cam;
    GameObject selectedObject;
    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SelectObject();
        if (selectedObject != null)
            MoveObject();
    }
    void SelectObject()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Cube"))
                selectedObject = hit.transform.gameObject;
            else
                selectedObject = null;
        }
    }
    void MoveObject()
    {
        Cube cube = selectedObject.GetComponent<Cube>();
        if (Input.GetKeyDown(KeyCode.W))
            cube.MoveTheCube(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.A))
            cube.MoveTheCube(-Vector3.right);
        else if (Input.GetKeyDown(KeyCode.S))
            cube.MoveTheCube(-Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.D))
            cube.MoveTheCube(Vector3.right);
    }
}