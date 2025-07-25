using UnityEngine;

namespace KingFighting.Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator animator;

        private int moveXFactorHash;
        private int moveYFactorHash;
        private int normalMoveFactorHash;
        private int isInCombatStateHash;
        private int attackHash;

        private bool isInCombat;

        private void Awake()
        {
            animator = gameObject.GetComponentInChildren<Animator>();

            moveXFactorHash = Animator.StringToHash("MoveX");
            moveYFactorHash = Animator.StringToHash("MoveY");
            normalMoveFactorHash = Animator.StringToHash("NormalMove");
            isInCombatStateHash = Animator.StringToHash("IsCombat");
            attackHash = Animator.StringToHash("Attack");
        }

        public void UpdateMoveFactoByInputDirection(Vector2 moveInputDirect)
        {
            if (isInCombat)
            {
                animator.SetFloat(moveXFactorHash, moveInputDirect.x);
                animator.SetFloat(moveYFactorHash, moveInputDirect.y);
            } else
            {
                animator.SetFloat(normalMoveFactorHash, moveInputDirect.magnitude);
            }
        }

        public void UpdateCombatState(bool isInCombat)
        {
            this.isInCombat = isInCombat;
            animator.SetBool(isInCombatStateHash, isInCombat);
        }

        public void TriggerAttack()
        {
            animator.SetTrigger(attackHash);
        }
    }
}
