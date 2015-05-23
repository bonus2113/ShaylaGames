using UnityEngine;
using System.Collections;

public class PostEventOnAwake : MonoBehaviour
{
    public GameObject PostOn;

    private void Start()
    {
        if (!PostOn)
            return;
        var node = GetComponent<EventNode>();
        AudioNodesManager.PostEvent(node.name, PostOn);
    }
}