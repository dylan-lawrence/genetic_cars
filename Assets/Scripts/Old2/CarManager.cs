using UnityEngine;
using System.Collections;

public class CarManager : MonoBehaviour {
	
	public int num_cars;
	public GameObject car;
	public Color[] car_colors;

	public float velocity_threshold;
	
	private Genome[] genomes;
	private GameObject[] cars;
	private float[] fitness;
	
	private bool spawning = false;
	
	void CreateGeneration() {

		genomes [0].CrossoverBodyWheels (genomes [1]);

		for (int i=0; i<num_cars; i++) {
			Debug.Log (genomes[i].ToString ());

			cars[i] = (GameObject) Instantiate (car, new Vector3 (2,2,0), Quaternion.identity);
			cars[i].GetComponent<Car>().SetupCar(genomes[i]);
			cars[i].GetComponent<Car>().car_color = car_colors[i % car_colors.Length];
			cars[i].GetComponent<Car>().SetupLines();
		}
		
		spawning = false;
	}
	
	// Use this for initialization
	void Start () {
		genomes = new Genome[num_cars];
		fitness = new float[num_cars];
		for (int i=0; i<num_cars; i++) {
			genomes[i] = new Genome();
			genomes[i].GenerateGenome(72);
			//Debug.Log (genomes[i].ToString());
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
		if (!spawning) {
			bool spawn_new = true;
			GameObject furthest = cars[0];
			foreach (GameObject g in cars) {
				if (g != null && g.GetComponent<Rigidbody2D>().velocity.magnitude < velocity_threshold) {
					g.GetComponent<Car>().ScheduleDestroy();
				}
				else if (g != null) {
					g.GetComponent<Car>().CancelDestroy();
				}
				
				if (g != null) {
					spawn_new = false;

					if (furthest != null && g.transform.position.x > furthest.transform.position.x) {
						furthest = g;
					}
					else if (furthest == null && g != null) {
						furthest = g;
					}
					if (furthest != null) {
						Camera.main.transform.position = furthest.transform.position - Vector3.forward + Vector3.up;
					}
				}
			}

			for (int i = 0; i<num_cars; i++) {
				if (cars[i] != null)
					fitness[i] = cars[i].transform.position.x;
			}

			if (spawn_new) {
				for (int i = 0; i<num_cars; i++) {
					Debug.Log ("Car " + (i+1) + " fitness is " + fitness[i]);
				}
				spawning = true;
				CreateGeneration();
			}
		}
	}
}
