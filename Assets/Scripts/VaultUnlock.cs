using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultUnlock : MonoBehaviour
{
    public GameObject staticVault;
    public GameObject animationVault;
    public Animator vaultUnlock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Unlock()
    {
        Destroy(staticVault);
        animationVault.SetActive(true);
        vaultUnlock.SetBool("isUnlocked", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
