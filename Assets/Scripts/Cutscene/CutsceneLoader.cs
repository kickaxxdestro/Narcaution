using UnityEngine;
using System.Collections;

[System.Serializable]
public class CutsceneIDandObjectName : System.Object
{
	public int ListID = 0;
	public string CutsceneName = "";
}

public class CutsceneLoader : MonoBehaviour {

    public GameObject tapToContinueIcon;
	public CutsceneIDandObjectName[] cutsceneList;

	// Use this for initialization
	void Awake () {
        GameObject cutscene = null;

		foreach(CutsceneIDandObjectName go in cutsceneList)
        {
			if(go.ListID == PlayerPrefs.GetInt("ppSelectedCutscene", 1))
            {
				cutscene = Instantiate(Resources.Load<GameObject> ("Cutscenes/" + go.CutsceneName)) as GameObject;//Instantiate(go) as GameObject;
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
