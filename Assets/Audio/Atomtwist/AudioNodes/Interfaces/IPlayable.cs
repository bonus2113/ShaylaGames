using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface IPlayable : IEventSystemHandler {
	void Play(float volume, float pitch, float delay);
	void Stop();
}
