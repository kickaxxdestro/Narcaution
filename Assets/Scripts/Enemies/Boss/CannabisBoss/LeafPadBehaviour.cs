using UnityEngine;
using System.Collections;

public class LeafPadBehaviour : MonoBehaviour {
	
	[System.NonSerialized]
	public Vector3 origPos;
	
	[System.NonSerialized]
	public float origAngle;
	
	GameObject bulletParticlePooler;
    patternList leafPatternList;
    [HideInInspector]
    public float attackTimer = 0f;
	
	void Awake()
	{
		bulletParticlePooler = GameObject.Find("bulletParticlePooler");
        leafPatternList = GetComponent<patternList>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Bullet") == true)
		{
			//get a pooled bullet particle
			if(other.GetComponent<BulletBehaviour>().bulletType != BulletBehaviour.BulletType.bulletBeam){
				GameObject go = bulletParticlePooler.GetComponent<ObjectPooler>().GetPooledObject();
				go.transform.position = other.transform.position;
				go.SetActive(true);
				go.GetComponent<ParticleSystem>().Play();
				
				//disable the bullet
				other.gameObject.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0f)
        {
            leafPatternList.attack(0);
            attackTimer = 3f;
        }
		if(Vector3.Distance(transform.position, origPos) > 0)
		{
			//move to original position upon enabling
			transform.position = Vector3.Slerp(transform.position, origPos, Time.deltaTime * 4.0f);
		}
		if(Vector3.Angle(transform.eulerAngles, new Vector3(0, 0, origAngle)) > 0)
		{
			float angle = Mathf.LerpAngle(transform.eulerAngles.z, origAngle, Time.deltaTime);
			transform.eulerAngles = new Vector3(0, 0, angle);
		}
	}
}
