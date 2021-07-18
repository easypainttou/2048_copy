using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFramework;
using UIFramework.Extend;

/// <summary>
/// 
/// </summary>
public class MainPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/MainPanel";

    /// <summary>
    /// 
    /// </summary>
    public MainPanel() : base(new UIType(path))
    {
        
    }

    protected override void InitEvent()
    {
        PanelTransform.GetOrAddComponentInChildren<Button>("QuitButton").onClick.AddListener(() =>
        {
            Pop();
        });
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnChange(BasePanel newPanel)
    {
        // MainPanel panel = newPanel as MainPanel;
    }
}
