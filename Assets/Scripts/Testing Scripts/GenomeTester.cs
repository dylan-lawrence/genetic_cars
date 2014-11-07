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
		List<BinaryGenome> genomes = new List<BinaryGenome>();
		for (int i = 0; i < no_of_genomes; i+=2) {
			genomes.Add (new BinaryGenome (genome_length, gene_size, colors [i % colors.Length]));
			genomes.Add (new BinaryGenome (genome_length, gene_size, colors [i % colors.Length]));
		}
		for (int i = 0; i < no_of_crossovers; i++) {
			for (int j = 0; j < no_of_genomes-1; j+=2)
				genomes[j].RandomCrossover(genomes[j+1]);
			Shuffle (genomes);
		}
		for (int i = 0; i < no_of_genomes; i++)
			Debug.Log (genomes [i]);

		//Test drawing out genome[0]
		Vector2[] points = new Vector2[genomes[0].Length/2];
		for (int i = 0; i < genomes[0].Length; i+=2)
			points[i/2] = new Vector2(System.Convert.ToInt32(genomes[0][i].gene,2),System.Convert.ToInt32(genomes[0][i+1].gene,2));

		points = Statics.CircleSort (points);

		Vector3[] p = new Vector3[genomes [0].Length / 2 + 1];
		for (int i = 0; i < genomes[0].Length/2; i++)
			p [i] = (Vector3)points [i];
		p [genomes [0].Length / 2] = (Vector3)points [0];

		VectorLine line = new VectorLine ("line", p, null, 100.0f, LineType.Continuous, Joins.Fill);
		line.smoothColor = true;

		Color[] cols = new Color[genomes[0].Length/2];
		for (int i = 0; i < genomes[0].Length/2; i++)
			cols [i] = genomes [0] [i].color;

		line.SetColors (cols);

		line.Draw3DAuto ();
	}
}
