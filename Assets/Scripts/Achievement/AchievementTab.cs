using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementTab : MonoBehaviour {

	public float fadeSpeed;
	public float holdTime;

	enum State
	{
		start,
		hold,
		end
	};

	State state;

	// Use this for initialization
	void Start () 
	{
		state = State.start;
		this.GetComponentInChildren<CanvasGroup> ().alpha = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (state) 
		{
			case State.start:
				this.GetComponentInChildren<CanvasGroup> ().alpha += Time.unscaledDeltaTime * fadeSpeed;
				if(this.GetComponentInChildren<CanvasGroup> ().alpha >= 1)
				{
					this.GetComponentInChildren<CanvasGroup> ().alpha = 1;
					state = State.hold;
				}
			break;

			case State.hold:
				holdTime -= Time.unscaledDeltaTime;
				if (holdTime <= 0)
					state = State.end;
			break;

			case State.end:
				this.GetComponentInChildren<CanvasGroup> ().alpha -= Time.unscaledDeltaTime * fadeSpeed;
				if(this.GetComponentInChildren<CanvasGroup> ().alpha <= 0)
				{
					Destroy (this.gameObject);
				}
			break;
		}
	}

	public void SetInfo(string name)
	{
		gameObject.transform.Find("Name").GetComponent<Text> ().text = name;
	}
}
