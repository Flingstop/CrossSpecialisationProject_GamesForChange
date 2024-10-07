using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{

    GameObject cutting;
    float enterHeight;

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
            Debug.Log("Hit" + other.gameObject);
            cutting = other.gameObject;
            enterHeight = transform.position.y;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == cutting)
        {
            if (transform.position.y < enterHeight)
            {
                Debug.Log("Cut" + other.gameObject);
            }
            cutting = null;
        }
    }
}
