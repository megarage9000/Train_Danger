using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzlerInterface : MonoBehaviour
{
    [SerializeField]
    public UnityEvent OnError;
    [SerializeField]
    public UnityEvent OnSuccess;
}
