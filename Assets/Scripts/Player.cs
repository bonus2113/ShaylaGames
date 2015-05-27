using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  public float MaxSpeed = 10;
  public float StrokeAcceleration = 10;
  public float Friction = 2;

  public float ConstrainRadius = 4;
  public Vector3 ConstrainOrigin = new Vector3(0, -5, 0);
  public Vector3 ConstrainDirection = new Vector3(0, 0, 1);

  public ParticleSystem BreathParticles;
public SoulManager SoulManager;
  public ZigSkeleton avatar;
	
  private Vector3 vel;
  private Vector3 acc;
  private Transform transform;

  private Vector3 leftHandPos = Vector3.zero;
  private Vector3 rightHandPos = Vector3.zero;

  private float leftTimer = 0;
  private float rightTimer = 0;

	public bool canMove = true;

	// Use this for initialization
	void Start () {
	  vel = Vector3.zero;
	  transform = GetComponent<Transform>();
    leftHandPos = Vector3.zero;
    rightHandPos = Vector3.zero;

	  FindObjectOfType<BubbleMaster>().DidBreath += Breath;
	}

	void Die() {
		SoulManager.AddSoul(transform.position);
		Application.LoadLevel(Application.loadedLevel);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Return)) {
			Die();
		}

		if(Input.GetKeyDown(KeyCode.T)) {
			Application.LoadLevel(Application.loadedLevel);
		}

	  if (Input.GetKeyDown(KeyCode.B)) {
	    Breath(.7f);
    }


	  rightTimer -= Time.deltaTime;
    leftTimer -= Time.deltaTime;

	  Vector3 lastLeft = leftHandPos;
	  Vector3 lastRight = rightHandPos;

    leftHandPos = avatar.LeftWrist.position - avatar.Waist.position;
    rightHandPos = avatar.RightWrist.position - avatar.Waist.position;

	  float leftDiff = (leftHandPos.z - lastLeft.z);
    float rightDiff = (rightHandPos.z - lastRight.z);
	  float diff = leftDiff + rightDiff;
	  diff *= 0.5f;

    float actualFriction = Friction;

	  if (Input.GetKeyDown(KeyCode.Space)) {
      acc += transform.forward * StrokeAcceleration * 20;
	  }

	  if (diff < -Time.deltaTime*0.15f || Input.GetKeyDown(KeyCode.Space)) {
	    if (Mathf.Abs(leftHandPos.x - rightHandPos.x) > 0.7f) {
        if (leftDiff < -Time.deltaTime * 0.15f) {
	        StrokeLeft();  
	      }

        if (rightDiff < -Time.deltaTime * 0.15f) {
          StrokeRight();
        }

	      acc += Camera.main.transform.forward*StrokeAcceleration*Mathf.Abs(diff/(Time.deltaTime*0.15f));
	    }
	  } else {
      if (Mathf.Abs(leftHandPos.x - rightHandPos.x) > 0.7f) {
        if (diff > Time.deltaTime*0.25f) {
					acc -= Camera.main.transform.forward * StrokeAcceleration * 0.5f * Mathf.Abs(diff / (Time.deltaTime * 0.25f));
        }
        actualFriction = 1.0f * Mathf.Abs(leftHandPos.x - rightHandPos.x);
      }
	  }

		if(!canMove) acc = Vector3.zero;

    vel += acc*Time.deltaTime;
    vel -= vel * actualFriction * Time.deltaTime;

	  vel = Vector3.ClampMagnitude(vel, MaxSpeed);

	  transform.position += vel*Time.deltaTime;
    Constrain();
	  acc = Vector3.zero;
	}

  void StrokeLeft() {
    if (leftTimer <= 0) {
      AudioController.SwimStrokeLeftArm();
      leftTimer = 1.0f;
    }
  }

  void StrokeRight() {
    if (rightTimer <= 0) {
      AudioController.SwimStrokeRightArm();
      rightTimer = 1.0f;
    }
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
