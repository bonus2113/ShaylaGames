using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioNodesManager : MonoBehaviour {


	private static AudioNodesManager _instance;

	public static AudioNodesManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<AudioNodesManager>();
			}
			return _instance;
		}
	}


	public AudioNodeEventPreview eventNodePreview;

	List<EventNode> eventNodes;
	void OnEnable()
	{
		eventNodes = GameObject.FindObjectsOfType<EventNode>().ToList();
	}
	

	//static wrappers
	
	public static void PostEvent(string eventName, GameObject targetObject)
	{
		foreach (var e in AudioNodesManager.instance.eventNodes)
		{
			e.PostEvent(eventName, targetObject);
		}
	}



  //contextMenu
#if UNITY_EDITOR 
	//TODO: Make Convert to Context Menu actions

	[MenuItem("GameObject/AudioNodes/Create SFX Node", false, 0)]
	public static void CreateSFXNode()
	{
		var selectedObject = Selection.activeObject as GameObject;
		var newNode = new GameObject();
		newNode.AddComponent<SFXNode>();
		newNode.GetComponent<AudioSource>().playOnAwake = false;
		newNode.transform.SetParent(selectedObject.transform);
	}

	[MenuItem("GameObject/AudioNodes/Create Multi Node", false, 0)]
	public static void CreateMultiNode()
	{
		var selectedObject = Selection.activeObject as GameObject;
		var newNode = new GameObject();
		newNode.AddComponent<MultiNode>();
		newNode.transform.SetParent(selectedObject.transform);
	}

	[MenuItem("GameObject/AudioNodes/Create Random Node", false, 0)]
	public static void CreateRandomNode()
	{
		var selectedObject = Selection.activeObject as GameObject;
		var newNode = new GameObject();
		newNode.AddComponent<RandomNode>();
		newNode.transform.SetParent(selectedObject.transform);
	}

	//TODO: make this create event from AudioNodes & put it into the right place in hierarchy
	[MenuItem("GameObject/AudioNodes/Create EventNode", false, 0)]
	public static void CreateEventNode()
	{
		var selectedObject = Selection.activeObject as GameObject;
		var newNode = new GameObject();
		newNode.AddComponent<EventNode>();
		newNode.transform.SetParent(selectedObject.transform);
	}
#endif
}

[System.Serializable]
public class AudioNodeEventPreview
{

}


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(AudioNodeEventPreview))]
public class AudioNodeEventPreviewProperty : PropertyDrawer
{

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		var events = GameObject.FindObjectsOfType<EventNode>().ToList();
		EditorGUILayout.LabelField("Preview Events:");

		foreach (var e in events)
		{
			if (GUILayout.Button(e.name))
			{
				e.PostEvent(e.uniqueID);
			}
		}

	}

}

#endif

