using System;
using UnityEngine;
using System.Collections;

public class UIAnimator : MonoBehaviour {

  void OnEnable() 
  {
      AudioController.OnIntroStart += AudioControllerOnOnIntroStart;  
  }
  void OnDisable() {
    AudioController.OnIntroStart -= AudioControllerOnOnIntroStart;
  }
  void OnDestroy() {
    AudioController.OnIntroStart -= AudioControllerOnOnIntroStart;
  }

  private void AudioControllerOnOnIntroStart() {
    GetComponent<Animator>().SetTrigger("PlayIntro");
  }

  // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
