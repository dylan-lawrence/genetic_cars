using UnityEngine;
using System.Collections;

public class CarBody : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector2[] points = GetComponent<PolygonCollider2D>().points;
		Texture2D tex = new Texture2D(256,256);
		for (int i = 0; i < points.Length; i++) {
			for (int j = 0; j<3; j++) {
				for (int k = 0; k<3;k++) {
					tex.SetPixel ((int) points[i].x + j, (int) points[i].y + k, Color.red);
				}
			}
		}
		tex.Apply ();
		renderer.material.mainTexture = tex;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
