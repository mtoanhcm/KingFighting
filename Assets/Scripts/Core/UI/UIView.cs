using UnityEngine;

namespace KingFighting.Core
{
    public abstract class UIView : MonoBehaviour
    {
        protected bool isInit;

        private void Awake()
        {
            Init();
        }

        public virtual void Show()
        {
            if (!isInit) { 
                Init();
            }

            gameObject.SetActive(true);
        }

        public virtual void Hide() {
            gameObject.SetActive(false);
        }

        protected abstract void Init();
    }
}
