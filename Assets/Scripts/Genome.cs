using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Genome {
	
	public static string[] Nucleotides = new string[4] {"A","T","C","G"};
	public static Dictionary<string, string> GeneToNum = new Dictionary<string, string> { {"A", "0"}, {"T", "1"}, {"C", "2"}, {"G", "3"} };
	
	public bool debug = false;
		
	private string _genome;
	
	public static int BaseFourConverter (string num) {
		int value = 0;
		for (int i = 0; i < num.Length - 1; i++) {
			value += (int)(System.Convert.ToInt32(num.Substring(i,1)) * Mathf.Pow (4,(num.Length -1 - i)));
		}
		return value;
	}
	
	public static int GeneToInt(string gene) {
		string nums = "";
		for (int i = 0; i<gene.Length; i++)
			nums += GeneToNum[gene.Substring(i, 1)];

		return BaseFourConverter(nums);
	}
	
	public static string RandomNucleotide() {
		return Nucleotides[Random.Range (0, 4)];
	}
	
	public void GenerateGenome(int length) {
		if (length % 4 != 0)
			throw new System.Exception ("Genome length must be divisble by 4!");
		_genome = "";
		for (int i = 0; i<length; i++)
			_genome += RandomNucleotide();
	}
	
	public void Mutate(float rate = 0.05f) {
		string g = (string) _genome.Clone(); //deepcopy safety
		_genome = "";
		for (int i = 0; i<g.Length; i++) {
			float m = Random.Range (0.0f, 1.0f);
			if (m <= rate)
				_genome += RandomNucleotide();
			else
				_genome += g[i];
		}
	}

	public string this[int i] {
		get { return _genome.Substring (i, 4); }
		set { 
			string temp = "";
			for (int il = 0; il < _genome.Length/4; il++) {
				if (il == i)
					temp += value;
				else
					temp += this[il];
			}
			_genome = temp;
		}
	}

	public string ToString() {
		return _genome;
	}

	//-- Consider Start to Exist only for debugging
	void Start() {
		if (debug) {
			GenerateGenome(20);
			Debug.Log(_genome);
			Mutate();
			Debug.Log(_genome);
			
			Debug.Log (_genome.Substring(0,4));
			Debug.Log (GeneToInt(_genome.Substring(0,4)));
		}
	}
}
