using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    Camera cam;
    PlayerMotor playerMotor;
    Interactable focus;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        playerMotor = GetComponent<PlayerMotor>();
        CreateHoverUI.instance.onDialogStartedCallback += WaitForDialog;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (CameraManager.instance.activeCamera == CameraManager.Cameras.RPG) //if active camera is rpg than get inputs
        {
            if (Input.GetMouseButtonDown(0)) //when pressing to left mouse button activate raycast system and get a point in world and move object towards that point
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //move player towards hit position
                    playerMotor.MoveToPoint(hit.point);

                    //stop focusing any object
                    RemoveFocus();
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) //when right mouse button click move towards point if it is an interactable item and interact it eventually
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //focus player to object if it is interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (hit.collider.GetComponent<Enemy>() != null) //if hit collider is enemy than focus to it
                        SetFocus(interactable, true);
                    else
                        SetFocus(interactable, false);
                }
            }
        }

        if(focus != null && focus.gameObject.GetComponent<Enemy>() != null)
        {
            if(EquipmentManager.instance.currentWeapon != null 
                && Vector3.Distance(focus.gameObject.transform.position, transform.position) <= EquipmentManager.instance.currentWeapon.range)
            {
                playerMotor.StopMovement();
            }
        }
    }

    void SetFocus(Interactable newFocus, bool isEnemy) //focusing to an interactable
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDeFocused();
            focus = newFocus;
            playerMotor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform, isEnemy);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDeFocused();
        focus = null;
        playerMotor.StopFollowing();
    }

    private IEnumerator WaitForDialog(GameObject dialogBox) //waiting until dialog box is closed by waiting for any key input (any key press will close dialog box as well)
    {
        while (!Input.anyKeyDown)
        {
            yield return null;
        }
    }
}
