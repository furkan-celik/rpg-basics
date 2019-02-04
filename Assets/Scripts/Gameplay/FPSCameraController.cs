using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FPSCameraController : MonoBehaviour {
    
    public Vector3 offset;

    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = PlayerManager.instance.player.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        //Move forward
        MovePlayerForward(Input.GetAxis("Vertical"));

        //Move left&right
        MoveLeftRight(Input.GetAxis("Horizontal"));
	}

    private void LateUpdate()
    {
        gameObject.transform.LookAt(Input.mousePosition);
        transform.position = PlayerManager.instance.player.transform.position - offset;
        transform.rotation = PlayerManager.instance.direction;
    }

    void MovePlayerForward(float input)
    {
        if(input >= 0)
        {
            agent.velocity = PlayerManager.instance.direction.eulerAngles * input;
        }
        else
        {
            
        }
    }

    void MoveLeftRight(float input)
    {

    }
}
