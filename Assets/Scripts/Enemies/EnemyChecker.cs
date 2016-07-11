using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyChecker : MonoBehaviour {
	
	[System.NonSerialized]
	public int crackAppeared = 0;
	[System.NonSerialized]
	public int heroinAppeared = 0;
	[System.NonSerialized]
	public int iceAppeared = 0;
	[System.NonSerialized]
	public int bzpAppeared = 0;
	[System.NonSerialized]
	public int ketamineAppeared = 0;
	[System.NonSerialized]
	public int cannabisAppeared = 0;

	[System.NonSerialized]
	public int buprenorphineAppeared = 0;
	[System.NonSerialized]
	public int mephedroneAppeared = 0;
	[System.NonSerialized]
	public int npsAppeared = 0;
	[System.NonSerialized]
	public int lsdAppeared = 0;
	[System.NonSerialized]
	public int ecstasyAppeared = 0;
	[System.NonSerialized]
	public int inhalantAppeared = 0;

	
	[System.NonSerialized]
	public int crackSeen = 0;
	[System.NonSerialized]
	public int heroinSeen = 0;
	[System.NonSerialized]
	public int iceSeen = 0;
	[System.NonSerialized]
	public int bzpSeen = 0;
	[System.NonSerialized]
	public int ketamineSeen = 0;
	[System.NonSerialized]
	public int cannabisSeen = 0;

	[System.NonSerialized]
	public int buprenorphineSeen = 0;
	[System.NonSerialized]
	public int mephedroneSeen = 0;
	[System.NonSerialized]
	public int npsSeen = 0;
	[System.NonSerialized]
	public int lsdSeen = 0;
	[System.NonSerialized]
	public int ecstasySeen = 0;
	[System.NonSerialized]
	public int inhalantSeen = 0;

	Transform introPanel;
	
	void Awake()
	{
		introPanel = transform.FindChild("introPanel");
		
		crackSeen = PlayerPrefs.GetInt("ppSeenCrack", 0);
		heroinSeen = PlayerPrefs.GetInt("ppSeenHeroin", 0);
		iceSeen = PlayerPrefs.GetInt("ppSeenICE", 0);
		bzpSeen = PlayerPrefs.GetInt("ppSeenBZP", 0);
		ketamineSeen = PlayerPrefs.GetInt("ppSeenKetamine", 0);
		cannabisSeen = PlayerPrefs.GetInt("ppSeenCannabis", 0);

		buprenorphineSeen = PlayerPrefs.GetInt("ppSeenBuprenorphine", 0);
		mephedroneSeen = PlayerPrefs.GetInt("ppSeenMephedrone", 0);
		npsSeen = PlayerPrefs.GetInt("ppSeenNPS", 0);
		lsdSeen = PlayerPrefs.GetInt("ppSeenLSD", 0);
		ecstasySeen = PlayerPrefs.GetInt("ppSeenEcstasy", 0);
		inhalantSeen = PlayerPrefs.GetInt("ppSeenInhalant", 0);
		if(crackSeen >= 1 && heroinSeen >= 1 && iceSeen >= 1 && bzpSeen >= 1 && ketamineSeen >= 1 && cannabisSeen >= 1 && 
		   buprenorphineSeen >= 1 && mephedroneSeen >= 1 && npsSeen >= 1 && lsdSeen >= 1 && ecstasySeen >= 1 && inhalantSeen >= 1)
		{
			this.enabled = false;
		}
	}
	
	public void SetTimeScale()
	{
		introPanel.gameObject.SetActive(false);
		introPanel.FindChild("intro_Crack").gameObject.SetActive(false);
		introPanel.FindChild("intro_Heroin").gameObject.SetActive(false);
		introPanel.FindChild("intro_ICE").gameObject.SetActive(false);
		introPanel.FindChild("intro_BZP").gameObject.SetActive(false);
		introPanel.FindChild("intro_Ketamine").gameObject.SetActive(false);
		introPanel.FindChild("intro_Cannabis").gameObject.SetActive(false);

		introPanel.FindChild("intro_Buprenorphine").gameObject.SetActive(false);
		introPanel.FindChild("intro_Mephedrone").gameObject.SetActive(false);
		introPanel.FindChild("intro_NPS").gameObject.SetActive(false);
		introPanel.FindChild("intro_LSD").gameObject.SetActive(false);
		introPanel.FindChild("intro_Ecstasy").gameObject.SetActive(false);
		introPanel.FindChild("intro_Inhalant").gameObject.SetActive(false);
		Time.timeScale = 1.0f;
	}
	
	void SetIntro()
	{
		//show intro
		if(crackAppeared >= 1 && crackSeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_Crack").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "Crack \naka Cocaine";
			crackSeen = 1;
			PlayerPrefs.SetInt("ppSeenCrack", crackSeen);
			Time.timeScale = 0.0f;
		}
		else if(heroinAppeared >= 1 && heroinSeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_Heroin").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "Heroin \naka White";
			heroinSeen = 1;
			PlayerPrefs.SetInt("ppSeenHeroin", heroinSeen);
			Time.timeScale = 0.0f;
		}
		else if(iceAppeared >= 1 && iceSeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_ICE").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "ICE \naka Meth";
			iceSeen = 1;
			PlayerPrefs.SetInt("ppSeenICE", iceSeen);
			Time.timeScale = 0.0f;
		}
		else if(bzpAppeared >= 1 && bzpSeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_BZP").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "BZP \naka Party Pills";
			bzpSeen = 1;
			PlayerPrefs.SetInt("ppSeenBZP", bzpSeen);
			Time.timeScale = 0.0f;
		}
		else if(ketamineAppeared >= 1 && ketamineSeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_Ketamine").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "Ketamine \naka K";
			ketamineSeen = 1;
			PlayerPrefs.SetInt("ppSeenKetamine", ketamineSeen);
			Time.timeScale = 0.0f;
		}
		else if(cannabisAppeared >= 1 && cannabisSeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_Cannabis").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "Cannabis \naka Marijuana";
			cannabisSeen = 1;
			PlayerPrefs.SetInt("ppSeenCannabis", cannabisSeen);
			Time.timeScale = 0.0f;
		}
		else if(buprenorphineAppeared >= 1 && buprenorphineSeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_Buprenorphine").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "Buprenorphine \naka Subutex";
			buprenorphineSeen = 1;
			PlayerPrefs.SetInt("ppSeenBuprenorphine", buprenorphineSeen);
			Time.timeScale = 0.0f;
		}
		else if(mephedroneAppeared >= 1 && mephedroneSeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_Mephedrone").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "Mephedrone \naka Snow";
			mephedroneSeen = 1;
			PlayerPrefs.SetInt("ppSeenMephedrone", mephedroneSeen);
			Time.timeScale = 0.0f;
		}
		else if(npsAppeared >= 1 && npsSeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_NPS").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "New Psychoactive Substances \naka NPS";
			npsSeen = 1;
			PlayerPrefs.SetInt("ppSeenNPS", npsSeen);
			Time.timeScale = 0.0f;
		}
		else if(lsdAppeared >= 1 && lsdSeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_LSD").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "Lysergide(LSD) \naka Acid";
			lsdSeen = 1;
			PlayerPrefs.SetInt("ppSeenLSD", npsSeen);
			Time.timeScale = 0.0f;
		}
		else if(ecstasyAppeared >= 1 && ecstasySeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_Ecstasy").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "Ecstasy \naka MDMA";
			ecstasySeen = 1;
			PlayerPrefs.SetInt("ppSeenEcstasy", ecstasySeen);
			Time.timeScale = 0.0f;
		}
		else if(inhalantAppeared >= 1 && inhalantSeen == 0)
		{
			introPanel.gameObject.SetActive(true);
			introPanel.FindChild("intro_Inhalant").gameObject.SetActive(true);
			introPanel.FindChild("introText").GetComponent<Text>().text = "Inhalant \naka Glue-sniffing";
			inhalantSeen = 1;
			PlayerPrefs.SetInt("ppSeenInhalant", inhalantSeen);
			Time.timeScale = 0.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		SetIntro();
		
	}
}
