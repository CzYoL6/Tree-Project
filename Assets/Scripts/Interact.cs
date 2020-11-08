using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        //Debug.Log("!!!");
        NodeSprites.Instance.Target = gameObject;
        //if (Tree.Instance.target != null) Debug.Log(Tree.Instance.target.val);
    }

    
}
