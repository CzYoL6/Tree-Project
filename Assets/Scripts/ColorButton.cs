using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setColor()
    {
        if (NodeSprites.Instance.Target == null) return;
        Color color = image.color;
        //Color comColor = new Color(1f - color.r / 255f, 1f - color.g / 255f, 1f - color.b / 255f);
        Color comColor = color == Color.white ? new Color(60/255f, 60/255f, 60/255f, 1f) : Color.white;
        NodeSprites.Instance.SetNodeColor(NodeSprites.Instance.Target,color, comColor);
        NodeSprites.Instance.Target = null;
    }
}
