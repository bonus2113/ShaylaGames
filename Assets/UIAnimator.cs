using System;
using UnityEngine;
using System.Collections;

public class UIAnimator : MonoBehaviour {
	public event Action OnGo;

  void OnEnable() 
  {
      AudioController.OnIntroStart += AudioControllerOnOnIntroStart;  
	  AudioController.StartIntro();
  }
  void OnDisable() {
    AudioController.OnIntroStart -= AudioControllerOnOnIntroStart;
  }
  void OnDestroy() {
    AudioController.OnIntroStart -= AudioControllerOnOnIntroStart;
  }


	public void DoGo() {
	FindObjectOfType<Player>().canMove = true;
		if(OnGo != null) {
			OnGo();
		}
	}

  private void AudioControllerOnOnIntroStart() {
	FindObjectOfType<Player>().canMove = true;
    GetComponent<Animator>().SetTrigger("PlayIntro");
  }

  // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
