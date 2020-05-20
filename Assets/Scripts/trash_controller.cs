using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash_controller : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //x > 1 is right side of the screen, <1 is left
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1)
            Destroy(this.gameObject);
    }
}
