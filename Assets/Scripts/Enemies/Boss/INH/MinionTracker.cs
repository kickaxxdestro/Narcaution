using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MinionTracker : MonoBehaviour {

	//List <GameObject> activeMinions = new List<GameObject>();
	public List<GameObject> activeMinions = new List<GameObject> ();
	bool allMinionsKilled;
	bool bossActive;

	public GameObject cwMinion, acwMinion, verticalMinion;

	// Use this for initialization
	void Start () {
		spawnMinions ();
		activeMinions = GameObject.FindGameObjectsWithTag("Minion").ToList();
	}

	void spawnMinions (){
		GameObject go = Instantiate (cwMinion);
		GameObject go1 = Instantiate (acwMinion);
		GameObject go2 = Instantiate (verticalMinion);
	}

	// Update is called once per frame
	void Update () {

		for (int i=0; i<activeMinions.Count; i++) {
			if(/*activeMinions[i] == null*/ !activeMinions[i].gameObject.activeInHierarchy) {
				activeMinions.Remove(activeMinions[i]);
			}
		}
		
		if (activeMinions.Count == 0) {
			if(!bossActive) {
				foreach (Transform child in transform) {
					if(child.name == "inhalant") {
						child.gameObject.SetActive(true);
						bossActive = true;
					}
				}
			}
		}
	}
}
