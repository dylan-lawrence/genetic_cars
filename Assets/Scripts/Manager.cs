using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	
	public int number_of_cars;
	public GameObject car;
	
	private Follow follow;
	
	private GameObject[] cars;
	private GameObject furthest;
	
	private bool can_start_tracking = false;
	// Use this for initialization
	void Start () {
		follow = gameObject.AddComponent<Follow>();
		cars = new GameObject[number_of_cars];
		
		for (int i = 0; i<number_of_cars; i++) {
			cars[i] = (GameObject) Instantiate (car, new Vector3(2,2,0), Quaternion.identity);
			cars[i].name = "Car " + i;
		}
		
	}
	
	void Update() {
		furthest = cars[0];
		for (int i = 1; i < number_of_cars; i++) {
			if (furthest.transform.GetChild(0).position.x < cars[i].transform.GetChild(0).position.x)
				furthest = cars[i];
		}
		follow.target = furthest.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
