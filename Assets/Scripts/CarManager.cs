using UnityEngine;
using System.Collections;

public class CarManager : MonoBehaviour {
	
	public int num_cars;
	public GameObject car;
	public Color[] car_colors;
	
	private Genome[] genomes;
	private GameObject[] cars;
	
	void CreateGeneration() {
		for (int i=0; i<num_cars; i++) {
			cars[0] = (GameObject) Instantiate (car, new Vector3 (2,2,0), Quaternion.identity);
			cars[0].GetComponent<Car>().SetupCar(genomes[i]);
			cars[0].GetComponent<Car>().car_color = car_colors[i % car_colors.Length];
			cars[0].GetComponent<Car>().SetupLines();
		}
	}
	
	// Use this for initialization
	void Start () {
		genomes = new Genome[num_cars];
		for (int i=0; i<num_cars; i++) {
			genomes[i] = new Genome();
			genomes[i].GenerateGenome(64);
		}
		
		cars = new GameObject[num_cars];	
		for (int i=0; i<num_cars; i++) {
			cars[i] = (GameObject) Instantiate (car, new Vector3 (2,2,0), Quaternion.identity);
			cars[i].GetComponent<Car>().SetupCar(genomes[i]);
			cars[i].GetComponent<Car>().car_color = car_colors[i % car_colors.Length];
			cars[i].GetComponent<Car>().SetupLines();
		}			
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject g in cars) {
			if (g != null && g.GetComponent<Rigidbody2D>().velocity.magnitude < 0.01f) {
				g.GetComponent<Car>().ClearLines();
				Destroy (g);
			}
		}
	}
}
