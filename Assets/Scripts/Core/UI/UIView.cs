using UnityEngine;

namespace KingFighting.Core
{
    public abstract class UIView : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide() {
            gameObject.SetActive(false);
        }
    }
}
