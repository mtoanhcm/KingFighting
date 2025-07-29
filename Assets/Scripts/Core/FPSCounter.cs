using UnityEngine;

namespace KingFighting.Core
{
    public class FPSCounter : MonoBehaviour
    {
        float deltaTime = 0.0f;

        void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }

        void OnGUI()
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(50, 50, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = Color.white;

            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.} fps", fps);
            GUI.Label(rect, text, style);
        }
    }
}
