using UnityEngine;
using UnityEngine.UI;
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
		foreach (GameObject g in GameObject.FindGameObjectsWithTag ("Car"))
			Destroy (g);

		genomes [0].GeneTrade (genomes [1]);

		for (int i = 0; i<number_of_cars; i++) {
			cars.Add ((GameObject) Instantiate (car, new Vector3(2,2,0), Quaternion.identity));
			cars[i].name = "Car " + i;
		}
		
		for (int i = 0; i<number_of_cars; i++) {
			cars[i].GetComponent<CarGenerator>().Generate(genomes[i]);
		}
		
		color_set = false;
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
				DrawWheel[] w = cars [i].GetComponentsInChildren<DrawWheel> ();
				w[0].SetColor(Color.gray);
				w[1].SetColor(new Color(0.85f,0.85f,0.85f));
			}
			color_set = true;
		}

		if (cars.Count == 0) {
			int count = VectorLine.canvas3D.transform.childCount;
			for (int i = 0; i < count; i++) {
				if (!VectorLine.canvas3D.transform.GetChild (i).gameObject.activeSelf)
					Destroy (VectorLine.canvas3D.transform.GetChild (i).gameObject);
			}
			MakeNewGeneration ();
		} else {
			cars.RemoveAll (Alive);

			int count = VectorLine.canvas3D.transform.childCount;
			for (int i = 0; i < count; i++) {
					if (!VectorLine.canvas3D.transform.GetChild (i).gameObject.activeSelf)
							Destroy (VectorLine.canvas3D.transform.GetChild (i).gameObject);
			}
			if (cars.Count > 0)
			  furthest = cars [0];
			for (int i = 1; i < cars.Count; i++) {
					if (cars [i].GetComponentInChildren<CarBody> ().IsAlive () && furthest.transform.GetChild (0).position.x < cars [i].transform.GetChild (0).position.x)
							furthest = cars [i];
			}
            if (cars.Count > 0)
			  follow.target = furthest.transform.GetChild (0).gameObject;
		}
		
		GameObject.Find ("Text").GetComponent<Text>().text = genomes[0].ToString() + "\n" + genomes[1].ToString ();
	}
	
	private static bool Alive (GameObject g) {
		return !g.GetComponentInChildren<CarBody> ().IsAlive ();
	}
}
