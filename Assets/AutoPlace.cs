using UnityEditor;
using UnityEngine;
using System.Collections;

public class AutoPlace : MonoBehaviour {

  private Transform cachedTransform;
  private Transform ownTransform;
  private MeshRenderer renderer;

  void Awake() {
    cachedTransform = Camera.main.transform;
    ownTransform = GetComponent<Transform>();
    renderer = GetComponent<MeshRenderer>();
  }

  void FixedUpdate() {
    if (Vector3.Distance(cachedTransform.position, ownTransform.position) > 40) {
      if (renderer.enabled) {
        renderer.enabled = false;
      }
    } else if (!renderer.enabled) {
      renderer.enabled = true;
    }
  }

#if UNITY_EDITOR
  [MenuItem("Shayla/PlaceAll")]
  public static void PlaceAll() {
    var objs = GameObject.FindObjectsOfType<AutoPlace>();
    foreach (var autoPlace in objs) {
      autoPlace.DoPlace();
    }
  }


  void DoPlace() {
    RaycastHit hit;
    if (Physics.Raycast(transform.position + Vector3.up*1000, -Vector3.up, out hit)) {
      transform.position = hit.point;
      transform.Rotate(0, Random.Range(0, 180.0f), 0, Space.World);
    }
  }
#endif

}
