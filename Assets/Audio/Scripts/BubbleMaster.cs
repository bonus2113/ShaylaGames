using UnityEngine;
using System.Collections;

public class BubbleMaster : MonoBehaviour {

	[Range(0,1)]
	public float micThreshold = 0.5f;
	public float micValue;
	public GameObject bubblePrefab;
	
	bool _isPlaying;

	void CheckMicInput()
	{
		if (MicController.InputSmoothed(0.5f) > micThreshold && !_isPlaying)
		{
			_isPlaying = true;
			AudioNodesManager.PostEvent("BubbleLoopStart", gameObject);
		} 
		if (MicController.InputSmoothed(0.5f) < micThreshold && _isPlaying)
		{
			_isPlaying = false;
			var bubbleTail = GameObject.Instantiate<GameObject>(bubblePrefab as GameObject) ;
			bubbleTail.transform.position = transform.position;
			StartCoroutine("WaitAndStop");
		}
	}

	IEnumerator WaitAndStop()
	{
		yield return new WaitForSeconds(0.5f);
		AudioNodesManager.PostEvent("BubbleLoopStop", gameObject);
	}

	// Update is called once per frame
	void Update () {
		CheckMicInput();
	}
}
