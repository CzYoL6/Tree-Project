using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeSprites : MonoBehaviour
{
    public static NodeSprites Instance { get; private set; }
    public GameObject nodePrefab, externalNodePrefab;
    [HideInInspector]
    public float D;
    public float speed;
    private GameObject target;

    public bool showExternal;
    public GameObject Target
    {
        get { return target; }
        set
        {
            if (value == null)
            {
                target.transform.localScale = new Vector3(1, 1, 1);
                target = null;
            }
            else
            {
                if(target != null) target.transform.localScale = new Vector3(1, 1, 1);
                target = value;
                target.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        D = nodePrefab.GetComponentInChildren<SpriteRenderer>().size.x;
        //D = 1s
     //   Debug.Log("D: " + D);
    }

    // Update is called once per frame
    void Update()
    {

    }


    /**
     * 创建新的节点
     */
    public GameObject NewNode(int ind, int val, Color bgColor, Color textColor, Node p, bool isExternal)
    {
        GameObject newNode = isExternal ? Instantiate(externalNodePrefab, transform.position, Quaternion.identity) : Instantiate(nodePrefab, transform.position, Quaternion.identity);

        newNode.GetComponent<NodeProperties>().Init(ind, val, bgColor, textColor);

        if(p != null)
            newNode.GetComponent<NodeRelations>().parent = p.CurrespondingGameObject;

        return newNode;
    }

    public void PlaceTheNode(GameObject node, int i, int h, int H, float d)
    {
        //  Debug.Log(i + " " + h + " " + H + " "+ d);
        //  Debug.Log((i - 1) * d - (d * Mathf.Pow(2, H - h) * (Mathf.Pow(2, h - 1) - 1)) / 2.0f);

        node.GetComponent<NodeProperties>().MoveToPosition(new Vector3((i - 1) * d * Mathf.Pow(2, H - h) - (d * Mathf.Pow(2, H - h) * (Mathf.Pow(2, h - 1) - 1)) / 2.0f,
            -d * (h - 1), node.transform.position.z));

    }

    public void SetNodeColor(GameObject node, Color bgColor, Color textColor)
    {
        NodeProperties t = node.GetComponent<NodeProperties>();
        t.ChangeBgColor(bgColor);
        t.ChangeValueTextColor(textColor);
    }

    public void SetNodeValue(GameObject node ,int val)
    {
        NodeProperties t = node.GetComponent<NodeProperties>();
        t.setText(val.ToString());
    }

    public void DeleteNode(GameObject node)
    {
        Destroy(node);
    }
}
