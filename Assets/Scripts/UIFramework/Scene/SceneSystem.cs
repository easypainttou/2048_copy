using System;
using UIFramework;

namespace UIFramework
{
    public class SceneSystem
    {
        private SceneState sceneState;

        public void SetScene(SceneState _sceneState)
        {
            sceneState?.OnExit();
            sceneState = _sceneState;
            sceneState?.OnEnter();
        }
    }
    
}

