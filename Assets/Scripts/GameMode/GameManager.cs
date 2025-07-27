using KingFighting.Core;
using UnityEngine;

namespace KingFighting.GameMode
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField]
        private GameMode gameMode;
        [SerializeField]
        private UISceneView sceneView;

        private void Awake()
        {
            if (Instance == null) { 
                Instance = this;
            }

            gameMode.AddListenerGameOver(ActiveGameOverEvent);
        }

        private void Start()
        {
            
        }

        private void ActiveGameOverEvent()
        {

        }
    }
}
