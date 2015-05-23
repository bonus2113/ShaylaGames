using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using Kaae;

public abstract class AudioNode : Node {

	//mixer routing
	public struct MixerGroupProperty
	{
		public AudioMixerGroup mixerGroup;
		public bool overrideParentMixerGroup;
	}

	[HideInInspector]
	public int originAudioNodeID;

	//TODO: Auditioning needs to be its owen Inteface & Push 2D Settings when triggered from Editor
	[DebugButton]
	public void Audition()
	{
		var p = this as IPlayable;
		p.Play(nodevolume, nodePitch, nodeDelay);
	}

	//playback related
	[Range(0,1)]
	public float nodevolume = 1;
	[Range(0,2)]
	public float nodePitch = 1;
	[Range(0,2)]
	public float nodeDelay;


}
