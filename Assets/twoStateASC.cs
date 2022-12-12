using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoStateASC : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;

    int VelocityZHash;
    int VelocityXHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaximumVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        if (forwardPressed && velocityZ < currentMaximumVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (leftPressed && velocityX > -currentMaximumVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if(rightPressed && velocityX < currentMaximumVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        } 

        if(!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        if(!forwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        if(!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        if(!leftPressed&&!rightPressed&&velocityX!=0.0f&&(velocityX>-0.05f&&velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }

        if(forwardPressed && runPressed && velocityZ > currentMaximumVelocity)
        {
            velocityZ = currentMaximumVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaximumVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaximumVelocity && velocityZ < (currentMaximumVelocity + 0.05f))
            {
                velocityZ = currentMaximumVelocity;
            }
        }
        else if (forwardPressed&&velocityZ < currentMaximumVelocity && velocityZ >(currentMaximumVelocity - 0.05f))
        {
            velocityZ = currentMaximumVelocity;
        }

        if (leftPressed && runPressed && velocityX < -currentMaximumVelocity)
        {
            velocityX = -currentMaximumVelocity;
        }
        else if (leftPressed && velocityX < -currentMaximumVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX < -currentMaximumVelocity && velocityX > (-currentMaximumVelocity - 0.05f))
            {
                velocityX = -currentMaximumVelocity;
            }
        }
        else if (leftPressed && velocityX > -currentMaximumVelocity && velocityX < (-currentMaximumVelocity + 0.05f))
        {
            velocityX = -currentMaximumVelocity;
        }

        if (rightPressed && runPressed && velocityX > currentMaximumVelocity)
        {
            velocityX = currentMaximumVelocity;
        }
        else if (rightPressed && velocityX > currentMaximumVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX > currentMaximumVelocity && velocityX < (currentMaximumVelocity + 0.05f))
            {
                velocityX = currentMaximumVelocity;
            }
        }
        else if (rightPressed && velocityX < currentMaximumVelocity && velocityX > (currentMaximumVelocity - 0.05f))
        {
            velocityX = currentMaximumVelocity;
        }

        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }
}
