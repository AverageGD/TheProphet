using System.Collections.Generic;
using UnityEngine;

public class GatesController : MonoBehaviour
{
    public static GatesController instance;

    public List <bool> gates = new List <bool> ();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
