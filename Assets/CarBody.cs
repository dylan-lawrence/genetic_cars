using UnityEngine;
using System.Collections;
using Vectrosity;

public class CarBody : MonoBehaviour {

	VectorLine myline;

	// Use this for initialization
	void Start () {
		Vector2[] points = GetComponent<PolygonCollider2D> ().points;
		Vector3[] points3 = new Vector3[points.Length];

		for (int i = 0; i<points.Length; i++)
			points3 [i] = (Vector3)points [i];

		Vector3[] p = new Vector3[points.Length + 1];
		myline = new VectorLine ("CarBody", p, null, 2.0f, LineType.Continuous, Joins.Weld);

		myline.MakeSpline (points3, points.Length, true);

		myline.SetColor (Color.yellow);
		myline.drawTransform = transform;
		myline.Draw3D ();
	}

	// Update is called once per frame
	void Update () {

	}

	void LateUpdate() {
		myline.Draw3D ();
	}
}
