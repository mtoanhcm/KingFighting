using UnityEngine;
using KingFighting.Core;
using System;

namespace KingFighting.GameMode
{
    public class UIGameView : UISceneView
    {
        private void Start()
        {
            ShowView<ChooseModeView>();
        }
    }
}
