using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Kaae;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class VRToggler : MonoBehaviour {

  public GameObject DefaultCam;
  public Object[] ToToggle;



  private void DistComps(List<Component> comps, GameObject go) {
    foreach (var comp in go.GetComponents<Component>().Where(c => !(c is Camera) && !(c is Transform))) {
      DestroyImmediate(comp);
    }

    foreach (var camComponent in comps) {
      go.AddComponent(camComponent);
    }
  }

  [DebugButton]
  public void Toggle() {

    foreach (var o in ToToggle) {
      if (o is GameObject) {
        var go = o as GameObject;
        if (go.name == "OVRCameraRig") {
          var camRig = go.GetComponent<OVRCameraRig>();
          camRig.enabled = !go.GetComponent<OVRCameraRig>().enabled;
          go.GetComponent<OVRManager>().enabled = !go.GetComponent<OVRManager>().enabled;
          go.GetComponent<SimpleMouseRotator>().enabled = !go.GetComponent<SimpleMouseRotator>().enabled;

          var defaultCamComponents = DefaultCam.GetComponents<Component>().Where(c => !(c is Camera) && !(c is Transform)).ToList();
          
          DistComps(defaultCamComponents, camRig.leftEyeAnchor.gameObject);
          DistComps(defaultCamComponents, camRig.rightEyeAnchor.gameObject);
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
