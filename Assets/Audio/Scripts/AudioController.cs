using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using Atomtwist;
using Kaae;

public class AudioController : MonoBehaviour {

	private static AudioController _instance;
	public static AudioController instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType<AudioController>();
			return _instance;
		}
	}

  public delegate void IntroDelegate();
  public static event IntroDelegate OnIntroStart;
	public AudioMixer underwaterMixer;
	public AudioMixer musicMixer;
	public GameObject leftArm;
	public GameObject rightArm;
	public AudioMixerSnapshot musicOverwater;
	public AudioMixerSnapshot musicUnderwater;
	public AudioMixerSnapshot dive;
	public AudioMixerSnapshot silent;
	public bool playIntro;

	void Start()
	{
		if (playIntro) 
		{
			AudioController.StartIntro();
		} 
		else
		{
			dive.TransitionTo(0);
			musicUnderwater.TransitionTo(0);
		}
    AudioNodesManager.PostEvent("StartAmbience",gameObject);
	}

	public static void SetAudioDivingHeight(float normalizedHeight)
	{

		AudioController.instance.underwaterMixer.SetFloat("HeightPitch",normalizedHeight);
	}


	public static void SwimStrokeLeftArm()
	{
		AudioNodesManager.PostEvent("SwimStroke", AudioController.instance.leftArm);
	}

	public static void SwimStrokeRightArm()
	{
		AudioNodesManager.PostEvent("SwimStroke", AudioController.instance.rightArm);
	}

	public static void StartIntro() {
	  if (OnIntroStart != null) 
    {
	    OnIntroStart();
    }
    AudioController.instance.silent.TransitionTo(0);
    AudioController.instance.musicOverwater.TransitionTo(0);
    AudioNodesManager.PostEvent("StartMusic", AudioController.instance.gameObject);
		AudioController.instance.StartCoroutine(AudioController.instance.MusicDive());
	}

	[DebugButton]
	public void Dive()
	{
		dive.TransitionTo(1);
		musicUnderwater.TransitionTo(1.3f);
		AudioNodesManager.PostEvent("Splash", gameObject);
	}

	IEnumerator MusicDive()
	{
		yield return new WaitForSeconds(22.29f);
		Dive ();
	}


	void Update()
	{
		float playerHeightForMixer = Camera.main.transform.position.y ;
		playerHeightForMixer = playerHeightForMixer.ATScaleRange(-9,-1,0.75f,1.2f);
		playerHeightForMixer = Mathf.Clamp(playerHeightForMixer,0.75f,1.2f);
		underwaterMixer.SetFloat("HeightPitch",playerHeightForMixer);

		if (Input.GetKeyDown(KeyCode.C))
		{
			AudioController.SwimStrokeLeftArm();
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			AudioController.SwimStrokeRightArm();
		}

	}

}
