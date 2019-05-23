using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController Player;
    public Hearts Health;

    public Vector2 transitionGrid = new Vector2(16, 10);
    public float TransitionSpeed = 10f;
    private bool transitioning = false;
    private Vector3 transitionDestination;
    public float TransitionBuffer = 0.1f;

    public bool DungeonExit = false;

    //public BoxCollider2D[] MyColliders;
	// Use this for initialization
	void Start () 
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 4.0f / 3.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = Camera.main;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (DungeonExit)
        {
            transitioning = false;
            Player.playerCanMove = true;
        }
        if (transitioning)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, transitionDestination, TransitionSpeed * Time.deltaTime);
            transform.position = newPosition;
            Player.cameraTransition = true;
            Player.playerCanMove = false;

            if(Vector3.Distance(transitionDestination, transform.position) < TransitionBuffer)
            {
                transform.position = transitionDestination;
                transitioning = false;
                Player.cameraTransition = false;
                Player.playerCanMove = true;
                GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().FinishAreaMove();
            }
        }
	}

    public void SetDungeonExit(bool state)
    {
        DungeonExit = state;
        
    }

    public bool GetTransitioning()
    {
        return transitioning;
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player" && !transitioning)
        {
            handlePlayerTracking();
        }
    }

    float edgeBuffer = 0.7f;
    private void handlePlayerTracking()
    {
        //moving left
        if(Player.transform.position.x <= transform.position.x - transitionGrid.x / 2 + edgeBuffer)
        {
            SceneTransition(new Vector2(-1, 0));
        }
        //moving right
        else if (Player.transform.position.x >= transform.position.x + transitionGrid.x / 2 - edgeBuffer)
        {
            SceneTransition(new Vector2(1, 0));
        }
        //moving up
        else if (Player.transform.position.y >= transform.position.y + transitionGrid.y / 2 - edgeBuffer)
        {
            SceneTransition(new Vector2(0, 1));
        }
        //moving down
        else if (Player.transform.position.y <= transform.position.y - transitionGrid.y / 2 + edgeBuffer)
        {
            SceneTransition(new Vector2(0, -1));
        }
    }

    public void SceneTransition(Vector2 direction)
    {
        if (!transitioning) //check level handler for transition check
        {
            transitioning = true;
            transitionDestination = direction * transitionGrid;
            transitionDestination += transform.position;
            Debug.Log(transitionDestination.ToString());

            //Player.cameraTransition = false;
            Player.playerCanMove = false;

            Player.cameraTransitionDirection = direction;
            Player.cameraTransition = true;
        }
        else
        {
            Player.cameraTransitionDirection = direction;
            Player.cameraTransition = true;

        }
    }
}
