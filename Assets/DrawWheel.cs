using UnityEngine;
using System.Collections;
using Vectrosity;

public class DrawWheel : MonoBehaviour {
	
	VectorLine myline;	
	bool drawn = false;
	
	// Use this for initialization
	void Start () {
		StartCoroutine (Draw());
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void LateUpdate() {
		if (drawn)
			myline.Draw3D ();
	}
	
	IEnumerator Draw() {
		yield return new WaitForSeconds(0.25f);
		Vector3[] p = new Vector3[100];
		myline = new VectorLine("Wheel", p, null, 2.0f, LineType.Continuous, Joins.Weld);
		myline.MakeCircle(transform.localPosition, GetComponent<CircleCollider2D>().radius);
		myline.SetColor (Color.black);
		myline.drawTransform = transform.parent;
		myline.Draw3D ();
		drawn = true;
	}
}
