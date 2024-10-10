using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Remesh();
    }

    void Remesh()
    {
        //Destroy(GetComponent<MeshCollider>());
        //gameObject.AddComponent<MeshCollider>();
        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
