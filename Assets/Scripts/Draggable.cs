using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool dragging;

    Vector3 mouseOffset;
    Rigidbody physics;

    [Header("Position")]
    [SerializeField] float zPlane;
    [SerializeField] bool limitMinimumY;
    [SerializeField] float minimumY;

    bool lockedX;
    float lockedXPosition;

    [Header("Rotation")]
    Quaternion startRotation;
    [SerializeField] bool lockRotationWhenDragging;
    [SerializeField] Vector3 defaultRotation;
    [SerializeField] float slerpTime;

    float timeOfPickup;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    public void LockX(bool locked)
    {
        lockedX = locked;
        if (locked)
        {
            lockedXPosition = transform.position.x;
        }
        else
        {
            mouseOffset = Input.mousePosition - GetScreenPosition();
        }
    }

    Vector3 GetScreenPosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mouseOffset = Input.mousePosition - GetScreenPosition();
        timeOfPickup = Time.time;
        startRotation = transform.rotation;
        dragging = true;
    }

    private void OnMouseDrag()
    {
        Vector3 rawPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - mouseOffset);
        Vector3 cameraPosition = Camera.main.transform.position;
        float adjustedX = (cameraPosition.x - rawPosition.x) * (zPlane - rawPosition.z) / (cameraPosition.z - rawPosition.z) + rawPosition.x;
        float adjustedY = (cameraPosition.y - rawPosition.y) * (zPlane - rawPosition.z) / (cameraPosition.z - rawPosition.z) + rawPosition.y;
        if (limitMinimumY && adjustedY < minimumY)
        {
            adjustedY = minimumY;
        }
        if (lockedX)
        {
            adjustedX = lockedXPosition; 
        }
        transform.position = new Vector3(adjustedX, adjustedY, zPlane);
        physics.velocity = new Vector3(0, 0, 0);

        if (lockRotationWhenDragging)
        {
            if (slerpTime <= 0)
            {
                transform.rotation = Quaternion.Euler(defaultRotation);
            }
            else
            {
                float slerpX = Mathf.Clamp((Time.time - timeOfPickup) / slerpTime, 0, 1);
                float slerpY = 1 - Mathf.Pow(slerpX - 1, 4);
                transform.rotation = Quaternion.Slerp(startRotation, Quaternion.Euler(defaultRotation), slerpY);
            }
        }
        physics.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnMouseUp()
    {
        physics.constraints = RigidbodyConstraints.None;
        dragging = false;
    }
}
