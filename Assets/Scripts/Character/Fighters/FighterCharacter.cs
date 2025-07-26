using KingFighting.Core;
using UnityEngine;

namespace KingFighting.Character
{
    public class FighterCharacter : CharacterBase
    {
        [SerializeField]
        private string myLayer;

        public override void Spawn(CharacterData characterData)
        {
            gameObject.layer = LayerMask.NameToLayer(myLayer);

            base.Spawn(characterData);
        }

        protected override void InitComponent(CharacterData data)
        {
            base.InitComponent(data);
        }

        protected override void InitEventListener()
        {
            base.InitEventListener();
        }

        protected override void OnDeath()
        {
            base.OnDeath();

            gameObject.layer = LayerMask.NameToLayer(ObjectLayer.DeathLayerName);
        }
    }
}
