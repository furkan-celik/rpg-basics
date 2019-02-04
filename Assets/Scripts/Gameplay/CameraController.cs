using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 1f;
    public float maxZoom = 15f;
    public float yawSpeed = 250f;
    public LayerMask layerMask;
    public Camera fps;
    public Camera rpg;

    private float zoom = 10f;
    public float yaw = 180f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; //zooming to player according to scrollwheel
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom); //limiting values of zoom between minZoom and maxZoom

        if (rpg.isActiveAndEnabled)
        {
            if (Input.GetMouseButton(2))
            {
                yaw -= Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime; //making yaw on middle mouse button click
            }

            foreach (var obs in Physics.RaycastAll(transform.position, target.position)) //getting hit according to layermask and in 100 distance and than if there is anything, making the object transparent.
            {
                if (obs.transform.tag != target.tag)
                {
                    if(obs.transform.GetComponent<MeshRenderer>() != null)
                    {
                        obs.transform.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                    }
                }
            }
        }

        if (zoom <= 1f)
        {
            //switch to fps camera
            rpg.enabled = false;
            fps.enabled = true;

            fps.gameObject.GetComponent<FPSCameraController>().enabled = true;
        }
        else
        {
            //switch back to rpg camera
            fps.enabled = false;
            rpg.enabled = true;

            fps.gameObject.GetComponent<FPSCameraController>().enabled = false;
        }
    }

    private void LateUpdate()
    {
        //Following player
        transform.position = target.position - offset * zoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, yaw);
    }
}
