using UnityEngine;
using TMPro;

namespace TopDownShooter
{
    public class DebugUI : Singleton<DebugUI>
    {
        public string LookVector;
        public string MoveVector;
        public string AnimVector;

        public TMP_Text Text;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            LookVector = Vector2.zero.ToString();
            MoveVector = Vector2.zero.ToString();
            AnimVector = Vector2.zero.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            Text.text = FormText();
        }

        string FormText()
        {
            return $"Look: {LookVector.ToString()}\nMove: {MoveVector.ToString()}\nAnim:{AnimVector.ToString()}";
        }
    }
}