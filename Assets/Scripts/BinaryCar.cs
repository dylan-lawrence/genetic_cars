using UnityEngine;
using System.Collections;
using Vectrosity;

public class BinaryCar : MonoBehaviour {

	public int no_points;

	private VectorLine body;

	public void BuildCar(BinaryGenome genome) {
		//assume the car has 5 points in its body, first 5 genes
		#region car_body
		Vector2[] points = new Vector2[no_points];

		for (int i = 0; i < no_points; i++) {
			points[i] = new Vector2 (System.Convert.ToInt32(genome[i].gene.Substring(0,4),2),System.Convert.ToInt32(genome[i].gene.Substring(4,4),2));
		}

		points = Statics.CircleSort (points);

		GetComponent<PolygonCollider2D>().points = points;

		Vector3[] p = new Vector3[no_points + 1];

		for (int i = 0; i < no_points; i++)
			p [i] = (Vector3) points [i];

		p [no_points] = (Vector3)points [0];

		body = new VectorLine ("CarBody", p, null, 3.0f, LineType.Continuous, Joins.Fill);

		Color[] cols = new Color[no_points];

		for (int i = 0; i < no_points; i++)
			cols [i] = genome [i].color;

		body.smoothColor = true;
		body.SetColors (cols);
		body.collider = true;

		body.Draw3DAuto ();
		#endregion

		#region car_wheels



		#endregion
	}

	public void CarDestroy() {
		//Handles removing lines
		VectorLine.Destroy (ref body);

		Destroy (gameObject);
	}
}
