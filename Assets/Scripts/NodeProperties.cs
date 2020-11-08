using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeProperties : MonoBehaviour
{
    public int index;
    public int value;
    public Color valueTextColor {
        get => valueText.color;
        set { }
    }
    public Color bgColor
    {
        get => bgSprite.color;
        set { }
    }



    private TextMeshPro valueText;
    public  SpriteRenderer bgSprite;
    private LineRenderer lineRenderer;
    private Vector3 targetPosition;
    private bool moving;

    public Node CurrespondingNode;
    

    // Start is called before the first frame update
    void Awake()
    {
        valueText = GetComponent<TextMeshPro>();
        lineRenderer = GetComponent<LineRenderer>(); 
    }

    private void Start()
    {
        
    }

    public void Init(int ind, int val, Color bg_Color, Color textColor)
    {
        index = ind;
        value = val;
        valueTextColor = textColor;
        ChangeValueTextColor(textColor);
        bgColor = bg_Color;
        ChangeBgColor(bg_Color);
        setText(val.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        /*        setText(index.ToString());
                ChangeIndexTextColor(valueTextColor);
                ChangeBgColor(bgColor);
        */

        if (moving)
        {
           // Debug.Log("!!!");
            transform.position = Vector3.MoveTowards(transform.position, targetPosition,  NodeSprites.Instance.speed);
            if(transform.position == targetPosition)
            {
                moving = false;
            }
        }

        lineRenderer.SetPosition(0, transform.position);
        if(GetComponent<NodeRelations>().parent != null)
            lineRenderer.SetPosition(1, GetComponent<NodeRelations>().parent.transform.position);
        else lineRenderer.SetPosition(1, transform.position);

    }

    public void ChangeValueTextColor(Color color)
    {
        valueText.color = color;
    }

    public void ChangeBgColor(Color color)
    {
        bgSprite.color = color;
    }

    public void setText(string s)
    {
        valueText.text = s;
    }

    /*    public void setEnd(GameObject end)
        {
            lineRenderer.SetPosition(1, end.transform.position);
        }*/

    public void MoveToPosition(Vector3 t)
    {
        targetPosition = t;
        moving = true;
    }
}
