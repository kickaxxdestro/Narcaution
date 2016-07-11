using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bgm : MonoBehaviour {
	
	AudioSource src;
	
	public static Bgm current;
	
	public AudioClip iceWorldBGM;
	public AudioClip mainMenuBGM;
	public AudioClip galleryBGM;
	public AudioClip tutorialBGM;
	public AudioClip iceWorldBoss;
	public AudioClip cannabisBGM;
	public AudioClip cannabisBoss;
	public AudioClip InhalantBGM;
	public AudioClip InhalantBoss;
	public AudioClip EcstasyBGM;
	public AudioClip EcstasyBoss;
	public AudioClip LsdBGM;
	public AudioClip LsdBoss;
	public AudioClip SecretBoss;
	
	void Awake ()
	{
		if(current != null)
		{
			Destroy(this.gameObject);	
		}
		else
		{
			current = this;	
		}
		DontDestroyOnLoad(this);
		
		src = GetComponent<AudioSource>();		
	}	
	
	void Update ()
	{
		src.volume = PlayerPrefs.GetFloat("ppBGMVolume", 1.0f);
		switch(Application.loadedLevelName)
		{	
			case "mainMenu" :
			if(src.clip != mainMenuBGM)
			{
				src.Stop();
				src.clip = mainMenuBGM;
			}
			else
			{
				if(src.isPlaying == false)
				{
					src.clip = mainMenuBGM;
					src.Play();
				}
			}
			break;
			
			case "tutorialScene":
			if(src.clip != tutorialBGM)
			{
				src.Stop();
				src.clip = tutorialBGM;
			}
			else
			{
				if(src.isPlaying == false)
				{
					src.clip = tutorialBGM;
					src.Play();
				}
			}
			break;

			case "gallery" :
			if(src.clip != galleryBGM)
			{
				src.Stop();
				src.clip = galleryBGM;
			}
			else
			{
				if(src.isPlaying == false)
				{
					src.clip = galleryBGM;
					src.Play();
				}
			}
			break;
			
			case "worldSelect" :
				if(src.clip != mainMenuBGM)
			{
				src.Stop();
				src.clip = mainMenuBGM;
			}
			else
			{
				if(src.isPlaying == false)
				{
					src.clip = mainMenuBGM;
					src.Play();
				}
			}
			break;
			
			case "worldOne" :
			if(src.clip != mainMenuBGM)
			{
				src.Stop();
				src.clip = mainMenuBGM;
			}
			else
			{
				if(src.isPlaying == false)
				{
					src.clip = mainMenuBGM;
					src.Play();
				}
			}
			break;
			
			case "worldTwo" :
			if(src.clip != mainMenuBGM)
			{
				src.Stop();
				src.clip = mainMenuBGM;
			}
			else
			{
				if(src.isPlaying == false)
				{
					src.clip = mainMenuBGM;
					src.Play();
				}
			}
			break;

			case "worldThree" :
				if(src.clip != mainMenuBGM)
				{
					src.Stop();
					src.clip = mainMenuBGM;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = mainMenuBGM;
						src.Play();
					}
				}
				break;

			case "worldFour" :
				if(src.clip != mainMenuBGM)
				{
					src.Stop();
					src.clip = mainMenuBGM;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = mainMenuBGM;
						src.Play();
					}
				}
				break;

			case "worldFive" :
				if(src.clip != mainMenuBGM)
				{
					src.Stop();
					src.clip = mainMenuBGM;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = mainMenuBGM;
						src.Play();
					}
				}
				break;
			
			case "gameScene":
			if(PlayerPrefs.GetString("ppSelectedLevel") == "Level1" || PlayerPrefs.GetString("ppSelectedLevel") == "Level2"  || PlayerPrefs.GetString("ppSelectedLevel") == "Level3")
			{
				if(src.clip != iceWorldBGM)
				{
					src.Stop();
					src.clip = iceWorldBGM;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = iceWorldBGM;
						src.Play();
					}
				}
			}
			else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level4" )
			{
				if(src.clip != iceWorldBoss)
				{
					src.Stop();
					src.clip = iceWorldBoss;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = iceWorldBoss;
						src.Play();
					}
				}
			}
			else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level5" || PlayerPrefs.GetString("ppSelectedLevel") == "Level6"  || PlayerPrefs.GetString("ppSelectedLevel") == "Level7")
			{
				if(src.clip != cannabisBGM)
				{
					src.Stop();
					src.clip = cannabisBGM;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = cannabisBGM;
						src.Play();
					}
				}
			
			}
			else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level8")
			{
				if(src.clip != cannabisBoss)
				{
					src.Stop();
					src.clip = cannabisBoss;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = cannabisBoss;
						src.Play();
					}
				}
			}
			else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level9" || PlayerPrefs.GetString("ppSelectedLevel") == "Level10"  || PlayerPrefs.GetString("ppSelectedLevel") == "Level11")
			{
				if(src.clip != InhalantBGM)
				{
					src.Stop();
					src.clip = InhalantBGM;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = InhalantBGM;
						src.Play();
					}
				}
				
			}
			else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level12")
			{
				if(src.clip != InhalantBoss)
				{
					src.Stop();
					src.clip = InhalantBoss;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = InhalantBoss;
						src.Play();
					}
				}
			}
			else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level13" || PlayerPrefs.GetString("ppSelectedLevel") == "Level14"  || PlayerPrefs.GetString("ppSelectedLevel") == "Level15")
			{
				if(src.clip != EcstasyBGM)
				{
					src.Stop();
					src.clip = EcstasyBGM;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = EcstasyBGM;
						src.Play();
					}
				}
				
			}
			else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level16")
			{
				if(src.clip != EcstasyBoss)
				{
					src.Stop();
					src.clip = EcstasyBoss;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = EcstasyBoss;
						src.Play();
					}
				}
			}
			else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level17" || PlayerPrefs.GetString("ppSelectedLevel") == "Level18"  || PlayerPrefs.GetString("ppSelectedLevel") == "Level19")
			{
				if(src.clip != LsdBGM)
				{
					src.Stop();
					src.clip = LsdBGM;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = LsdBGM;
						src.Play();
					}
				}
				
			}
			else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level20" || PlayerPrefs.GetString("ppSelectedLevel") == "Level22")
			{
				if(src.clip != SecretBoss)
				{
					src.Stop();
					src.clip = SecretBoss;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = SecretBoss;
						src.Play();
					}
				}
			}
			else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level21")
			{
				if(src.clip != LsdBoss)
				{
					src.Stop();
					src.clip = LsdBoss;
				}
				else
				{
					if(src.isPlaying == false)
					{
						src.clip = LsdBoss;
						src.Play();
					}
				}
			}
			break;
			
			default :
				src.Stop();
			break;
		}
	}
}
