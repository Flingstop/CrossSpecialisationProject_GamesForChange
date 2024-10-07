using Hanzzz.MeshSlicerFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{

    MeshSlicer slicer = new MeshSlicer();

    GameObject cutting;
    float enterHeight;

    [SerializeField] Transform[] referencePoints = new Transform[3];
    (Vector3, Vector3, Vector3) slicePlane;

    [SerializeField] Material testMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Cuttable>() != null && cutting == null)
        {
            Debug.Log("Hit " + other.gameObject);
            
            cutting = other.gameObject;
            cutting.GetComponent<Cuttable>().LockX(transform);

            enterHeight = transform.position.y;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == cutting)
        {
            if (transform.position.y < enterHeight)
            {
                Debug.Log("Cut " + cutting);
                (GameObject, GameObject) pieces = Cut();
                pieces.Item1.GetComponent<Cuttable>().FreeX();
                pieces.Item2.GetComponent<Cuttable>().FreeX();
                Destroy(cutting);
            }
            else 
            {
                cutting.GetComponent<Cuttable>().FreeX();
            }
            
            cutting = null;
        }
    }

    (GameObject, GameObject) Cut()
    {
        slicePlane = (referencePoints[0].position, referencePoints[1].position, referencePoints[2].position);
        return slicer.Slice(cutting, slicePlane, testMaterial);
    }
}
