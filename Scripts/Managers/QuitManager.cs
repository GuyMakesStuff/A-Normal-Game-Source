using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ANormalGame.Managers
{
    public class QuitManager : Manager<QuitManager>
    {
        // Start is called before the first frame update
        void Start()
        {
            Init(this);
        }

        public void QuitGame()
        {
            Application.Quit();
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
            #endif
        }
    }
}