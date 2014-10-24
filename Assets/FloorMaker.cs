using UnityEngine;
using System.Collections;

public class FloorMaker : MonoBehaviour {
	
	public GameObject floor_piece;
	
	private Vector2 prev = Vector2.zero;
	private Vector2 curr = Vector2.zero;
	
	private int count = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (count < 250) {
			float vert_offset = Random.Range(-1.0f,1.0f);
			
			curr = prev + new Vector2(1,vert_offset);
			curr.y = Mathf.Clamp (curr.y, -8, 8);
			
			GameObject curr_piece = (GameObject) Instantiate(floor_piece, Vector2.zero, Quaternion.identity);
			curr_piece.transform.parent = gameObject.transform;
			
			PolygonCollider2D curr_poly = curr_piece.GetComponent<PolygonCollider2D>();
			curr_poly.points = new Vector2[] {prev, curr, curr + Vector2.up, prev + Vector2.up};
			
			LineRenderer curr_line = curr_piece.GetComponent<LineRenderer>();
			curr_line.SetVertexCount(5);
			for (int i=0; i<5; i++)
				curr_line.SetPosition(i, curr_poly.points[i%4]);
			
			prev = curr;
			
			count++;
		}
	}
}
