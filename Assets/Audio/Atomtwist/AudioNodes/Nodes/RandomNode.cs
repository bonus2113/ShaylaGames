using UnityEngine;
using System.Collections;
using Kaae;
using System.Collections.Generic;
using System.Linq;

public class RandomNode : AudioNode, IPlayable {

	#region IPlayable implementation
	public void Play (float volume, float pitch, float delay)
	{
		if (playableNodes.Count == 0 || playableNodes == null) return;
		var r = Random.Range(0,playableNodes.Count);
		playableNodes[r].Play(volume * nodevolume, pitch * nodePitch, delay + nodeDelay);
	}
	
	[DebugButton]
	public void Stop ()
	{
		if (playableNodes.Count == 0 || playableNodes == null) return;
		foreach (var i in playableNodes)
		{
			i.Stop();
		}
	}
	#endregion
	
	public List<IPlayable> playableNodes = new List<IPlayable>();
	
	// Use this for initialization
	public override void OnEnable () {
		base.OnEnable();
		//find all playable child nodes and add them to the list
		GetChildNodes();
	}
	
	void GetChildNodes()
	{
		playableNodes.Clear();
		playableNodes = transform.GetChildren().Select(c => c.GetComponent<IPlayable>()).ToList();
	}
}
