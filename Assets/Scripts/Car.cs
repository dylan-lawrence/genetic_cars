using UnityEngine;
using System.Collections;
using Vectrosity;

public class Car : MonoBehaviour {
	
	public Genome genome;
	public Color car_color;
	
	VectorLine body_outline;
	VectorLine body_inner;
	VectorLine wheel_one_circle;
	VectorLine wheel_one_spokes;
	VectorLine wheel_two_circle;
	VectorLine wheel_two_spokes;
	
	public void SetupCar (Genome g) {
		Vector2[] points = new Vector2[6];
		for (int i=0; i<12; i+=2) {
			points[i/2] = new Vector2((Genome.GeneToInt(g[i]) - 127.5f)/(127.5f/1.5f),(Genome.GeneToInt(g[i+1]) - 127.5f)/(127.5f/1.5f));
		}
		
		GetComponent<PolygonCollider2D>().points = CarBody.CircleSort(points);
		
		WheelJoint2D[] wheels = GetComponents<WheelJoint2D>();
		wheels [0].anchor = points [(int) Mathf.Round (Genome.GeneToInt(g [12]) / 255.0f)];
		wheels [1].anchor = points [(int) Mathf.Round (Genome.GeneToInt(g [14]) / 255.0f)];
		
		CircleCollider2D[] circs = GetComponentsInChildren<CircleCollider2D>();
		circs[0].radius = Genome.GeneToInt (g [13]) / 255.0f;
		circs[1].radius = Genome.GeneToInt (g [15]) / 255.0f;
	}
	
	public void SetupLines() {		
		//Body outline
		int poly_points = GetComponent<PolygonCollider2D>().points.Length;
		Debug.Log (poly_points);
		
		Vector3[] p1 = new Vector3[poly_points + 1];
		body_outline = new VectorLine("Body", p1, null, 3.0f, LineType.Continuous, Joins.Fill);
		Vector3[] points3 = new Vector3[poly_points];
		for (int i = 0; i<poly_points; i++)
			points3[i] = (Vector3) GetComponent<PolygonCollider2D>().points[i];
			
		body_outline.MakeSpline(points3, poly_points, true);
		body_outline.SetColor (car_color);
		body_outline.drawTransform = transform;
		
		//Body inner
		Vector3 center = Vector3.zero;
		for (int i = 0; i<poly_points; i++) {
			center.x += GetComponent<PolygonCollider2D>().points[i].x;
			center.y += GetComponent<PolygonCollider2D>().points[i].y;
		}
		center.x = center.x/poly_points;
		center.y = center.y/poly_points;
		
		Vector3[] p2 = new Vector3[poly_points * 2];
		for (int i = 0; i < poly_points * 2; i+=2) {
			p2[i] = center;
			p2[i+1] = (Vector3) GetComponent<PolygonCollider2D>().points[i/2];
		}
		
		body_inner = new VectorLine("BodyInner", p2, null, 3.0f, LineType.Discrete);
		
		body_inner.SetColor (car_color);
		body_inner.drawTransform = transform;
		
		//Wheel one circle
		Vector3[] p3 = new Vector3[20];
		wheel_one_circle = new VectorLine("WheelOne", p3, null, 3.0f, LineType.Continuous, Joins.Fill);
		
		wheel_one_circle.MakeCircle (Vector3.zero, GetComponentsInChildren<CircleCollider2D>()[0].radius);
		wheel_one_circle.SetColor (Color.gray);
		wheel_one_circle.drawTransform = transform.GetChild (0);
		
		//Wheel one spoke
		Vector3[] p4 = new Vector3[4];
		p4[0] = Vector3.up * GetComponentsInChildren<CircleCollider2D>()[0].radius;
		p4[1] = -Vector3.up * GetComponentsInChildren<CircleCollider2D>()[0].radius;
		p4[2] = Vector3.right * GetComponentsInChildren<CircleCollider2D>()[0].radius;
		p4[3] = -Vector3.right * GetComponentsInChildren<CircleCollider2D>()[0].radius;
		
		wheel_one_spokes = new VectorLine("WheelOneSpokes", p4, null, 3.0f, LineType.Discrete);
		wheel_one_spokes.SetColor(Color.gray);
		wheel_one_spokes.drawTransform = transform.GetChild (0);
		
		//Wheel two circle
		Vector3[] p5 = new Vector3[20];
		wheel_two_circle = new VectorLine("WheelTwo", p5, null, 3.0f, LineType.Continuous, Joins.Fill);
		
		wheel_two_circle.MakeCircle (Vector3.zero, GetComponentsInChildren<CircleCollider2D>()[1].radius);
		wheel_two_circle.SetColor (Color.black);
		wheel_two_circle.drawTransform = transform.GetChild (1);
		
		//Wheel two spoke
		Vector3[] p6 = new Vector3[4];
		p6[0] = Vector3.up * GetComponentsInChildren<CircleCollider2D>()[1].radius;
		p6[1] = -Vector3.up * GetComponentsInChildren<CircleCollider2D>()[1].radius;
		p6[2] = Vector3.right * GetComponentsInChildren<CircleCollider2D>()[1].radius;
		p6[3] = -Vector3.right * GetComponentsInChildren<CircleCollider2D>()[1].radius;
		
		wheel_two_spokes = new VectorLine("WheelTwoSpokes", p6, null, 3.0f, LineType.Discrete);
		wheel_two_spokes.SetColor(Color.black);
		wheel_two_spokes.drawTransform = transform.GetChild (1);
		
		body_outline.Draw3DAuto();
		body_inner.Draw3DAuto();
		wheel_one_circle.Draw3DAuto();
		wheel_one_spokes.Draw3DAuto();
		wheel_two_circle.Draw3DAuto();
		wheel_two_spokes.Draw3DAuto();
	}
	
	public void ClearLines() {
		body_outline.active = false;
		body_inner.active = false;
		wheel_one_circle.active = false;
		wheel_one_spokes.active = false;
		wheel_two_circle.active = false;
		wheel_two_spokes.active = false;
	}
	
	void Start() {
		//Genome g = new Genome();
		//g.GenerateGenome(64);
		//SetupCar (g);
		//SetupLines();
	}
}
