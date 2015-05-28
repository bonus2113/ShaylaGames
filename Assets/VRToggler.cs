using Kaae;
using UnityEngine;
using System.Collections;

public class VRToggler : MonoBehaviour {

  public Object[] ToToggle;

  [DebugButton]
  public void Toggle() {

    foreach (var o in ToToggle) {
      if (o is GameObject) {
        var go = o as GameObject;
        if (go.name == "OVRCameraRig") {
          go.GetComponent<OVRCameraRig>().enabled = !go.GetComponent<OVRCameraRig>().enabled;
          go.GetComponent<OVRManager>().enabled = !go.GetComponent<OVRManager>().enabled;
        } else {
          go.SetActive(!go.activeSelf);
        }
      } else if (o is MonoBehaviour) {
        var mo = o as MonoBehaviour;
        mo.enabled = !mo.enabled;
      }
    }
  }

}
