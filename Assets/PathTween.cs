using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Linq;

public class PathTween : MonoBehaviour {

	public Transform[] Points;
	public PathType pathType;
	public float Duration;
	public float TurnSpeed;
	public bool ClosePath;

	// Use this for initialization
	void Start () {
		Tween t = transform.DOPath(Points.ToList().ConvertAll( (tr) => tr.position).ToArray(), Duration, pathType)
			.SetOptions(ClosePath)
				.SetLookAt(TurnSpeed);
		// Then set the ease to Linear and use infinite loops
		t.SetEase(Ease.Linear).SetLoops(-1);
	}
}
