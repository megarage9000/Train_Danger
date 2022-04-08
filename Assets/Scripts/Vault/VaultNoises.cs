using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultNoises : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip vaultOpenNoise;
    public AudioClip vaultUnlockNoise;
    public AudioClip lockFloorHitNoise;
    public void playlockHitNoise()
    {
        Utilities.playSound(audioSource, lockFloorHitNoise);
    }

    public void playUnlockNoise()
    {
        Utilities.playSound(audioSource, vaultUnlockNoise);
    }

    public void playVaultOpen()
    {
        Utilities.playSound(audioSource, vaultOpenNoise);
    }
}
