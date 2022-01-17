using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 15;
    [Range(0,1)] public float sliding = 0.9f;
    public float jumpForce = 1200;


    // Update is called once per frame
    void FixedUpdate()
    {
      float h = Input.GetAxis("Horizontal");

      Vector2 v = GetComponent<Rigidbody2D>().velocity;

      //ESTE CODIGO MUEVE A BABY  MARIO DE IZQUIERDA A DERECHA, Y LO PARA
      if(h != 0) {
        GetComponent<Rigidbody2D>().velocity = new Vector2(h * moveSpeed, v.y);
        transform.localScale = new Vector2(Mathf.Sign(h), transform.localScale.y);
       } else {
        GetComponent<Rigidbody2D>().velocity = new Vector2(v.x * sliding, v.y);
      }

      GetComponent<Animator>().SetFloat("Speed", h);

      //ESTE CODIGO HACE SALTAR A BABY MARIO
      bool grounded = IsGrounded();

      //SI HA PULSADO, LA TECLA UP
      if(Input.GetKey(KeyCode.UpArrow) && grounded) {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
        GetComponent<Animator>().SetBool("Jumping", !grounded);
      }


    }


    bool IsGrounded() {
        

        
        Bounds bounds = GetComponent<Collider2D>().bounds;
        float range = bounds.size.y * 0.1f;
        
        
        Vector2 v = new Vector2(bounds.center.x,
                                bounds.min.y - range);

        
        RaycastHit2D hit = Physics2D.Linecast(v, bounds.center);
        
        
        return (hit.collider.gameObject != gameObject);
    }
}


