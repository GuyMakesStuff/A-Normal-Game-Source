using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ANormalGame.Managers
{
    public class Manager<T> : MonoBehaviour
    {
        public bool IsGlobal;
        public static T Instance { get; private set; }
        public static bool IsInstanced
        {
            get { return Instance != null; }
        }

        protected void Init(T OBJ)
        {
            Instance = OBJ;

            if (IsGlobal)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}