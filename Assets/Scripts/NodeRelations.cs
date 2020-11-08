using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRelations : MonoBehaviour
{
    public GameObject parent;
    private GameObject[] children;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setParent(GameObject p)
    {
        parent = p;
        //GetComponent<NodeProperties>().setEnd(parent);
    }
}
