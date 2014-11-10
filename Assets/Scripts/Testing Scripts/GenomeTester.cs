using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class GenomeTester : MonoBehaviour {

	public int genome_length;
	public int gene_size;
	public int no_of_genomes;
	public int no_of_crossovers;

	public Color[] colors;

	public static void Shuffle<T> (List<T> list) {
		System.Random rng = new System.Random ();
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = rng.Next(n+1);
			T temp = list[k];
			list[k] = list[n];
			list[n] = temp;
		}
	}

	// Use this for initialization
	void Start () {
		BinaryGenome g1 = new BinaryGenome (4, 4, Color.red);
		BinaryGenome g2 = new BinaryGenome (4, 4, Color.blue);

		BinaryGenome temp = g1.Copy(); //this is a shallow copy...

		temp.RandomCrossover (g2);

		Debug.Log (g1);
		Debug.Log (g2);
		Debug.Log (temp);
	}
}
