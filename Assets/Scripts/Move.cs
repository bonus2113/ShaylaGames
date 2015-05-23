using UnityEngine;

public class Move : MonoBehaviour {
  public Vector3 Dir;
  public float Speed;

  private Transform transform;

  private void Start() {
    transform = GetComponent<Transform>();
  }

  void Update () {
    transform.position += Dir.normalized*Speed*Time.deltaTime;
  }
}
