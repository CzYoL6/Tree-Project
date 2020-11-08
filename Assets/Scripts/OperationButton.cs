using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationButton : MonoBehaviour
{
    public void LeftRotate()
    {
        if (NodeSprites.Instance.Target == null) return;
        Tree.Instance.rotate(1);
        NodeSprites.Instance.Target = null;
    }
    public void RightRotate()
    {
        if (NodeSprites.Instance.Target == null) return;
        Tree.Instance.rotate(2);
        NodeSprites.Instance.Target = null;
    }
    public void Delete()
    {
        if (NodeSprites.Instance.Target == null) return;
        Node target = NodeSprites.Instance.Target.GetComponent<NodeProperties>().CurrespondingNode;
        if (target == null) return;

        
        Node p = target.parent;

        if (p == null)
        {
            Tree.Instance.Delete(ref Tree.Instance.root);
            /*if(Tree.Instance.root == null && NodeSprites.Instance.showExternal)
            {
                Tree.Instance.InsertExternalNode(ref Tree.Instance.root, null);
            }*/
        }
        else
        {
            if (p.lc == target)
            {
                Tree.Instance.Delete(ref p.lc);
                if (p.lc == null && NodeSprites.Instance.showExternal)
                {
                    Tree.Instance.InsertExternalNode(ref p.lc, p);
                }
            }
            else
            {
                Tree.Instance.Delete(ref p.rc);
                if (p.rc == null && NodeSprites.Instance.showExternal)
                {
                    Tree.Instance.InsertExternalNode(ref p.rc, p);
                }
            }
        }
        Tree.Instance.UpdateGraphics();
        NodeSprites.Instance.Target = null;
    }
}
