using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    float initialPos;

    void Start()
    {
        initialPos = transform.position.x;
    }

    void Update()
    {
        transform.Translate(2.5f * Time.deltaTime, 0, 0);
        Tuner();
    }
    void Tuner()
    {
        if (transform.position.x > initialPos + 10)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        if (transform.position.x < initialPos - 10)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
