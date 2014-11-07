using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monitor : MonoBehaviour {

	//This collects and gathers all information going on
	//It also collects and handles genomes in the running
	//and handles crossovers or other combinations

	public GameObject CarObj;
	public int no_cars;
	public Color[] colors;

	private BinaryGenome[] genomes;
	private GameObject[] cars;
	private float[] fitness;

	// Use this for initialization
	void Start () {
		genomes = new BinaryGenome[no_cars];
		cars = new GameObject[no_cars];
		fitness = new float[no_cars];
		for (int i = 0; i < no_cars; i++) {
			genomes[i] = new BinaryGenome(9, 8, colors[i % colors.Length]);
			cars[i] = (GameObject) Instantiate(CarObj, new Vector3(2, 6, 0), Quaternion.identity);
			cars[i].GetComponent<BinaryCar>().BuildCar(genomes[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		bool all_destroyed = true;
		foreach (GameObject g in cars) {
			if (g == null) {

			}
			else {
				all_destroyed = false;

				if (g.rigidbody2D.velocity.magnitude < 0.05f) {
					g.GetComponent<BinaryCar>().ScheduleDestroy();
				}
				else {
					g.GetComponent<BinaryCar>().CancelDestroy();
				}
			}
		}
		if (all_destroyed) {
			//Handle next generation
			Debug.Log ("All dead!");
		}
	}

	IEnumerator DestroyCar (BinaryCar car) {
		yield return new WaitForSeconds(3);
		car.CarDestroy ();
	}
}
