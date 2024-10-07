using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour
{
    float xDifference;
    Transform reference;
    Quaternion lockedRotation;
    bool locked;

    // Start is called before the first frame update
    void Start()
    {
        // Remesh the fresh
        DestroyImmediate(GetComponent<MeshCollider>());
        MeshCollider newCollider = gameObject.AddComponent<MeshCollider>();
        newCollider.convex = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (locked)
        {
            transform.position = new Vector3(reference.position.x + xDifference, transform.position.y, transform.position.z);
            transform.rotation = lockedRotation;
        }
    }

    public void LockX(Transform lockTo)
    {
        reference = lockTo;
        xDifference = transform.position.x - lockTo.position.x;
        lockedRotation = transform.rotation;
        locked = true;
    }

    public void FreeX()
    {
        locked = false;
    }
}
