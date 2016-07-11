using UnityEngine;
using System.Collections;

public class sineCurve : MonoBehaviour {

	//Freq = 2.5 * MoveSpeed
	//Movespeed = 3 * amp
	public float MoveSpeed;	  // width of arc
	public float frequency;  // Speed of sine movement
	public float magnitude;   // Size of sine movement

	private Vector3 axis, direction;
	private Vector3 pos;

	float count;

	public enum dir {
		up,
		down,
		left,
		right
	}

	public dir curveDir, ampDir;
	public bool move;

	void Start () {
		pos = transform.position;
		/*(1)*/
		//axis = Vector3.right; //(up /down) / (left/ right)

		switch (curveDir) {
		case dir.down :
			direction = Vector3.down;
			break;
		case dir.up :
			direction = Vector3.up;
			break;
		case dir.left :
			direction = Vector3.left;
			break;
		case dir.right :
			direction = Vector3.right;
			break;
		}

		switch (ampDir) {
		case dir.down :
			axis = Vector3.down;
			break;
		case dir.up :
			axis = Vector3.up;
			break;
		case dir.left :
			axis = Vector3.left;
			break;
		case dir.right :
			axis = Vector3.right;
			break;
		}
	}
	
	void Update () {
		count += Time.deltaTime;
		//if (count < Mathf.PI/4) {	//stops curve at quarter sine curve
		/*(2)*/
		if (move) {
			pos += direction * Time.deltaTime * MoveSpeed;	//direction (left /right) / (up/down)
			//y(t) = Amp* sin(freq *t + phase)
			//transform.LookAt((pos) + (axis * Mathf.Sin (count * frequency) * magnitude));

			transform.position = (pos) + (axis * Mathf.Sin (count * frequency) * magnitude);

		}
		//}
	}
}

//Up - Down curve
//(1) Curves to the left/right (Vector3.right/ left)
//(2) Direction : Down/Up (Vector3.down/up)

//Left - Right curve
//(1) Curves up/ down (Vector3.up/down)
//(2) Direction : Left/Right (Vector3.left/right)