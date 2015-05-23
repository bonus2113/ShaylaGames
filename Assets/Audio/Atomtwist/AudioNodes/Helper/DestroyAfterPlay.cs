using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class DestroyAfterPlay : MonoBehaviour {

	List<AudioSource> audioSources;

	void OnEnable()
	{
		audioSources = GetComponentsInChildren<AudioSource>().ToList();
#if UNITY_EDITOR
		if (!Application.isPlaying)
			EditorApplication.update += EditorUpdate;
#endif
	}

	void EditorUpdate ()
	{
		DestroyIfNotPlaying();
	}

	void DestroyIfNotPlaying()
	{
		if (Application.isPlaying) 
		{
			if (AnySourcePlaying()) return;
			Destroy (gameObject);
		}
#if UNITY_EDITOR
		if (!Application.isPlaying)
		{
			if (audioSources[0] == null) return;
			if (AnySourcePlaying()) return;
			EditorApplication.update -= EditorUpdate;
			DestroyImmediate(gameObject);
		}
#endif

	}

	void OnDestroy()
	{
#if UNITY_EDITOR
		if (!Application.isPlaying)
			EditorApplication.update -= EditorUpdate;
#endif
	}

	bool AnySourcePlaying()
	{
		if (audioSources.FirstOrDefault(s => s.isPlaying) ) return true;
		else
			return false;
	}

	// Update is called once per frame
	void Update () {
		DestroyIfNotPlaying();

	}
}
