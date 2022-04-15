using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    //Used for testing moving damage sources early on
    //Not used anywhere in final game

    private bool dirRight = true;
    public float speed = 2.0f;

    void FixedUpdate()
    {
        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= 10.0f)
        {
            dirRight = false;
        }

        if (transform.position.x <= 4f)
        {
            dirRight = true;
        }

    }
}
