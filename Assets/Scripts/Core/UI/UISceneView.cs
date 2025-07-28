using System.Collections.Generic;
using UnityEngine;

namespace KingFighting.Core
{
    public abstract class UISceneView : MonoBehaviour
    {
        protected List<UIView> views;

        private void Awake()
        {
            views = new List<UIView>(gameObject.GetComponentsInChildren<UIView>(true));
        }

        public virtual T ShowView<T>() where T : UIView
        {
            foreach (var view in views)
            {
                if (view is T targetView)
                {
                    view.Show();
                    return targetView;
                }

                view.Hide();
            }

            return null;
        }

        public virtual bool GetView<T>(out T targetView) where T : UIView
        {
            targetView = null;
            var index = views.FindIndex(view => view is T);
            if (index >= 0)
            {
                targetView = views[index] as T;
                return true;
            }

            return false;
        }

        public virtual void HideAllViews()
        {
            foreach (var view in views)
            {
                view.Hide();
            }
        }
    }
}
