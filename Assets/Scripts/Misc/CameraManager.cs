using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    #region singleton

    public static CameraManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public enum Cameras { RPG, FPS }
    public Cameras activeCamera;
    public Camera rpg;
    public Camera fps;
	
	// Update is called once per frame
	void Update () {
        if (rpg.isActiveAndEnabled)
            activeCamera = Cameras.RPG;
        else if (fps.isActiveAndEnabled)
            activeCamera = Cameras.FPS;
	}

    public Camera GetActiveCamera()
    {
        if (activeCamera == Cameras.FPS) return fps;
        else if (activeCamera == Cameras.RPG) return rpg;
        else return rpg;
    }
}
