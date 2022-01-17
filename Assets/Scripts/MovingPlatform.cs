using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float leftBorder, rightBorder;

    [SerializeField]
    private float speed = 1.5f;

    private bool isMovingLeft = false;

    [SerializeField]
    private bool isInvert = false;

    private Vector3 vector;

    // Start is called before the first frame update
    void Start()
    {
        vector = Vector3.up;
        if (isInvert)
            vector *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingLeft)
            transform.Translate(vector * speed * Time.deltaTime);
        else
            transform.Translate(-vector * speed * Time.deltaTime);

        if (transform.position.x <= leftBorder)
            isMovingLeft = false;
        if (transform.position.x >= rightBorder)
            isMovingLeft = true;
    }
}
