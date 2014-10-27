using UnityEngine;
using System.Collections;
using Vectrosity;

public class DrawWheel : MonoBehaviour {
	
	VectorLine myline;	
	VectorLine spokes;
	bool drawn = false;
	
	// Use this for initialization
	void Start () {
		StartCoroutine (Draw());
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void LateUpdate() {
		if (drawn) {
			myline.Draw3D ();
			spokes.Draw3D ();
		}
	}
	
	IEnumerator Draw() {
		yield return new WaitForSeconds(0.25f);

		float radius = GetComponent<CircleCollider2D> ().radius;

		Vector3[] p = new Vector3[100];
		myline = new VectorLine("Wheel", p, null, 2.0f, LineType.Continuous, Joins.Weld);
		myline.MakeCircle(Vector3.zero, radius);
		myline.SetColor (Color.black);
		myline.drawTransform = transform;
		myline.Draw3D ();

		Vector3[] p2 = new Vector3[8];
		//--
		p2 [0] = Vector2.zero;
		p2[1] = new Vector2 (Vector2.right.x * radius, Vector2.right.y * radius);
		p2[2] = Vector2.zero;
		p2[3] = new Vector2 (-Vector2.right.x * radius, -Vector2.right.y * radius);
		p2[4] = Vector2.zero;
		p2[5] = new Vector2 (Vector2.up.x * radius, Vector2.up.y * radius);
		p2[6] = Vector2.zero;
		p2[7] = new Vector2 (-Vector2.up.x * radius, -Vector2.up.y * radius);
		//--
		spokes = new VectorLine("Spokes", p2, null, 2.0f, LineType.Discrete, Joins.Weld);
		spokes.SetColor (Color.black);
		spokes.drawTransform = transform;
		spokes.Draw3D ();

		drawn = true;
	}
}
