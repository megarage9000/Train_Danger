using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPanel : MonoBehaviour
{

    [SerializeField]
    public LightBulb[] lightBulbs;

    public int[] lightPattern = { 4, 3, 2, 1, 6, 5 };

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowPattern());   
    }

    IEnumerator ShowPattern()
    {
        int numBulbs = lightBulbs.Length;
        for(int i = 0; i < numBulbs; i++)
        {
            LightBulb bulb = lightBulbs[lightPattern[i] - 1];
            bulb.TurnOn();
            yield return new WaitForSeconds(1f);
            bulb.TurnOff();
        }
    }

}
