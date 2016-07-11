using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	float fadeSpeed = 4.0f;          // Speed that the screen fades to and from black.
	
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.
	Image ScreenFadeTexture;
	
	void Start ()
	{
		ScreenFadeTexture = GetComponent<Image>();
	}
	
	void Update ()
	{
		// If the scene is starting...
		if(sceneStarting)
			// ... call the StartScene function.
			StartScene();
		else {
			gameObject.SetActive(false);
			
		}
		
	}
	
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		ScreenFadeTexture.color = Color.Lerp(ScreenFadeTexture.color, Color.clear, fadeSpeed * Time.unscaledDeltaTime);
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		ScreenFadeTexture.color = Color.Lerp(ScreenFadeTexture.color, Color.black, fadeSpeed * Time.unscaledDeltaTime);
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(ScreenFadeTexture.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			ScreenFadeTexture.color = Color.clear;
			//guiTexture.enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		//guiTexture.enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		if(ScreenFadeTexture.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			ScreenFadeTexture.color = Color.clear;
			//guiTexture.enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
}
