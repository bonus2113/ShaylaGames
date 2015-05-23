using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Linq;
using Kaae;
using UnityEngine.EventSystems;


public enum EventActionType
{
	Play, 
	Stop,
}

public enum EventActionScope
{
	Gameobject,
	Global,
}

[System.Serializable]
public class EventAction
{
	public int uniqueAudioNodeID;
	public EventActionType actionType;
	public GameObject selectedNodeGameObject;
	public EventActionScope actionScope;

	//TODO: Need an applySettings Method here to apply settings as eventactions before play

	public void ExecuteEventAction(GameObject targetGameObject)
	{
		GameObject nodeObject;
		switch (actionType)
		{
		case EventActionType.Play : 
			nodeObject = GameObject.Instantiate<GameObject>(selectedNodeGameObject);
			//make eventInterface to update the settings
			nodeObject.GetComponent<AudioNode>().originAudioNodeID = uniqueAudioNodeID;
			nodeObject.AddComponent<DestroyAfterPlay>();
			nodeObject.transform.SetParent(targetGameObject.transform);
			nodeObject.transform.localPosition = Vector3.zero;
			ExecuteEvents.Execute<IPlayable>(nodeObject,null, (x,y) => x.Play(1, 1, 0) );
			break;
		case EventActionType.Stop  :
			if (actionScope == EventActionScope.Gameobject)
			{
				var nodeObjects = targetGameObject.GetComponentsInChildren<AudioNode>();
				foreach (var n in nodeObjects)
				{
					if (n.originAudioNodeID == uniqueAudioNodeID)
					{
						ExecuteEvents.Execute<IPlayable>(n.gameObject,null, (x,y) => x.Stop() );
					}

				}
			}
			if (actionScope == EventActionScope.Global)
			{
				var nodeObjects = GameObject.FindObjectsOfType<AudioNode>();
				foreach (var n in nodeObjects)
				{
					if (n.originAudioNodeID == uniqueAudioNodeID)
					ExecuteEvents.Execute<IPlayable>(n.gameObject,null, (x,y) => x.Stop() );
				}
			}
			break;
		}
	}


}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EventAction))]
public class EventActionProperty : PropertyDrawer
{
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		GUILayout.BeginHorizontal();
		property.FindPropertyRelative("actionType").enumValueIndex = EditorGUILayout.Popup(property.FindPropertyRelative("actionType").enumValueIndex,property.FindPropertyRelative("actionType").enumDisplayNames);

		DrawAudioNodePopup(property);

		if ( property.FindPropertyRelative("actionType").enumValueIndex == property.FindPropertyRelative("actionType").enumNames.ToList().IndexOf("Stop"))
			property.FindPropertyRelative("actionScope").enumValueIndex = EditorGUILayout.Popup(property.FindPropertyRelative("actionScope").enumValueIndex,property.FindPropertyRelative("actionScope").enumDisplayNames);

		//useful for debugging
		//EditorGUILayout.IntField(property.FindPropertyRelative("uniqueAudioNodeID").intValue);

		GUILayout.EndHorizontal();
	}

	void DrawAudioNodePopup(SerializedProperty property)
	{
		EditorGUI.BeginChangeCheck();
		var audioNodes = GameObject.FindObjectsOfType<AudioNode>().ToList();
		var audioNodeID = property.FindPropertyRelative ("uniqueAudioNodeID").intValue;
		var audioNodeNames = audioNodes.Select(n => n.name).ToArray();
		var audioNodeIDList = audioNodes.Select (n => n.uniqueID).ToList();
		var selectedIndex = EditorGUILayout.Popup(audioNodeIDList.IndexOf(property.FindPropertyRelative ("uniqueAudioNodeID").intValue), audioNodeNames);
		selectedIndex = Mathf.Clamp(selectedIndex,0,int.MaxValue);
		var selectedNode = audioNodes[selectedIndex];
		property.FindPropertyRelative("selectedNodeGameObject").objectReferenceValue = selectedNode.gameObject;
		if( EditorGUI.EndChangeCheck()) 
		{
			property.FindPropertyRelative ("uniqueAudioNodeID").intValue = selectedNode.uniqueID;
		}
	}
}
#endif

public class EventNode : Node {

	[SerializeField]
	public EventAction[] eventAction;
		
	public void PostEvent(int uniqueEventID)
	{
		if (uniqueID != uniqueEventID) return;
		foreach (var e in eventAction)
		{
			e.ExecuteEventAction(FindObjectOfType<AudioNodesManager>().gameObject);
		}
	}
	

	public void PostEvent(string eventName)
	{
		if (eventName != name) return;
		foreach (var e in eventAction)
		{
			e.ExecuteEventAction(FindObjectOfType<AudioNodesManager>().gameObject);
		}
	}
	
	public void PostEvent(int uniqueEventID, GameObject targetGameObject)
	{
		if (uniqueID != uniqueEventID) return;
		foreach (var e in eventAction)
		{
			e.ExecuteEventAction(targetGameObject);
		}
	}
	

	public void PostEvent(string eventName, GameObject targetGameObject)
	{
		if (eventName != name) return;
		foreach (var e in eventAction)
		{
			e.ExecuteEventAction(targetGameObject);
		}
	}

	[DebugButton]
	public void AuditionEvent()
	{
		foreach (var e in eventAction)
		{
			e.ExecuteEventAction(FindObjectOfType<AudioNodesManager>().gameObject);
		}
	}

	[DebugButton]
	public void StopAudition()
	{
		var audioNodePlayers = FindObjectsOfType<AudioNode>().Cast<IPlayable>();
		foreach (var p in audioNodePlayers)
		{
			p.Stop();
		}
	}




}
