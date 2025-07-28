using UnityEngine;
using KingFighting.Core;
using System;

namespace KingFighting.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMovement : CharacterComponent, IMovement
    {
        private Action<Vector2> onMoveByVector;

        public float MoveSpeed => moveSpeed;

        private float moveSpeed;
        private float combatMoveSpeed;
        private float rotateSpeed;

        private Vector3 moveDirection;
        private Vector3 lookPos;

        private Rigidbody rb;
        private Collider myCollider;
        private bool isInCombat;
        private bool canMove;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            myCollider = GetComponent<Collider>();
        }

        private void FixedUpdate()
        {
            if (!isActiveComponent)
            {
                return;
            }

            var moveVector = (isInCombat ? combatMoveSpeed : moveSpeed) * Time.fixedDeltaTime * moveDirection;
            var lookVector = isInCombat ?
                (lookPos - transform.position).normalized :
                moveVector.normalized;

            if(lookVector == Vector3.zero)
            {
                lookVector = transform.forward.normalized;
            }

            if(canMove && moveVector != Vector3.zero)
            {
                rb.MovePosition(rb.position + moveVector);
            }

            if(canMove)
            {
                var quaternion = Quaternion.LookRotation(lookVector, Vector3.up);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, quaternion, rotateSpeed * Time.fixedDeltaTime));
            }
        }

        public void Init(float moveSpeed, float combatMoveSpeed, float rotateSpeed)
        {
            this.moveSpeed = moveSpeed;
            this.rotateSpeed = rotateSpeed;
            this.combatMoveSpeed = combatMoveSpeed;

            canMove = true;
            rb.isKinematic = false;
            myCollider.isTrigger = false;

            enabled = true;
        }

        public void UpdateCombatState(bool isInCombat)
        {
            this.isInCombat = isInCombat;
        }

        public void UpdateLookFocusPos(Vector3 lookPos)
        {
            this.lookPos = lookPos;
        }

        public void Move(Vector2 direction)
        {
            moveDirection = new Vector3(direction.x, 0, direction.y);
            onMoveByVector?.Invoke(direction);
        }

        public void StopMove(bool isStop) {
            canMove = !isStop;
        }

        public void AddListenerMoveByVector(Action<Vector2> action)
        {
            onMoveByVector -= action;
            onMoveByVector += action;
        }

        public void Disable()
        {
            rb.isKinematic = true;
            myCollider.isTrigger = true;
        }
    }
}
