using System;
using UnityEngine;
using System.Collections;

public class UIAnimator : MonoBehaviour {
	public event Action OnGo;

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


	public void DoGo() {
		if(OnGo != null) {
			OnGo();
		}
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
