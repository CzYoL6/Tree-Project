using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private int index;
    public int val, depth;
    public Node lc, rc;
    public Node parent;
    public GameObject CurrespondingGameObject;
    public bool isExternal;
    public Node(int v, int ind, Node p, bool external)
    {
        val = v;
        lc = rc = null;
        parent = p;

        index = ind;

        isExternal = external;
    }
}
