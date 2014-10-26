using UnityEngine;
using System.Collections;
using Vectrosity;

public class DrawPolycollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		VectorLine.SetCanvasCamera(Camera.main);
		
		VectorLine line = new VectorLine("Car", Vector2[0], null, 2);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
