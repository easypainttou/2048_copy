using System;
using UIFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIFramework
{
    public abstract class SceneState
    {
        protected PanelManager panelManager;
        protected string sceneName = "";
        protected int sceneIndex = -1;
        protected bool async = false;
        //protected bool showLoadPanel = false;
        

        public SceneState()
        {
            panelManager = new PanelManager();
        }


        public virtual void OnEnter()
        {
            if (!async)
            {
                LoadScene();
            }
            else
            {
                LoadSceneAsync();
            }
        }

        public virtual void OnExit()
        {
            panelManager.PopAll();
        }

        protected virtual void LoadScene()
        {
            if (sceneIndex < 0)
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                SceneManager.LoadScene(sceneIndex);
            }
            SceneManager.sceneLoaded += SceneLoaded;
        }

        protected virtual void LoadSceneAsync()
        {

        }

        protected virtual void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GameRoot.Instance.Init(panelManager);
            SceneManager.sceneLoaded -= SceneLoaded;
            Debug.Log($"{sceneName}加载完毕");
        }


    }
}


