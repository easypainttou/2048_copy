using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIFramework;

public class StartScene : SceneState
{
    /// <summary>
    /// 
    /// </summary>
    public StartScene()
    {
        sceneName = "Start";
        sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
        async = false;
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <param name="__loadPanel">是否显示加载面板</param>
    public StartScene(bool __loadPanel)
    {
        sceneName = "Start";
        sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
        async = true;
        //loadPanel = __loadPanel;
    }

    protected override void LoadScene()
    {
        #region 不允许重新加载场景
        /*
        if (sceneName == SceneManager.GetActiveScene().name)
        {
            panelManager.Push(new StartPanel());
            Game.Initialize(panelManager);
            return;
        }
        */
        #endregion
        base.LoadScene();
    }

    protected override void LoadSceneAsync()
    {
        #region 不允许重新加载场景
        /*
        if (sceneName == SceneManager.GetActiveScene().name)
        {
            panelManager.Push(new StartPanel());
            Game.Initialize(panelManager);
            return;
        }
        */
        #endregion
        base.LoadSceneAsync();
    }

    protected override void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        panelManager.Push(new StartPanel());
        base.SceneLoaded(scene, mode);
    }
}
