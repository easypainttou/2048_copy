using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UIFramework.Extend;

namespace UIFramework
{
    public class PanelManager
    {
        private Dictionary<string, GameObject> uiDict;
        private Dictionary<string, BasePanel> panelDict;
        private Stack<BasePanel> panelStack;
        private string canvasName;
        private GameObject canvasGo;

        public PanelManager()
        {
            uiDict = new Dictionary<string, GameObject>();
            panelDict = new Dictionary<string, BasePanel>();
            panelStack = new Stack<BasePanel>();
            canvasName = "Canvas";
            canvasGo = GameObject.Find(canvasName);
        }

        public PanelManager(string _canvasName)
        {
            uiDict = new Dictionary<string, GameObject>();
            panelDict = new Dictionary<string, BasePanel>();
            panelStack = new Stack<BasePanel>();
            canvasName = _canvasName;
            canvasGo = GameObject.Find(canvasName);
        }

        public GameObject GetUI(UIType ui)
        {
            if (uiDict.ContainsKey(ui.Path))
            {
                ui.IsInit = true;
                return uiDict[ui.Path];
            }
            if (canvasGo == null)
            {
                Debug.LogError("canvas不存在");
                return null;
            }
            GameObject go = GameObject.Instantiate<GameObject>(AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/{ui.Path}.prefab"), canvasGo.transform);
            go.name = ui.Name;
            uiDict.Add(ui.Path, go);

            return go;
        }

        public void DestroyUI(UIType ui, bool isDestroy = false)
        {
            if (uiDict.ContainsKey(ui.Path))
            {
                if (isDestroy)
                {
                    GameObject.Destroy(uiDict[ui.Path]);
                    uiDict.Remove(ui.Path);
                    panelDict.Remove(ui.Path);
                }
                else
                {
                    uiDict[ui.Path].transform.PanelAppearance(false);
                }
            }
        }

        public void Push(BasePanel newPanel)
        {
            if (panelStack.Count > 0)
                panelStack.Peek().OnDisable();

            if (!panelDict.ContainsKey(newPanel.UI.Path))
            {
                panelDict.Add(newPanel.UI.Path, newPanel);
                GameObject obj = GetUI(newPanel.UI);
                newPanel.PanelTransform = obj.transform;
                newPanel.Initializa(this);
            }
            else
            {
                BasePanel p = panelDict[newPanel.UI.Path];
                p.OnChange(newPanel);
                newPanel = p;
            }

            newPanel.OnStart();
            if (panelStack.Count > 0)
            {
                //防止连续推送重复的面板
                if (panelStack.Peek().UI.Path != newPanel.UI.Path)
                    panelStack.Push(newPanel);
            }
            else
                panelStack.Push(newPanel);
        }

        /// <summary>
        /// 弹出一个面板
        /// </summary>
        public void Pop()
        {
            if (panelStack.Count > 0)
                panelStack.Pop().OnDestroy();
            if (panelStack.Count > 0)
                panelStack.Peek().OnEnable();
        }

        /// <summary>
        /// 弹出所有面板
        /// </summary>
        public void PopAll()
        {
            var values = new List<BasePanel>(panelDict.Values);
            while (values.Count > 0)
            {
                values[0].OnDestroy(true);
                values.RemoveAt(0);
            }
        }

    }
}


