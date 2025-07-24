using UnityEngine;

namespace KingFighting.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMovement : MonoBehaviour
    {
        private float moveSpeed;
        private float rotateSpeed;

        private Vector3 moveDirection;
        private Vector3 lookPos;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var moveVector = moveSpeed * Time.fixedDeltaTime * moveDirection;
            var lookVector = lookPos != Vector3.zero ?
                (lookPos - transform.position).normalized :
                moveVector.normalized;

            if(moveVector != Vector3.zero)
            {
                rb.MovePosition(rb.position + moveVector);
            }

            if(lookVector != Vector3.zero)
            {
                var quaternion = Quaternion.LookRotation(lookVector, Vector3.up);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, quaternion, rotateSpeed * Time.fixedDeltaTime));
            }
        }

        public void Init(float moveSpeed, float rotateSpeed)
        {
            this.moveSpeed = moveSpeed;
            this.rotateSpeed = rotateSpeed;
        }

        public void UpdateMoveDirection(Vector3 direction, Vector3 lookPos)
        {
            moveDirection = direction;
            this.lookPos = lookPos;
        }
    }
}
