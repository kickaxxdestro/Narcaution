using UnityEngine;
using System.Collections;

public class IceChunkBehaviour : MonoBehaviour {
	
	
	[System.NonSerialized]
	public bool moveActive = false;
	[System.NonSerialized]
	public bool shootActive = false;
	
	[System.NonSerialized]
	public Vector3 defPos;
	
	void Awake()
	{
		transform.localScale = Vector3.zero;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("Bullet") == true)
        {
            //disable the bullet
            //other.gameObject.SetActive(false); //bullet should be disabled in bullet behaviour
            GameObject go = GameObject.Find("bulletParticlePooler").GetComponent<ObjectPooler>().GetPooledObject();
            go.transform.position = transform.position;
            go.SetActive(true);
            go.GetComponent<ParticleSystem>().Play();

            //disable the bullet
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("BeamBullet"))
        {
            //disable the bullet
            //other.gameObject.SetActive(false); //bullet should be disabled in bullet behaviour
            GameObject go = GameObject.Find("bulletParticlePooler").GetComponent<ObjectPooler>().GetPooledObject();
            go.transform.position = transform.position;
            go.SetActive(true);
            go.GetComponent<ParticleSystem>().Play();
        }
        else if (other.CompareTag("Player"))
            GameObject.Find("EffectsHandler").GetComponent<EffectsHandler>().ActivateFrozenEffect();
	}
	
	// Update is called once per frame
	void Update () {
		
		//movement phase
		if(moveActive == true)
		{
			transform.position = Vector3.Lerp(transform.position, defPos, Time.deltaTime * 5.0f);
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.25f, 1.25f), Time.deltaTime * 3.0f);
			if(Vector3.Distance(transform.position, defPos) <= 0.01f)
			{
				moveActive = false;
			}
		}
		else if (moveActive == false)
		{
			//constant rotation
			transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
		}
		
		//shooting phase
		if(shootActive == true)
		{
			transform.position += new Vector3(0, -10) * Time.deltaTime;
		}
		
		
		//if it is out of bounds, reset and disable the object
		if(transform.position.y < -SystemVariables.current.CameraBoundsY - (transform.localScale.y * 0.5f))
		{
			transform.position = Vector3.zero;
			transform.localScale = Vector3.zero;
			shootActive = false;
			gameObject.SetActive(false);	
		}
	}
}
