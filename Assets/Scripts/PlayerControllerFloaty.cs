using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace floaty
{
    public class PlayerControllerFloaty : MonoBehaviour
    {
        [SerializeField] Transform groundNormalDirection;
        [SerializeField] float acceleration, speed, maxSpeed, jumpForce;
        [SerializeField] float rideSpringStrenght, RideSpringDamper, RideHeigh, rayLenght, groundedRayDistance;
        Rigidbody playerRigidBody;
        [SerializeField] Transform orientation;
        [SerializeField] LayerMask ground;

        float horizontalInput, verticalInput;

        bool grounded, pressedJump, disabledFloat, coyoteTime, canJump = true;

        RaycastHit hit;
        void Start()
        {
            playerRigidBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pressedJump = true;
                CancelInvoke("DiscardJump");
                Invoke(nameof(DiscardJump), 0.25f);
            }
        }

        void DiscardJump()
        {
            pressedJump = false;
        }

        void FixedUpdate()
        {
            Jump();
            Float();
            CalculateMovementAndApply();
            LimitVelocity();
            StopMovement();
        }

        void Float()
        {
            if (Physics.Raycast(playerRigidBody.transform.position, Vector3.down, out hit, rayLenght, ground) && !disabledFloat)
            {
                Vector3 vel = playerRigidBody.velocity;
                Vector3 rayDir = transform.TransformDirection(Vector3.down);
                Vector3 otherVel = Vector3.zero;

                float rayDirVel = Vector3.Dot(rayDir, vel);
                float otherDirVel = Vector3.Dot(rayDir, otherVel);

                float relVel = rayDirVel - otherDirVel;
                float x = hit.distance - RideHeigh;

                float springForce = (x * rideSpringStrenght) - (relVel * RideSpringDamper);

                playerRigidBody.AddForce(rayDir * springForce);
            }
            else
            {
                hit.normal = Vector3.up;
            }

            if (hit.distance < groundedRayDistance && hit.distance > 0.1f)
            {
                grounded = true;
            }
            else if (Vector3.Angle(hit.normal, Vector3.up) > 16 && hit.distance < groundedRayDistance * 1.1f && playerRigidBody.velocity.y < 0 && hit.distance > 0.1f)
            {
                grounded = true;
            }
            else if (grounded)
            {
                grounded = false;
                coyoteTime = true;
                Invoke(nameof(resetCoyoteTime), 0.15f);
            }

            Debug.DrawRay(playerRigidBody.transform.position, Vector3.down * rayLenght, Color.red);

        }

        public void resetCoyoteTime()
        {
            coyoteTime = false;
        }

        void CalculateMovementAndApply()
        {
            Vector3 groundNormalFoward = Vector3.Cross(orientation.transform.right, hit.normal);
            groundNormalDirection.transform.forward = groundNormalFoward;

            Vector3 directionMove = groundNormalDirection.transform.forward * verticalInput + groundNormalDirection.transform.right * horizontalInput;

            playerRigidBody.AddForce(directionMove.normalized * acceleration);
            Debug.DrawRay(playerRigidBody.transform.position, directionMove * rayLenght, Color.red);
        }
        void LimitVelocity()
        {
            Vector3 velocityHolder = playerRigidBody.velocity;
            velocityHolder.y = 0;

            if (velocityHolder.magnitude > maxSpeed)
            {
                velocityHolder = velocityHolder.normalized * maxSpeed;
            }

            velocityHolder.y = playerRigidBody.velocity.y;
            playerRigidBody.velocity = velocityHolder;
        }

        void StopMovement()
        {
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x * 0.85f, playerRigidBody.velocity.y, playerRigidBody.velocity.z * 0.85f);

        }

        void Jump()
        {
            if ((grounded || coyoteTime) && pressedJump && canJump)
            {
                playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, 0, playerRigidBody.velocity.z);
                playerRigidBody.AddForce(Vector3.up * jumpForce);
                pressedJump = false;
                disabledFloat = true;
                canJump = false;
                coyoteTime = false;
                Invoke(nameof(floatAgain), 0.3f);
                Invoke(nameof(CanJumpAgain), 0.25f);
            }
        }

        void CanJumpAgain()
        {
            canJump = true;
        }

        void floatAgain()
        {
            disabledFloat = false;
        }
    }
}