using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRepeat : MonoBehaviour
{
    private static Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = player.position - transform.position;
        if (Mathf.Abs(distance.x) > 80)
        {
            transform.position += Mathf.Sign(distance.x) * new Vector3(160,0,0);
        }

        if (Mathf.Abs(distance.y) > 80)
        {
            transform.position += Mathf.Sign(distance.y) * new Vector3(0, 160, 0);
        }
    }
}
