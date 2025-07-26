using KingFighting.Core;
using UnityEngine;
using KingFighting.AI;

namespace KingFighting.Character
{
    public class FighterCharacter : CharacterBase
    {
        [SerializeField]
        private string myLayer;

        private AINoobBrain noobBrain;

        public override void Spawn(CharacterData characterData)
        {
            gameObject.layer = LayerMask.NameToLayer(myLayer);

            base.Spawn(characterData);
        }

        protected override void InitComponent(CharacterData data)
        {
            base.InitComponent(data);

            InitAIBrain();
        }

        protected override void InitEventListener()
        {
            base.InitEventListener();

            sensorComp.AddListenerEnemyDetect(noobBrain.SetTarget);
        }

        protected override void OnDeath()
        {
            base.OnDeath();

            gameObject.layer = LayerMask.NameToLayer(ObjectLayer.DeathLayerName);
        }

        private void InitAIBrain() {
            if (noobBrain == null || !TryGetComponent(out noobBrain))
            {
                noobBrain = gameObject.AddComponent<AINoobBrain>();
            }

            noobBrain.Init(movementComp, combatComp, healthComp, null);
        }
    }
}
