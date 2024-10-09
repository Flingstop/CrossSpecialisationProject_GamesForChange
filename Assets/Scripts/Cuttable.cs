using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour
{
    float xDifference;
    Transform reference;
    Quaternion lockedRotation;
    bool locked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Remesh()
    {
        //Destroy(GetComponent<MeshCollider>());
        //gameObject.AddComponent<MeshCollider>();
        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;

        Debug.Log("Remeshed");
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
