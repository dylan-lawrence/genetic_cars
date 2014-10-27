using UnityEngine;
using System.Collections;
using Vectrosity;

public class CarBody : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector2[] p = new Vector2[240];
		VectorLine body = new VectorLine ("CarBody", p, null, 2.0f, LineType.Continuous, Joins.Weld);
		
		Vector2[] points = new Vector2[] {
			new Vector2(Random.Range(-1.0f,-.66f),Random.Range (0.0f,1.0f)) * 100,
			new Vector2(Random.Range(-.66f,-.33f),Random.Range (0.0f,1.0f)) * 100,
			new Vector2(Random.Range(-.33f,0.0f),Random.Range (0.0f,1.0f)) * 100,
			new Vector2(Random.Range(0.0f,.33f),Random.Range (0.0f,1.0f)) * 100,
			new Vector2(Random.Range(.33f, .66f),Random.Range (0.0f, 1.0f)) * 100,
			new Vector2(Random.Range(.66f,1.0f),Random.Range (0.0f,1.0f)) * 100,
			new Vector2(Random.Range(.66f,1.0f),Random.Range (-1.0f,0.0f)) * 100,
			new Vector2(Random.Range(.33f,.66f),Random.Range (-1.0f,0.0f)) * 100,
			new Vector2(Random.Range(0.0f,.33f),Random.Range (-1.0f,0.0f)) * 100,
			new Vector2(Random.Range(-.33f,0.0f),Random.Range (-1.0f,0.0f)) * 100,
			new Vector2(Random.Range(-.66f,-.33f),Random.Range (-1.0f,0.0f)) * 100,
			new Vector2(Random.Range(-1.0f,-.66f),Random.Range (-1.0f,0.0f)) * 100				
		};
		
		body.MakeSpline (points, 239, true);
		
		body.color = Color.red;
		body.Draw();
		
		Vector2[] p2 = new Vector2[100];
		VectorLine wheel_one = new VectorLine("WheelOne", p2, null, 2.0f, LineType.Continuous, Joins.Weld);
		
		wheel_one.MakeCircle(points[0], Random.Range (0.1f,1.0f) * 50);
		wheel_one.SetColor (Color.blue);
		wheel_one.Draw ();
		
		VectorPoints center = new VectorPoints("Hub", points, null, 3.0f);
		center.SetColor(Color.yellow);
		center.Draw ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
