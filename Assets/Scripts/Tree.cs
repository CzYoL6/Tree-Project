using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public static Tree Instance { get; private set; }

    public int nodeCnt = 0;
    private int maxHeight;
    public Node root;
    //public Dictionary<Node, GameObject> dict;



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;


        

        //StartCoroutine(addNewNode());


        
    }

    public void Insert(ref Node rt, int val, Node p)
    {
        if (rt != null && rt.isExternal)
        {
            MultipleTargetCamera.Instance.externalTargets.Remove(rt.CurrespondingGameObject.transform);
            Delete(ref rt);
        }
         if (rt == null)
        {
            rt = new Node(val, ++nodeCnt, p, false);

            rt.parent = p;

            GameObject newNode = NodeSprites.Instance.NewNode(nodeCnt, val, Color.white, new Color(60 / 255f, 60 / 255f, 60 / 255f, 1f), p, false);
            MultipleTargetCamera.Instance.targets.Add(newNode.transform);

            rt.CurrespondingGameObject = newNode;
            newNode.GetComponent<NodeProperties>().CurrespondingNode = rt;

            if (NodeSprites.Instance.showExternal)
            {
                InsertExternalNode(ref rt.lc, rt);
                InsertExternalNode(ref rt.rc, rt);
            }

            return;
        }
        if (rt.val > val)
        {
            Insert(ref rt.lc, val, rt);
            
        }
        else if (rt.val < val) Insert(ref rt.rc, val, rt);
    }

    public void InsertExternalNode(ref Node rt, Node p)
    {
        rt = new Node(0, ++nodeCnt, p, true);
        rt.parent = p;

        GameObject newNode = NodeSprites.Instance.NewNode(nodeCnt, 0, new Color(60 / 255f, 60 / 255f, 60 / 255f, 1f), new Color(60 / 255f, 60 / 255f, 60 / 255f, 1f), p, true);
        MultipleTargetCamera.Instance.externalTargets.Add(newNode.transform);

        rt.CurrespondingGameObject = newNode;
        newNode.GetComponent<NodeProperties>().CurrespondingNode = rt;

    }

    /*    public Node GetPredecessor(Node rt)
        {
            if (rt == null) return null;
            Node ret = rt.lc;
            if (ret == null) return null;
            while (ret.rc != null) ret = ret.rc;
            return ret;
        }

        public Node GetSuccessor(Node rt)
        {
            if (rt == null) return null;
            Node ret = rt.rc;
            if (ret == null) return null;
            while (ret.lc != null) ret = ret.lc;
            return ret;
        }*/

    public void Delete(ref Node node)
    {
        if (node == null) return;

        
        Debug.Log(node.val.ToString() + " " + (node.lc ==null).ToString() + " " + (node.rc == null).ToString());

        if (node.lc != null && node.lc.isExternal)
        {
            MultipleTargetCamera.Instance.externalTargets.Remove(node.lc.CurrespondingGameObject.transform);
            Delete(ref node.lc);

        }
        if (node.rc != null && node.rc.isExternal)
        {
            MultipleTargetCamera.Instance.externalTargets.Remove(node.rc.CurrespondingGameObject.transform);
            Delete(ref node.rc);
            
        }/*
        if(NodeSprites.Instance.showExternal && node.isExternal && node.lc == null && node.rc == null)
        {
            InsertExternalNode(ref node, node.parent);
        }*/
        if (node.lc == null)
        {
            Node nrc = node.rc;
            if (node.rc != null) node.rc.parent = node.parent;
            DeleteDataAndGraphic(ref node);
            node = nrc;
            
        }
        else if (node.rc == null)
        {
            Node nlc = node.lc;
            if (node.lc != null) node.lc.parent = node.parent;
            DeleteDataAndGraphic(ref node);
            node = nlc;
            
        }
        else DeleteNodeWithTwoChildren(node, ref node.lc);
        UpdateGraphics();
    }

    private void DeleteNodeWithTwoChildren(Node node, ref Node r)
    {
        if (!NodeSprites.Instance.showExternal && r.rc != null)
        {
                DeleteNodeWithTwoChildren(node, ref r.rc);
        }
        else if(NodeSprites.Instance.showExternal && r.rc != null && !r.rc.isExternal)
        {
            
            DeleteNodeWithTwoChildren(node, ref r.rc);
        }
        else
        {
            if(NodeSprites.Instance.showExternal && r.rc != null && r.rc.isExternal)
            {

                MultipleTargetCamera.Instance.externalTargets.Remove(r.rc.CurrespondingGameObject.transform);
                Delete(ref r.rc);
            }
            NodeProperties np = r.CurrespondingGameObject.GetComponent<NodeProperties>();
            node.val = r.val;
            NodeSprites.Instance.SetNodeColor(node.CurrespondingGameObject, np.bgColor, np.valueTextColor);
            NodeSprites.Instance.SetNodeValue(node.CurrespondingGameObject, r.val);
            Node rlc = r.lc;
            if (r.lc != null) r.lc.parent = r.parent;
            DeleteDataAndGraphic(ref r);
            r = rlc;
            
        }
    }

    /**
     * 删除数据和图像
     */
    private void DeleteDataAndGraphic(ref Node node)
    {
        if (node == null) return;
        MultipleTargetCamera.Instance.targets.Remove(node.CurrespondingGameObject.transform);
        NodeSprites.Instance.DeleteNode(node.CurrespondingGameObject);
        node = null; 
    }

    /**
     * 后根遍历并对节点执行操作
     */
    public void Dfs_Post(Node rt, int dep, Action<Node> action = null)
    {
        if (rt == null) return;
        rt.depth = dep;
        if (rt.lc != null) Dfs_Post(rt.lc, dep + 1, action);
        if (rt.rc != null) Dfs_Post(rt.rc, dep + 1, action);
        action?.Invoke(rt);
    }

    public void Dfs_Post_Place(Node rt, int dep, int[] cd, float d)
    {
        if (rt == null)
        {
            for (int i = 1; i <= maxHeight - dep + 1; i++)
                cd[dep - 1 + (i - 1)] += (int)Math.Pow(2, i - 1);
            return;
        }

        NodeSprites.Instance.PlaceTheNode(rt.CurrespondingGameObject, ++cd[rt.depth - 1], rt.depth, maxHeight, d);
        //Debug.Log("val: " + rt.val + ", cd: " + cd[rt.depth - 1]);
        Dfs_Post_Place(rt.lc, dep + 1, cd, d);
        Dfs_Post_Place(rt.rc, dep + 1, cd, d);
        

    }

    void Update()
    {
        
    }

    IEnumerator addNewNode()
    {
        int[] toadd = { 8, 9, 3, 7, 2, 4, 5, 10, 11, 1 };
        for (int i = 0; i < toadd.Length; i++)
        {
            Insert(ref root, toadd[i], null);

            UpdateGraphics();
            yield return new WaitForSeconds(0.3f);
        }

        
    }

    //public Node target;
    public void rotate(int T)
    {
        Node target = NodeSprites.Instance.Target.GetComponent<NodeProperties>().CurrespondingNode;
        if (target == null) return;
        if (T == 1)
            LeftRotate(ref target);
        else if (T == 2) RightRotate(ref target);
        //Debug.Log("root " + root.val);
        UpdateGraphics();
    }

    void RightRotate(ref Node node)
    {
        if (node == null) return;
        if (!NodeSprites.Instance.showExternal && node.lc == null) return;
        if (NodeSprites.Instance.showExternal &&  node.lc.isExternal ) return;
        Node parent = node.parent;
        Node lc = node.lc;
        Node lc_rc = lc.rc;

        node.parent = lc;
        if (lc != null)
        {
            lc.parent = parent;
            lc.rc = node;
        }
        if (lc_rc != null)
        { 
            lc_rc.parent = node;
            
        }
        node.lc = lc_rc;

        if (parent != null)
        {
            if (parent.lc == node) parent.lc = lc;
            else if (parent.rc == node) parent.rc = lc;
        }

        if(node == root)
        {
            root = lc;
        }
    }

    void LeftRotate(ref Node node)
    {
        if (node == null) return;
        if (!NodeSprites.Instance.showExternal && node.rc == null) return;
        if (NodeSprites.Instance.showExternal && node.rc.isExternal) return;
        Node parent = node.parent;
        Node rc = node.rc;
        Node rc_lc = rc.lc;

        node.parent = rc;
        if (rc != null)
        {
            rc.parent = parent;
            rc.lc = node;
        }
        if (rc_lc != null)
        {
            rc_lc.parent = node;
            
        }
        node.rc = rc_lc;
        if (parent != null)
        {
            if (parent.lc == node) parent.lc = rc;
            else if (parent.rc == node) parent.rc = rc;
        }
        if (node == root)
        {
            root = rc;
        }
    }

    public void UpdateGraphics()
    {
        maxHeight = 0;

        //Debug.Log("post :");
        //Dfs_Post(root, 1, n => { Debug.Log(n.val); });

        Dfs_Post(root, 1, n => { maxHeight = Math.Max(maxHeight, n.depth); n.CurrespondingGameObject.GetComponent<NodeRelations>().setParent(n.parent != null?n.parent.CurrespondingGameObject:null); }) ;

        // Debug.Log(maxHeight);
        int[] currentIndexInEachDepth = new int[maxHeight];


        Dfs_Post_Place(root, 1, currentIndexInEachDepth, 1.5f * NodeSprites.Instance.D);
    }
}
