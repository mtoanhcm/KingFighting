using UnityEngine;

namespace KingFighting.Core
{
    public abstract class CharacterComponent : MonoBehaviour
    {
        protected bool isActiveComponent;

        public virtual void SetActive(bool isActive)
        {
            isActiveComponent = isActive;
        }
    }
}
