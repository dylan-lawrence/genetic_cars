using UnityEngine;
using System.Collections;
using Vectrosity;

public class GroundGenerator : MonoBehaviour {

	public PhysicsMaterial2D mat;

	// Use this for initialization
	void Start () {
		Vector3[] po = new Vector3[5000];
		Vector3[] p = new Vector3[2500];

		Vector3 curr = new Vector3 (-10, 0, 0);
		for (int i = 0; i < 2500; i++) {
			p[i] = curr;
			curr += new Vector3(1.65f, Random.Range (-1.0f, 1.0f));
		}

		VectorLine myline = new VectorLine ("ground", po, null, 5.0f, LineType.Continuous, Joins.Fill);

		myline.MakeSpline (p);

		myline.SetColor (Color.green);
		myline.physicsMaterial = mat;
		myline.collider = true;
		myline.Draw3D ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
