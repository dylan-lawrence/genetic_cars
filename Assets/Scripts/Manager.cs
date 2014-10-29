using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class Manager : MonoBehaviour {
	
	public int number_of_cars;
	public GameObject car;
	public Color[] car_colors;
	
	private Follow follow;

	private Genome[] genomes;
	private List<GameObject> cars = new List<GameObject>();
	private GameObject furthest;

	private bool color_set = false;

	void MakeNewGeneration() {

	}

	// Use this for initialization
	void Start () {
		genomes = new Genome[number_of_cars];
		follow = gameObject.AddComponent<Follow>();
		
		for (int i = 0; i<number_of_cars; i++) {
			cars.Add ((GameObject) Instantiate (car, new Vector3(2,2,0), Quaternion.identity));
			cars[i].name = "Car " + i;
		}

		for (int i = 0; i<number_of_cars; i++) {
			genomes[i] = new Genome();
			genomes[i].GenerateGenome(64);
			cars[i].GetComponent<CarGenerator>().Generate(genomes[i]);
		}
	}
	
	void Update() {
		if (!color_set) {
			for (int i = 0; i<number_of_cars; i++) {
				Debug.Log ("Setting car color...");
				cars [i].GetComponentInChildren<CarBody> ().SetColor (car_colors [i]);
			}
			color_set = true;
		}

		if (cars.Count == 0) {
						MakeNewGeneration ();
				} else {
						cars.RemoveAll (Alive);

						int count = VectorLine.canvas3D.transform.childCount;
						for (int i = 0; i < count; i++) {
								if (!VectorLine.canvas3D.transform.GetChild (i).gameObject.activeSelf)
										Destroy (VectorLine.canvas3D.transform.GetChild (i).gameObject);
						}

						furthest = cars [0];
						for (int i = 1; i < cars.Count; i++) {
								if (cars [i].GetComponentInChildren<CarBody> ().IsAlive () && furthest.transform.GetChild (0).position.x < cars [i].transform.GetChild (0).position.x)
										furthest = cars [i];
						}
						follow.target = furthest.transform.GetChild (0).gameObject;
				}
	}
	
	private static bool Alive (GameObject g) {
		return !g.GetComponentInChildren<CarBody> ().IsAlive ();
	}
}
