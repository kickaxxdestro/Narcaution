using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    public static AudioManager audioManager;

    public AudioClip MainMenuBGM;
    public AudioClip GalleryBGM;
    public AudioClip CutsceneBGM;
    public AudioClip CreditsBGM;
    public AudioClip LevelSelectBGM;
    public AudioClip ShopBGM;
    public AudioClip StartEndBGM;
    public AudioClip PreproomBGM;
    public AudioClip MinigameBGM;

    public AudioClip World1BGM;
    public AudioClip World2BGM;
    public AudioClip World3BGM;
    public AudioClip World4BGM;
    public AudioClip World5BGM;

    public AudioClip IceBossBGM;
    public AudioClip WeedBossBGM;
    public AudioClip InhalantBossBGM;
    public AudioClip EcstacyBossBGM;
    public AudioClip LSDBossBGM;
    public AudioClip NPSBossBGM;

    private AudioSource audioSource;
    private ObjectPooler positiveButtonPooler;

    void Awake()
    {
        if (!audioManager)
        {
            audioManager = this;
            DontDestroyOnLoad(this);
        }
        else if(audioManager != this)
        {
            Destroy(this);
        }

        audioSource = GetComponent<AudioSource>();
        positiveButtonPooler = GetComponent<ObjectPooler>();

		UpdateVolume ();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	public void UpdateBGM () {
        if (SceneManager.GetActiveScene().name == "preproomScreen")
            return;
        audioSource.Stop();

	    switch(SceneManager.GetActiveScene().name)
        {
            case "mainMenuSliding":
                audioSource.clip = MainMenuBGM;
                break;
            case "galleryNew":
                audioSource.clip = GalleryBGM;
                break;
            case "gameScene":
                int levelNum = PlayerPrefs.GetInt("ppSelectedLevel", 1);

                if (levelNum <= 3)
                    audioSource.clip = World1BGM;
                else if (levelNum == 4)
                    audioSource.clip = IceBossBGM;
                else if (levelNum <= 7)
                    audioSource.clip = World2BGM;
                else if (levelNum == 8)
                    audioSource.clip = WeedBossBGM;
                else if (levelNum <= 11)
                    audioSource.clip = World3BGM;
                else if (levelNum == 12)
                    audioSource.clip = InhalantBossBGM;
                else if (levelNum <= 15)
                    audioSource.clip = World4BGM;
                else if (levelNum == 16)
                    audioSource.clip = EcstacyBossBGM;
                else if (levelNum <= 19)
                    audioSource.clip = World5BGM;
                else if (levelNum == 20)
                    audioSource.clip = LSDBossBGM;
                else if (levelNum == 21)
                    audioSource.clip = NPSBossBGM;
                break;
            case "cutsceneScreen":
                audioSource.clip = CutsceneBGM;
                break;
            case "levelSelect":
                audioSource.clip = LevelSelectBGM;
                break;
            //case "preproomScreen":
            //    audioSource.clip = PreproomBGM;
            //    break;
            case "minigameScreen":
                audioSource.clip = MinigameBGM;
                break;
            case "gameEnd":
                audioSource.clip = CreditsBGM;
                break;
            case "shopScreen":
                audioSource.clip = ShopBGM;
                break;
        }
        audioSource.Play();
	}

    public void UpdateVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("ppBGMVolume", 1.0f);
    }

    public void PlayDefaultButtonSound()
    {
        GameObject go = positiveButtonPooler.GetPooledObject();
        go.SetActive(true);
    }
}
