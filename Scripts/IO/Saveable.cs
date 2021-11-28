using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ANormalGame.IO
{
    [System.Serializable]
    public class Saveable
    {
        public string FileName;

        public void Save()
        {
            Saver.Serialize(this);
        }
    }
}