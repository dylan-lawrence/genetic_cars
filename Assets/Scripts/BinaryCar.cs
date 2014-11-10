using UnityEngine;
using System.Collections;
using Vectrosity;

public class BinaryCar : MonoBehaviour {

	public int no_points;

	private VectorLine body;
	private VectorLine wheel_one;
	private VectorLine wheel_one_spoke;
	private VectorLine wheel_two;
	private VectorLine wheel_two_spoke;
		
	private bool scheduled_destroy = false;

	public void BuildCar(BinaryGenome genome) {
		//assume the car has 5 points in its body, first 5 genes
		#region car_body
		Vector2[] points = new Vector2[no_points];

		for (int i = 0; i < no_points; i++) {
			points[i] = new Vector2 (System.Convert.ToInt32(genome[i].gene.Substring(0,4),2)/2.0f,System.Convert.ToInt32(genome[i].gene.Substring(4,4),2)/2.0f);
		}

		points = Statics.CircleSort (points);

		GetComponent<PolygonCollider2D>().points = points;

		Vector3[] p = new Vector3[no_points + 1];

		for (int i = 0; i < no_points; i++)
			p [i] = (Vector3) points [i];

		p [no_points] = (Vector3)points [0];

		body = new VectorLine ("CarBody", p, null, 2.0f, LineType.Continuous, Joins.Fill);

		Color[] cols = new Color[no_points];

		for (int i = 0; i < no_points; i++)
			cols [i] = genome [i].color;

		body.smoothColor = true;
		body.SetColors (cols);
		body.drawTransform = transform;

		body.Draw3DAuto ();
		#endregion

		#region car_wheels

		WheelJoint2D[] joints = GetComponents<WheelJoint2D>();
		CircleCollider2D[] wheels = GetComponentsInChildren<CircleCollider2D>();

		//Wheel 1
		joints[0].anchor = points[System.Convert.ToInt32(genome[5].gene,2) % 5];
		wheels[0].radius = System.Convert.ToInt32(genome[6].gene, 2)/100.0f;

		wheel_one = new VectorLine("wheel_one", new Vector3[25], null, 2.0f, LineType.Continuous, Joins.Fill);
		wheel_one.MakeCircle(wheels[0].center, wheels[0].radius);
		wheel_one.SetColor (genome[6].color);
		wheel_one.drawTransform = wheels[0].transform;

		wheel_one.Draw3DAuto();

		Vector3[] p1 = new Vector3[] { wheels[0].center, Vector3.up * wheels[0].radius};
		wheel_one_spoke = new VectorLine("spoke_one", p1, null, 2.0f, LineType.Discrete);
		wheel_one_spoke.SetColor (genome[6].color);
		wheel_one_spoke.drawTransform = wheels[0].transform;

		wheel_one_spoke.Draw3DAuto();

		//Wheel 2
		joints[1].anchor = points[System.Convert.ToInt32(genome[7].gene,2) % 5];
		wheels[1].radius = System.Convert.ToInt32(genome[8].gene, 2)/100.0f;

		wheel_two = new VectorLine("wheel_two", new Vector3[25], null, 2.0f, LineType.Continuous, Joins.Fill);
		wheel_two.MakeCircle(wheels[1].center, wheels[1].radius);
		wheel_two.SetColor (genome[8].color);
		wheel_two.drawTransform = wheels[1].transform;

		wheel_two.Draw3DAuto();

		Vector3[] p2 = new Vector3[] { wheels[1].center, Vector3.up * wheels[1].radius};
		wheel_two_spoke = new VectorLine("spoke_two", p2, null, 2.0f, LineType.Discrete);
		wheel_two_spoke.SetColor (genome[8].color);
		wheel_two_spoke.drawTransform = wheels[1].transform;
		
		wheel_two_spoke.Draw3DAuto();

		#endregion
	}

	public void ScheduleDestroy() {
		scheduled_destroy = true;
		StartCoroutine (Kill ());
	}

	public void CancelDestroy() {
		scheduled_destroy = false;
	}

	public IEnumerator Kill() {
		yield return new WaitForSeconds (3.0f);
		if (scheduled_destroy) {
			VectorLine.Destroy (ref body);
			VectorLine.Destroy (ref wheel_one);
			VectorLine.Destroy (ref wheel_two);
			VectorLine.Destroy (ref wheel_one_spoke);
			VectorLine.Destroy (ref wheel_two_spoke);
			Destroy (gameObject);			
		}
	}

	public void CarDestroy() {
		//Handles removing lines
		VectorLine.Destroy (ref body);
		VectorLine.Destroy (ref wheel_one);
		VectorLine.Destroy (ref wheel_two);
		VectorLine.Destroy (ref wheel_one_spoke);
		VectorLine.Destroy (ref wheel_two_spoke);

		if (gameObject != null)
			Destroy (gameObject);
	}
}
