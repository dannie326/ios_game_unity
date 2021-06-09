using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Collider2D collider = null;
    Rigidbody2D rigidbody = null;
    [SerializeField]
    float speed = 0;
    bool canMove = true;

    bool isOutframe { get { return Mathf.Abs(transform.position.x) > 12; } }

    void Awake()
    {
        collider = this.GetComponent<BoxCollider2D>();
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOutframe) Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        //Debug.LogWarning(transform.position.x);
    }
    public void Fly(int d)
    {
        collider.enabled = true;
        if (canMove)
        {
            rigidbody.velocity = new Vector2(speed * d, 0.0f);
        }
    }
    public void Pause()
    {
        collider.enabled = false;
        rigidbody.velocity = Vector2.zero;
    }

}
