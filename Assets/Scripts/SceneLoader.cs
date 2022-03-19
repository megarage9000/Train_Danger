using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public int numScenes = 3;
    static int index = 0;
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag.Equals("Player"))
        {
            index++;
            if(index >= numScenes)
            {
                index = 0;
            }
            SceneManager.LoadScene(index);
        }
    }
}
