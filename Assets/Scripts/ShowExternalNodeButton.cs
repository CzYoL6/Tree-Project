using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowExternalNodeButton : MonoBehaviour
{
    public Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setExternalNode()
    {
        NodeSprites.Instance.showExternal = toggle.isOn;
        if (!NodeSprites.Instance.showExternal)
        {
            foreach (var x in MultipleTargetCamera.Instance.externalTargets)
            {
                Node y = x.GetComponent<NodeProperties>().CurrespondingNode;
                if (y.parent.lc == y) Tree.Instance.Delete(ref y.parent.lc);
                else Tree.Instance.Delete(ref y.parent.rc);
            }
            MultipleTargetCamera.Instance.externalTargets.Clear();
        }
        else
        {
            Tree.Instance.Dfs_Post(Tree.Instance.root, 1, n =>
            {
                if (n.lc == null) Tree.Instance.InsertExternalNode(ref n.lc, n);
                if (n.rc == null) Tree.Instance.InsertExternalNode(ref n.rc, n);
            });
        }
        Tree.Instance.UpdateGraphics();
    }
}
