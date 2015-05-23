using UnityEngine;
using System.Collections;
using Atomtwist;

public class MicController : MonoBehaviour {

	[Range(0,55)]
	public float sensitivity = 1;

	private static MicController _instance;
	public static MicController instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType<MicController>();
			return _instance;
		}
	}


	public static float InputRaw()
	{
		var input = Mathf.Clamp01(MicInput.MicLoudness * MicController.instance.sensitivity);
		return input;
	}

	public static float InputSmoothed(float smoothTime)
	{
		var input = Mathf.Clamp01(MicInput.MicLoudness * MicController.instance.sensitivity);
		input.ATSmoothValue(smoothTime);
		return input;
	}



}
