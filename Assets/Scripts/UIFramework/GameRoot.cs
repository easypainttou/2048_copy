using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;

namespace UIFramework
{

    public class GameRoot : MonoBehaviour
    {

        private static GameRoot instance;

        public static GameRoot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameRoot>();
                }
                return instance;
            }
        }

        private PanelManager panelManager;

        public PanelManager PanelManager { get => panelManager; }

        private SceneSystem sceneSystem;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            sceneSystem = new SceneSystem();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoadScene(new StartScene());
        }



        public void LoadScene(SceneState sceneState)
        {
            sceneSystem?.SetScene(sceneState);
        }

        public void Init(PanelManager _panelManager)
        {
            panelManager = _panelManager;
        }


    }
}