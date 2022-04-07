using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultUnlock : MonoBehaviour
{
    public GameObject staticVault;
    public GameObject animationVault;
    public Animator vaultUnlock;

    public void Unlock()
    {
        Destroy(staticVault);
        animationVault.SetActive(true);
        vaultUnlock.SetBool("isUnlocked", true);
    }
}
