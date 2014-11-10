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

	private bool building_cars = false;

	public void ShuffleGenomes (BinaryGenome[] list) {
		System.Random rng = new System.Random ();
		int n = list.Length;
		while (n > 1) {
			n--;
			int k = rng.Next(n+1);
			BinaryGenome temp = list[k];
			list[k] = list[n];
			list[n] = temp;
		}
	}

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
		if (!building_cars) {
			bool all_destroyed = true;
			for (int i = 0; i < no_cars; i++) {
					if (cars [i] == null) {

					} else {
							all_destroyed = false;

							fitness [i] = cars [i].transform.position.x;
							
							if (cars[i].transform.position.y < -500)
								cars[i].GetComponent<BinaryCar>().CarDestroy();

							if (cars [i].rigidbody2D.velocity.magnitude < 0.30f) {
									cars [i].GetComponent<BinaryCar> ().ScheduleDestroy ();
							} else {
									cars [i].GetComponent<BinaryCar> ().CancelDestroy ();
							}
					}
			}
			int curr_furthest_index = 0;
			for (int i = 0; i < no_cars; i++) {
				if (cars[i] != null) {
					if (fitness[i] > fitness[curr_furthest_index]) {
						curr_furthest_index = i;
					}
				}
			}
			while (cars[curr_furthest_index % cars.Length] == null) { //safety measure
				if (curr_furthest_index > cars.Length * 3)
					break;
				curr_furthest_index++;
			}

			if (cars[curr_furthest_index % cars.Length] != null)
				Camera.main.transform.position = new Vector3(cars[curr_furthest_index % cars.Length].transform.position.x,cars[curr_furthest_index % cars.Length].transform.position.y,-10);

			if (all_destroyed) {
				//Handle next generation
				building_cars = true;
				CreateNewGeneration ();
			}
		}
	}

	void CreateNewGeneration() {
		ShuffleGenomes (genomes);
		for (int i = 0; i < no_cars; i+=2)
			genomes [i].RandomCrossover (genomes [i + 1]);
		for (int i = 0; i < no_cars; i++) {
			cars[i] = (GameObject) Instantiate(CarObj, new Vector3(2, 6, 0), Quaternion.identity);
			cars[i].GetComponent<BinaryCar>().BuildCar(genomes[i]);
		}
		building_cars = false;
	}

	IEnumerator DestroyCar (BinaryCar car) {
		yield return new WaitForSeconds(3);
		car.CarDestroy ();
	}
}
