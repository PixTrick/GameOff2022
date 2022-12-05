using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailPace : MonoBehaviour
{
    [SerializeField] GameObject drool;
    [SerializeField] float droolSpittingDelay = 0.2f;

    private bool canSpit = true;
    private int level = 0;
    public int Level
    { get { return level; } set { level = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpit && level > 0)
        {
            StartCoroutine(DroolSpittingDelay());
        }
    }

    IEnumerator DroolSpittingDelay()
    {
        canSpit = false;
        yield return new WaitForSeconds(droolSpittingDelay);
        Instantiate(drool, transform.position - 2.7f * Vector3.up, Quaternion.identity);
        canSpit = true;
    }
}
