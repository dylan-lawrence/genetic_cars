using UnityEngine;
using System.Collections;

public class Statics {

	public static bool Less (Vector2 a, Vector2 b, Vector2 center) {
		if (a.x - center.x >= 0 && b.x - center.x <= 0)
			return true;
		if (a.x - center.x < 0 && b.x - center.x >= 0)
			return false;
		if (a.x - center.x == 0 && b.x - center.x == 0) {
			if (a.y - center.y >= 0 || b.y - center.y >= 0)
				return a.y  > b.y;
			return b.y > a.y;
		}
		
		float det = (a.x - center.x) * (b.y - center.y) - (b.x - center.x) * (a.y - center.y);
		if (det < 0)
			return true;
		if (det > 0)
			return false;
			
		float d1 = (a.x - center.x) * (a.x - center.x) + (a.y - center.y) * (a.y - center.y);
		float d2 = (b.x - center.x) * (b.x - center.x) + (b.y - center.y) * (b.y - center.y); 
		return d1 > d2;
	}
	
	public static Vector2[] CircleSort(Vector2[] list) {
		Vector2[] output = (Vector2[]) list.Clone();

		//Calculate center
		float x = 0.0f;
		float y = 0.0f;
		for (int i = 0; i<list.Length; i++) {
			x += list[i].x;
			y += list[i].y;
		}
		
		Vector2 center = new Vector2 (x/list.Length, y/list.Length);
		
		//Bubble because lazy...
		
		bool swapped = true;
		int j = 0;
		while (swapped) {
			swapped = false;
			j++;
			for (int i = 0; i<list.Length-j; i++) {
				if (Less (output[i+1], output[i], center)) {
					Vector2 temp = output[i+1];
					output[i+1] = output[i];
					output[i] = temp;
					swapped = true;
				}
			}
		}
		
		return output;
	}
}
