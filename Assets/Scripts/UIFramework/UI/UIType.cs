using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;

namespace UIFramework
{
    public class UIType : MonoBehaviour
    {
        private string name;
        private string path;
        private bool isInit;

        public string Name { get => name; }
        public string Path { get => path; }
        public bool IsInit { get => isInit; set => isInit = value; }

        public UIType(string _path)
        {
            isInit = false;
            path = _path;
            name = path.Substring(path.LastIndexOf('/') + 1);
        }


    }
}

