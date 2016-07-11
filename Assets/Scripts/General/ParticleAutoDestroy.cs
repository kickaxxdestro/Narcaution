using UnityEngine;
using System.Collections;

public class ParticleAutoDestroy : MonoBehaviour {

	ParticleSystem ps;
	
	void Awake()
	{
		ps = GetComponent<ParticleSystem>();
	}
	
	void Update () 
	{
		if(ps)
		{
			if(!ps.IsAlive())
			{
				ps.Clear();
				gameObject.SetActive(false);
			}
		}		
	}
}