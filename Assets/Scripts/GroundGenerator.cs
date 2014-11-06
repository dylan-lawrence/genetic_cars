using UnityEngine;
using System.Collections;
using Vectrosity;

public class GroundGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3[] p = new Vector3[1000];

		Vector3 curr = Vector3.zero;
		for (int i = 0; i < 1000; i++) {
			p[i] = curr;
			if (i > 20)
				curr += new Vector3(1, Random.Range (-0.5f, 0.5f));
			else
				curr += Vector3.right;
		}

		VectorLine myline = new VectorLine ("ground", p, null, 5.0f, LineType.Continuous, Joins.Fill);

		myline.SetColor (Color.green);
		myline.collider = true;
		myline.Draw3D ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
