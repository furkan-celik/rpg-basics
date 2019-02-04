using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBarStater : MonoBehaviour {

    public Scrollbar scrollbar;
    
    private const float baseTime = 0.1f;
    private float tickle;

    public void Initialize(float targetTime)
    {
        tickle = targetTime / baseTime;
        tickle = 1 / tickle;
        Invoke("Run", baseTime);
    }

    private void Run()
    {
        scrollbar.size += tickle;
        if (scrollbar.size != 1)
            Invoke("Run", baseTime);
        else
            Destroy(gameObject);
    }

}
