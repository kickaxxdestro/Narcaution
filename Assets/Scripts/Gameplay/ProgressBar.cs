using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public GameObject tracker;
    public bool doProgression = false;
    GameObject currentLevel;
    float levelDuration;
    Vector3 trackerStartPos;
    Vector3 trackerEndPos;
    float trackerTimer = 0f;

	// Use this for initialization
	void Start () {
        currentLevel = GameObject.FindGameObjectWithTag("Level");
        levelDuration = currentLevel.GetComponent<LevelGeneratorScript>().GetLevelDuration();
        trackerStartPos = tracker.transform.FindChild("StartPos").transform.position;
        trackerEndPos = tracker.transform.FindChild("EndPos").transform.position;

        GameObject trackerBacking = transform.FindChild("TrackerBacking").gameObject;

        trackerStartPos = new Vector3(-trackerBacking.GetComponent<RectTransform>().rect.width * 0.5f, tracker.transform.localPosition.y);
        trackerEndPos = new Vector3(trackerBacking.GetComponent<RectTransform>().rect.width * 0.5f, tracker.transform.localPosition.y);

        tracker.transform.localPosition = trackerStartPos;
        //movementAmount = (trackerEndPos - trackerStartPos).magnitude / levelDuration;
	}
	
	// Update is called once per frame
	void Update () {
        if (doProgression)
            trackerTimer += Time.deltaTime;
        tracker.transform.localPosition = new Vector3(trackerStartPos.x + ((trackerTimer / levelDuration) * (trackerEndPos - trackerStartPos).magnitude), tracker.transform.localPosition.y);
        //    //tracker.transform.position = new Vector3(tracker.transform.position.x + (movementAmount * Time.deltaTime), tracker.transform.position.y);
	}
}
