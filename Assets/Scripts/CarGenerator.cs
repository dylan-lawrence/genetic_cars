using UnityEngine;
using System.Collections;

public class CarGenerator : MonoBehaviour {

	public GameObject carBody;
	public GameObject wheel;

	private GameObject body;
	private GameObject wheel1;
	private GameObject wheel2;

	private Genome genome;

	// Use this for initialization
	void Start () {
		body = (GameObject) Instantiate (carBody, new Vector2 (5, 3), Quaternion.identity);
		wheel1 = (GameObject) Instantiate (wheel, new Vector2 (0, 0), Quaternion.identity);
		wheel2 = (GameObject) Instantiate (wheel, new Vector2 (0, 0), Quaternion.identity);

		wheel1.transform.parent = body.transform;
		wheel2.transform.parent = body.transform;

		//So we can avoid wonky physics in setup
		body.rigidbody2D.isKinematic = true;

		GetComponent<Follow> ().target = body;

		genome = new Genome ();

		// Gene 0 - 11 :: x,y specifiers for poly points
		// Gene 12 :: wheel 1 point select
		// Gene 13 :: wheel 1 radius
		// Gene 14 :: wheel 2 point select
		// Gene 15 :: wheel 2 radius

		genome.GenerateGenome (64);

		Vector2[] points = new Vector2[6];
		for (int i=0; i<12; i+=2) {
			points[i/2] = new Vector2((Genome.GeneToInt(genome[i]) - 127.5f)/(127.5f/1.5f),(Genome.GeneToInt(genome[i+1]) - 127.5f)/(127.5f/1.5f));
		}
		body.GetComponent<PolygonCollider2D>().points = CarBody.CircleSort(points);

		WheelJoint2D[] wheels = body.GetComponents<WheelJoint2D> ();

		wheels [0].connectedBody = wheel1.rigidbody2D;
		wheels [0].anchor = points [(int) Mathf.Round (Genome.GeneToInt(genome [12]) / 255.0f)];
		wheels [1].connectedBody = wheel2.rigidbody2D;
		wheels [1].anchor = points [(int) Mathf.Round (Genome.GeneToInt(genome [14]) / 255.0f)];

		wheel1.GetComponent<CircleCollider2D> ().radius = Genome.GeneToInt (genome [13]) / 255.0f;
		wheel2.GetComponent<CircleCollider2D> ().radius = Genome.GeneToInt (genome [15]) / 255.0f;

		StartCoroutine (BeginPhys ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator BeginPhys() {
		yield return new WaitForSeconds(0.25f);
		body.rigidbody2D.isKinematic = false;
	}
}
