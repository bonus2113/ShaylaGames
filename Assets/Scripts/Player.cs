using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  public float StrokeAcceleration = 10;
  public float Friction = 2;

  public float ConstrainRadius = 4;
  public Vector3 ConstrainOrigin = new Vector3(0, -5, 0);
  public Vector3 ConstrainDirection = new Vector3(0, 0, 1);

  public ParticleSystem BreathParticles;

  private Vector3 vel;
  private Vector3 acc;
  private Transform transform;

	// Use this for initialization
	void Start () {
	  vel = Vector3.zero;
	  transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

	  if (Input.GetKeyDown(KeyCode.B)) {
	    Breath(.7f);
	  }

	  if (Input.GetKeyDown(KeyCode.Space)) {
	    acc += transform.forward * StrokeAcceleration;
	  }

    vel += acc*Time.deltaTime;
	  vel -= vel*Friction*Time.deltaTime;

	  transform.position += vel*Time.deltaTime;
    Constrain();
	  acc = Vector3.zero;
	}

  void Constrain() {
    var posOffset = transform.position - ConstrainOrigin;
    var posProjection = Vector3.Project(posOffset, ConstrainDirection);
    var posDir = posOffset - posProjection;
    if (posDir.magnitude > ConstrainRadius) {
      posDir = Vector3.ClampMagnitude(posDir, ConstrainRadius);
      transform.position = posProjection + ConstrainOrigin + posDir;
      vel = Vector3.Project(vel, ConstrainDirection);
    }
  }

  void Breath(float strength) {
    BreathParticles.Emit((int)(100 * strength));
  }
}
