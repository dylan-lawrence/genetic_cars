using UnityEngine;
using System.Collections;
using Vectrosity;

public class DrawRect : MonoBehaviour {
	
	VectorLine myline;	
	
	// Use this for initialization
	void Start () {
		Vector2 size = GetComponent<BoxCollider2D>().size;
	
		Vector3[] p = new Vector3[5];
		myline = new VectorLine("Body", p, null, 2.0f, LineType.Continuous, Joins.Weld);
		myline.MakeRect (new Rect(-size.x/2.0f,-size.y/2.0f, size.x, size.y));
		myline.SetColor (Color.yellow);
		myline.drawTransform = transform;
		
		myline.Draw3D ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void LateUpdate() {
		myline.Draw3D();
	}
}
