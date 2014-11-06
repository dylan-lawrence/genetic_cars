using UnityEngine;
using System.Collections;

public class BinaryGene {

	private string _gene;

	public string gene {
		get { return _gene; }
	}

	private int _length;
	private Color _color;

	public Color color {
		get { return _color; }
	}

	public BinaryGene(int length = 8, Color c = default(Color)) {
		//@param c defaults to red
		_gene = "";
		_color = c;
		for (int i = 0; i<length; i++)
			_gene += Random.Range (0, 2).ToString ();
	}
	
	override public string ToString() {
		string r = ((int)(_color.r * 255)).ToString("X2");
		string g = ((int)(_color.g * 255)).ToString("X2");
		string b = ((int)(_color.b * 255)).ToString("X2");

		string col = "#" + r + g + b + "FF";

		return "<color=" + col + ">" + _gene + "</color>";
	}
}
