using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InsertField : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField val_Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Insert()
    {
        if (val_Text.text.Equals("")) return;
        int val = int.Parse(val_Text.text);
        Tree.Instance.Insert(ref Tree.Instance.root, val, null);
        Tree.Instance.UpdateGraphics();
    }
}
