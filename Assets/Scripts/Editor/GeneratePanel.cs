using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

#if UNITY_EDITOR
public class GeneratePanel : Editor
{
    [MenuItem("Assets/Create/CreatePanelCS", false, 1)]
    private static void CreatePanelCS()
    {
        var objs = Selection.gameObjects;
        var selected = Selection.assetGUIDs;
        for (int i = 0; i < objs.Length; i++)
        {
            GameObject obj = objs[i];
            string name = obj.name;
            string filePath = $"Assets/Scripts/UI/Panel/{name}.cs";
            string objPath = AssetDatabase.GUIDToAssetPath(selected[i]);

            if (objPath.Contains(".prefab"))
                objPath = objPath.Replace(".prefab", "");
            if (objPath.Contains("Assets/"))
                objPath = objPath.Replace("Assets/", "");

            // Debug.Log($"filePath = {filePath}, objPath = {objPath}");
            if (!File.Exists(filePath))
            {
                string content =
                #region cs code
$@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFramework;
using UIFramework.Extend;

/// <summary>
/// 
/// </summary>
public class {name} : BasePanel
{"{"}
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = ""{objPath}"";

    /// <summary>
    /// 
    /// </summary>
    public {name}() : base(new UIType(path))
    {"{"}
        
    {"}"}

    protected override void InitEvent()
    {"{"}
        // ActivePanel.GetOrAddComponentInChildren<Button>(""BtnExit"").onClick.AddListener(() =>
        // {"{"}
        //            Pop();
        // {"}"});
    {"}"}

    public override void OnStart()
    {"{"}
        base.OnStart();
    {"}"}

    public override void OnChange(BasePanel newPanel)
    {"{"}
        // {name} panel = newPanel as {name};
    {"}"}
{"}"}
";
                #endregion
                File.WriteAllText(filePath, content);
                Debug.Log($"生成完毕\n路径为{filePath}");
                AssetDatabase.Refresh();
            }
            else
                Debug.Log("已存在该文件");
        }
    }

    [MenuItem("Assets/Create/GenerateSceneCS", false, 2)]
    private static void GenerateSceneCS()
    {
        var objs = Selection.gameObjects;
        var selected = Selection.assetGUIDs;
        for (int i = 0; i < selected.Length; i++)
        {
            string objPath = AssetDatabase.GUIDToAssetPath(selected[i]);

            if (!objPath.EndsWith(".unity"))
                continue;

            objPath = objPath.Replace(".unity", "");
            int index = objPath.LastIndexOf('/');
            string name = objPath;
            if (index > 0)
                name = objPath.Substring(index + 1);

            string filePath = $"Assets/Scripts/Scene/{name}Scene.cs";

            if (!File.Exists(filePath))
            {
                string content =
                #region cs code
$@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIFramework;

public class {name}Scene : SceneState
{"{"}
    /// <summary>
    /// 
    /// </summary>
    public {name}Scene()
    {"{"}
        sceneName = ""{name}"";
        sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
        async = false;
    {"}"}

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <param name=""__loadPanel"">是否显示加载面板</param>
    public {name}Scene(bool __loadPanel)
    {"{"}
        sceneName = ""{name}"";
        sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
        async = true;
        loadPanel = __loadPanel;
    {"}"}

    protected override void LoadScene()
    {"{"}
        #region 不允许重新加载场景
        /*
        if (sceneName == SceneManager.GetActiveScene().name)
        {"{"}
            panelManager.Push(new {name}Panel());
            Game.Initialize(panelManager);
            return;
        {"}"}
        */
        #endregion
        base.LoadScene();
    {"}"}

    protected override void LoadSceneAsync()
    {"{"}
        #region 不允许重新加载场景
        /*
        if (sceneName == SceneManager.GetActiveScene().name)
        {"{"}
            panelManager.Push(new {name}Panel());
            Game.Initialize(panelManager);
            return;
        {"}"}
        */
        #endregion
        base.LoadSceneAsync();
    {"}"}

    protected override void SceneLoaded(Scene scene, LoadSceneMode mode)
    {"{"}
        // panelManager.Push(new {name}Panel());
        base.SceneLoaded(scene, mode);
    {"}"}
{"}"}
";
                #endregion
                File.WriteAllText(filePath, content);
                Debug.Log($"生成完毕\n路径为{filePath}");
                AssetDatabase.Refresh();
            }
            else
                Debug.Log("已存在该文件");
        }
    }
}
#endif
