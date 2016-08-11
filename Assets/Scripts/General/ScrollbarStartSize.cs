using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollbarStartSize : MonoBehaviour {

	public float size;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (hi());
	}

	IEnumerator hi()
	{
		while (gameObject.GetComponent<Scrollbar> ().size != size) 
		{
			yield return new WaitForEndOfFrame();
			gameObject.GetComponent<Scrollbar> ().size = size;
		}
	}
}