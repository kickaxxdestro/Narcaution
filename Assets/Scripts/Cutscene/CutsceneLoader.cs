using UnityEngine;
using System.Collections;

public class CutsceneLoader : MonoBehaviour {

    public GameObject tapToContinueIcon;
    public GameObject[] cutsceneList;

	// Use this for initialization
	void Awake () {
        GameObject cutscene = null;

        foreach(GameObject go in cutsceneList)
        {
            if(go.transform.FindChild("TransitionListHandler").GetComponent<TransitionListHandler>().ListID == PlayerPrefs.GetInt("ppSelectedCutscene", 1))
            {
                cutscene = Instantiate(go) as GameObject;
                cutscene.transform.SetParent(GameObject.Find("UICanvas").transform, false);
                cutscene.transform.FindChild("TransitionListHandler").GetComponent<TransitionListHandler>().GetComponent<TransitionListHandler>().tapToContinueIcon = this.tapToContinueIcon;
                break;
            }
        }

        if (cutscene == null)
            print("Cutscene not found");

        //Destroy after loading
        Destroy(this.gameObject);
	}
}
