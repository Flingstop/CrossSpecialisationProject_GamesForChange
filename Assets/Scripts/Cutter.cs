using Hanzzz.MeshSlicerFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{

    MeshSlicer slicer = new MeshSlicer();
    Draggable drag;
    Collider parentCollider;

    GameObject cutting;
    float enterHeight;

    [SerializeField] Transform[] referencePoints = new Transform[3];
    (Vector3, Vector3, Vector3) slicePlane;

    [SerializeField] Material testMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
        drag = GetComponentInParent<Draggable>();
        parentCollider = GetComponentInParent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Cuttable>() != null && cutting == null && drag.dragging)
        {
            cutting = other.gameObject;
            drag.LockX(true);
            cutting.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

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
                
                if (pieces.Item1 && pieces.Item2)
                {
                    pieces.Item1.GetComponent<Cuttable>().Initialize();
                    pieces.Item2.GetComponent<Cuttable>().Initialize();

                    Destroy(cutting);
                }
                else
                {
                    cutting.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }

                drag.LockX(false);

            }
            else 
            {
                drag.LockX(false);
                cutting.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
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
