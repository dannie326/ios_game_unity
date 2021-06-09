using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public System.Action gameEnd = null;


    Collider2D collider = null;
    Rigidbody2D rigidbody = null;
    bool canMove = false;
    [SerializeField]
    float moveSpeed = 0;
    [SerializeField]
    float jumpForce = 0;
    bool moveR, moveL = false;

    void Awake()
    {
        collider = this.GetComponent<BoxCollider2D>();
        rigidbody = this.GetComponent<Rigidbody2D>();
        moveSpeed = 8;
        jumpForce = 500;
        canMove = true;
        moveR = false;
        moveL = false;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        if(canMove)
        {
            if (moveR && this.transform.position.x < 9.0f)
                this.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
            else if (moveL && this.transform.position.x > -9.0f)
                this.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            //Destroy(collider.gameObject);
            gameEnd.Invoke();
        }
    }
    #region UIEvent
    public void OnPress_R()
    {
        moveR = true;
    }
    public void OnRelease_R()
    {
        moveR = false;
    }
    public void OnPress_L()
    {
        moveL = true;
    }
    public void OnRelease_L()
    {
        moveL = false;
    }
    public void OnClick_J()
    {
        if((this.transform.position.y<4 && rigidbody.velocity.y<0.5f))
            rigidbody.AddForce(new Vector2(0.0f, jumpForce));
    }
    #endregion
}
