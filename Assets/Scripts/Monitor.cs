using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monitor : MonoBehaviour {

	//This collects and gathers all information going on
	//It also collects and handles genomes in the running
	//and handles crossovers or other combinations

	public GameObject CarObj;

	// Use this for initialization
	void Start () {
		List<BinaryGenome> genomes = new List<BinaryGenome> ();
		genomes.Add (new BinaryGenome (5, 8, Color.red));
		genomes.Add (new BinaryGenome (5, 8, Color.yellow));
		genomes.Add (new BinaryGenome (5, 8, Color.blue));
		genomes.Add (new BinaryGenome (5, 8, Color.green));
		genomes.Add (new BinaryGenome (5, 8, Color.black));

		for (int i = 0; i<10; i++) {
			GenomeTester.Shuffle(genomes);
			genomes[0].RandomCrossover(genomes[1]);
		}

		Debug.Log (genomes[0]);

		GameObject obj = (GameObject) Instantiate (CarObj, new Vector3 (2, 2, 0), Quaternion.identity);
		obj.GetComponent<BinaryCar> ().BuildCar (genomes[0]);

		StartCoroutine(DestroyCar(obj.GetComponent<BinaryCar>()));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator DestroyCar (BinaryCar car) {
		yield return new WaitForSeconds(3);
		car.CarDestroy ();
	}
}
