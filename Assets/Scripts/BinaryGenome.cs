using UnityEngine;
using System.Collections;

public class BinaryGenome {

	public int Length {
		get {
			return _genome.Length;
		}
	}

	private BinaryGene[] _genome;

	public BinaryGenome(int num_genes, int gene_size = 8, Color c = default(Color)) {
		_genome = new BinaryGene[num_genes];
		for (int i = 0; i < num_genes; i++)
			_genome [i] = new BinaryGene (gene_size, c);
	}

	public BinaryGene this[int i] {
		get {
			return _genome[i];
		}
		set {
			_genome[i] = value;
		}
	}

	override public string ToString() {
		string output = "";
		for (int i = 0; i < _genome.Length; i++)
			output += _genome [i].ToString ();
		return output;
	}

	public void RandomCrossover(BinaryGenome other) {
		if (this.Length != other.Length) {
			throw new System.Exception("Genomes must be of equivalent side to crossover");
		}
		int index = Random.Range (0, this.Length);
		for (int i = index; i<this.Length; i++) {
			BinaryGene temp = this[i];
			this[i] = other[i];
			other[i] = temp;
		}
	}

	public BinaryGenome Copy() {
		BinaryGenome newGenome = new BinaryGenome (Length, this [0].gene.Length);
		for (int i = 0; i<Length; i++)
			newGenome [i] = this [i].Copy ();
		return newGenome;
	}
}
