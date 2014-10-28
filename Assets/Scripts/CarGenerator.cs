using UnityEngine;
using System.Collections;

public class CarGenerator : MonoBehaviour {

	public GameObject CarBody;
	public GameObject Wheel;

	private GameObject body;
	private GameObject wheel1;
	private GameObject wheel2;

	private Genome genome;

	// Use this for initialization
	void Start () {
		body = (GameObject) Instantiate (CarBody, new Vector2 (0, 0), Quaternion.identity);
		wheel1 = (GameObject) Instantiate (Wheel, new Vector2 (0, 0), Quaternion.identity);
		wheel2 = (GameObject) Instantiate (Wheel, new Vector2 (0, 0), Quaternion.identity);

		wheel1.transform.parent = body.transform;
		wheel2.transform.parent = body.transform;

		genome = new Genome ();

		// Genome 0 - 48 :: x,y specifiers for poly points
		// Genome 48 - 52 :: wheel 1 point select
		// Genome 52 - 56 :: wheel 1 radius

		genome.GenerateGenome (32+48);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
