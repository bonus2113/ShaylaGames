using UnityEngine;

public class PositionInfront : MonoBehaviour {

  public Transform Target;
  public Vector3 Axis;
	
	[ContextMenu("Position")]
	void Update () {
	  transform.position = Target.position + Axis;
	}
}
