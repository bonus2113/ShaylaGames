using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

public class SoulManager : MonoBehaviour {

	public GameObject SoulPrefab;

	List<Vector3> soulPositions = new List<Vector3>();
	XmlSerializer serializer = new XmlSerializer(typeof(List<Vector3>));

	void OnEnable() {
		var soulString = PlayerPrefs.GetString("Souls", "");

		if(soulString == "") return;

		using (var strReader = new StringReader(soulString)) {
			soulPositions = (List<Vector3>)serializer.Deserialize(strReader);
		}

		foreach(var pos in soulPositions) {
			SpawnSoul(pos);
		}
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.R)) {
			ClearSouls();
		}
	}

	void Serialize() {
		using (var strWriter = new StringWriter()) {
			serializer.Serialize(strWriter, soulPositions);
			PlayerPrefs.SetString("Souls", strWriter.ToString());
		}
	
	}

	public void ClearSouls() {
		foreach(Transform child in transform) {
			Destroy(child);
		}

		soulPositions.Clear();
		Serialize();
	}

	public void AddSoul(Vector3 position) {
		soulPositions.Add(position);
		SpawnSoul(position);
		Serialize();
	}

	private void SpawnSoul(Vector3 pos) {
		var obj = (GameObject)GameObject.Instantiate(SoulPrefab, pos, Quaternion.identity);
		obj.transform.parent = transform;
		obj.transform.position = pos;
	}
}
