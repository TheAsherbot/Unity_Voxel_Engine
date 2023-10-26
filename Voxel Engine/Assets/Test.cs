using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private Bit bit1;
    private Bit bit2;

    private void Start()
    {
        bit1 = true;
        bit2 = true;
        Debug.Log(bit1 != true);
    }
}
