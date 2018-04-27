using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPointMove))]
public class CheckInteractable : MonoBehaviour {

    // Use this for initialization
    Camera cam;
    PlayerPointMove pointMove;
    public Interactable focus;
    public LayerMask movementMask;

	void Start () {
        cam = Camera.main;
        pointMove = GetComponent<PlayerPointMove>();
	}
	
	// Update is called once per frame
	void Update () {
		
        // If we press Left mouse :
        if (Input.GetMouseButtonDown(0))
        {
            //We create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Move our player to mouse
                pointMove.MoveToPoint(hit.point);
                //Stop focusing on objects
                RemoveFocus();
            }
        }

        // If we press Right mouse:
        if (Input.GetMouseButtonDown(1))
        {
            //We create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
               // Check if we hit an interatable
               //If we set it as our focus
               if (interactable != null) {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus (Interactable newFocus)
    {
		if (newFocus != focus) {
			if (focus != null)
				focus.OnDefocused ();
			
			focus = newFocus;
			pointMove.FollowTarget(newFocus);
		}

		newFocus.OnFocused (transform);
    }
    void RemoveFocus()
    {
		if (focus != null)
			focus.OnDefocused ();
		
        focus = null;
        pointMove.StopFollowingTarget();
    }
}
