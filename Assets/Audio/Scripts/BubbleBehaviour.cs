using UnityEngine;
using System.Collections;


public class BubbleBehaviour : MonoBehaviour {

	public float speed = 0.5f;
	
	void OnEnable()
	{
		transform.position = Camera.main.transform.position;
		transform.SetParent(Camera.main.transform);
		AudioNodesManager.PostEvent("Bubble", gameObject);
		StartCoroutine(WaitAndDestroy());
	}

	IEnumerator WaitAndDestroy()
	{
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + speed * Time.deltaTime, transform.localPosition.z);
	}
}
