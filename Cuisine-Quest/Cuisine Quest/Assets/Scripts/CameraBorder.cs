using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBorder : MonoBehaviour {

    public Vector2 TransitionDirection;
    public CameraController CC;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if(CC) CC.SceneTransition(TransitionDirection);
            else //for legacy script
            {
                transform.parent.GetComponent<CameraController>().SceneTransition(TransitionDirection);
            }
            //Debug.Log("Edge Collsion with: " + transform.name);
        }
    }
}
