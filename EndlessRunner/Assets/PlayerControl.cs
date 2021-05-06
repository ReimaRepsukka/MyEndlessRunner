using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    PlayerInputs pia;

    float targetX = 1;
    float moveDir;
    bool sideMoving = false;
    bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        pia = new PlayerInputs();
        pia.Enable();

        pia.Player.Move.performed += (ctx) => Move( ctx.ReadValue<Vector2>().x );
        pia.Player.Jump.performed += (ctx) => Jump();
    }

    void Move( float sideMoveDirection )
    {
        if (sideMoving)
            return;

        sideMoving = true;

        moveDir = sideMoveDirection;

        targetX += moveDir;
        targetX = Mathf.Clamp(targetX, 0, 2);

        StartCoroutine(MoveHandle());
    }

    void Jump()
    {
        if(!jumping)
        {
            jumping = true;
            StartCoroutine(JumpHandle());
        }
    }

    IEnumerator MoveHandle()
    {
        while( (moveDir<0 && transform.position.x > targetX)|| 
            ( moveDir>0 && transform.position.x < targetX ))
        {
            transform.Translate(Vector3.right * moveDir * Time.deltaTime * 3);

            yield return null;
        }

        sideMoving = false;
    }


    IEnumerator JumpHandle()
    {
        float startY = transform.position.y;
        float currentY = startY;
        float jumpSpeed = 2;
        Vector3 pos;
     
        GetComponent<Animator>().SetBool("jump", true);


        do
        {
            pos = transform.position;
            pos.y += Time.deltaTime * jumpSpeed;

            pos.y = Mathf.Clamp(pos.y, startY,  startY+1);

            transform.position = pos;


            if(transform.position.y >= startY+1 && jumpSpeed > 0)
            {
                jumpSpeed *= -1;
            }

            yield return null;

        } while (pos.y > startY);

        GetComponent<Animator>().SetBool("jump", false);
        jumping = false;

    }

}
