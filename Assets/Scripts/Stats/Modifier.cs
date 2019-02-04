using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Modifier {

    [SerializeField]
    private float baseModifier;

    private List<float> affects = new List<float>();

    public float GetValue()
    {
        float totalAffect = 0f;
        affects.ForEach(x => totalAffect += x);
        return (baseModifier * (100 - totalAffect)) / 100;
    }

    public void AddAffect(float amount)
    {
        affects.Add(amount);
    }

    public void RemoveAffect(float amount)
    {
        affects.Remove(amount);
    }
}