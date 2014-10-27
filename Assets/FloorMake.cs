using UnityEngine;
using System.Collections;
using Vectrosity;
using System.Security.Cryptography;

public class FloorMake : MonoBehaviour {
	
	private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();
	
	public static float Between(float minimumValue, float maximumValue)
	{
		byte[] randomNumber = new byte[1];
		
		_generator.GetBytes(randomNumber);
		
		float asciiValueOfRandomCharacter = (float)System.Convert.ToDouble(randomNumber[0]);
		
		// We are using Math.Max, and substracting 0.00000000001, 
		// to ensure "multiplier" will always be between 0.0 and .99999999999
		// Otherwise, it's possible for it to be "1", which causes problems in our rounding.
		float multiplier = Mathf.Max(0, (asciiValueOfRandomCharacter / 255f) - 0.00000000001f);
		
		// We need to add one to the range, to allow for the rounding done with Math.Floor
		float range = maximumValue - minimumValue + 1;
		
		float randomValueInRange = Mathf.Floor(multiplier * range);
		
		return (minimumValue + randomValueInRange);
	}
	
	// Use this for initialization
	void Start () {
		Vector3[] p = new Vector3[10000];
		VectorLine myline = new VectorLine("Floor", p, null, 5.0f, LineType.Continuous, Joins.Weld);
		
		Vector3[] points = new Vector3[500];
		Vector3 prev = Vector3.zero;
		for (int i = 0; i<500; i++) {
		  if (i < 50)
		  	prev += new Vector3(1, 0, 0);
		  else
		    prev += new Vector3(1, Between(-1.0f, 1.0f), 0);
		    
		  points[i] = prev;
		}
		
		myline.MakeSpline(points);
		myline.SetColor (Color.red);
		
		myline.collider = true;
		myline.Draw3D ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}