using System;
using UnityEngine;
using System.Collections;

public class UIAnimator : MonoBehaviour {
  private void OnEnable() {
    AudioController.OnIntroStart += AudioControllerOnOnIntroStart;
    //AudioController.StartIntro();
  }

  private void OnDisable() {
    AudioController.OnIntroStart -= AudioControllerOnOnIntroStart;
  }

  private void OnDestroy() {
    AudioController.OnIntroStart -= AudioControllerOnOnIntroStart;
  }


  public void DoGo() {
  }

  private void AudioControllerOnOnIntroStart() {
    GetComponent<Animator>().SetTrigger("PlayIntro");
  }

  // Use this for initialization
  private void Start() {

  }

  // Update is called once per frame
  private void Update() {

  }
}
