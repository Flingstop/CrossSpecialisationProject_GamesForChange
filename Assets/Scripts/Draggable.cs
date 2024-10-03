using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{

    Vector3 mouseOffset;
    Rigidbody physics;

    [SerializeField] float zPlane;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetScreenPosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mouseOffset = Input.mousePosition - GetScreenPosition();
    }

    private void OnMouseDrag()
    {
        Vector3 rawPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - mouseOffset);
        Vector3 cameraPosition = Camera.main.transform.position;
        float adjustedX = (cameraPosition.x - rawPosition.x) * (zPlane - rawPosition.z) / (cameraPosition.z - rawPosition.z) + rawPosition.x;
        float adjustedY = (cameraPosition.y - rawPosition.y) * (zPlane - rawPosition.z) / (cameraPosition.z - rawPosition.z) + rawPosition.y;
        transform.position = new Vector3(adjustedX, adjustedY, zPlane);
        physics.velocity = new Vector3(0, 0, 0);
    }
}
