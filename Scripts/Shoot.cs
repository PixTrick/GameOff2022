using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (IShootable iShootable in GetComponents<IShootable>())
        {
            iShootable.Fire();
        }
    }
}
