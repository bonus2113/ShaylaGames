using UnityEngine;
using System.Collections;
using Kaae;

[RequireComponent(typeof(AudioSource))]
public class SFXNode : AudioNode, IPlayable {

	#region IPlayable implementation
	public void Play (float volume, float pitch, float delay)
	{
		_audiosource.clip = clip;
		_audiosource.volume = volume * nodevolume;
		_audiosource.pitch = pitch * nodePitch;
		_audiosource.PlayScheduled(AudioSettings.dspTime + delay + nodeDelay);
	}
	[DebugButton]
	public void Stop ()
	{
		_audiosource.Stop();
	}
	#endregion

	public AudioClip clip;

	AudioSource _audiosource;
	public override void OnEnable()
	{
		base.OnEnable();
		_audiosource = GetComponent<AudioSource>();
	}
}
