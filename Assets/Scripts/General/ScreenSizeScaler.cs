using UnityEngine;
using System.Collections;

public class ScreenSizeScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {


        var sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            var height1 = Camera.main.orthographicSize * 2.0;
            var width1 = height1 * Screen.width / Screen.height;
            transform.localScale = new Vector3((float)width1, (float)height1, 0.1f);
            return;
        }

        transform.localScale = new Vector3(1, 1, 1);

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3((float)worldScreenWidth / width, (float)worldScreenHeight / height, 0.1f);

        //var width = Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height;
        //transform.localScale = new Vector3((float)width, transform.localScale.y, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
