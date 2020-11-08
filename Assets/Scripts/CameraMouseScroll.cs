using UnityEngine;
using System.Collections;

public class CameraMouseScroll : MonoBehaviour
{
	public float ScaleSpeed;
	void Start()
	{

	}


	void Update()
	{
		//Zoom out
		if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 0.01)
		{
			Camera.main.orthographicSize -= ScaleSpeed * Time.deltaTime;
			Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 0.01f, 20);
		}
		//Zoom in
		if (Input.GetAxis("Mouse ScrollWheel")< 0 && Camera.main.orthographicSize < 20)
		{
			Camera.main.orthographicSize += ScaleSpeed * Time.deltaTime;
			Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 0.01f, 20);
		}

        /* if (Input.GetKeyDown(KeyCode.E))
         {
             Tree.Instance.rotate(1);
         }
         if (Input.GetKeyDown(KeyCode.R))
         {
             Tree.Instance.rotate(2);
         }
         if (Input.GetKeyDown(KeyCode.T))
         {
             Tree.Instance.Delete(ref Tree.Instance.root);
         }*/
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
			NodeSprites.Instance.showExternal = !NodeSprites.Instance.showExternal;
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
        }*/
    }
}
