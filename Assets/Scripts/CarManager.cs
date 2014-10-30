using UnityEngine;
using System.Collections;

public class CarManager : MonoBehaviour {
	
	public int num_cars;
	public GameObject car;
	public Color[] car_colors;
	
	private Genome[] genomes;
	private GameObject[] cars;
	
	private bool spawning = false;
	
	void CreateGeneration() {
		/* Debug.Log ("Spawning new generation!");
		genomes[0].GeneTrade(genomes[1]);
		Debug.Log ("Swapped 1 and 2");
		genomes[2].GeneTrade(genomes[3]);
		Debug.Log ("Swapped 3 and 4"); */
	
		for (int i=0; i<num_cars; i++) {
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
		for (int i=0; i<num_cars; i++) {
			genomes[i] = new Genome();
			genomes[i].GenerateGenome(64);
			Debug.Log (genomes[i].ToString());
		}
		
		cars = new GameObject[num_cars];	
		for (int i=0; i<num_cars; i++) {
			Debug.Log("Making car " + i);
			cars[i] = (GameObject) Instantiate (car, new Vector3 (2,2,0), Quaternion.identity);
			Debug.Log("Made car " + i);
			cars[i].GetComponent<Car>().SetupCar(genomes[i]);
			Debug.Log("Setup car " + i);
			cars[i].GetComponent<Car>().car_color = car_colors[i % car_colors.Length];
			cars[i].GetComponent<Car>().SetupLines();
		}			
	}
	
	// Update is called once per frame
	void Update () {
		if (!spawning) {
			bool spawn_new = true;
			foreach (GameObject g in cars) {
				if (g != null && g.GetComponent<Rigidbody2D>().velocity.magnitude < 0.01f) {
					g.GetComponent<Car>().ScheduleDestroy();
				}
				else if (g != null) {
					g.GetComponent<Car>().CancelDestroy();
				}
				
				if (g != null)
					spawn_new = false;	
			}
			
			if (spawn_new) {
				Debug.Log ("All cars destroyed!");
				spawning = true;
				CreateGeneration();
			}
		}
	}
}
