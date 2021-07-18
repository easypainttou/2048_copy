using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFramework;
using UIFramework.Extend;

/// <summary>
/// 
/// </summary>
public class StartPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Resources/Prefabs/UI/Panel/StartPanel";

    /// <summary>
    /// 
    /// </summary>
    public StartPanel() : base(new UIType(path))
    {
        
    }

    protected override void InitEvent()
    {
        PanelTransform.GetOrAddComponentInChildren<Button>("NewGameButton").onClick.AddListener(() =>
        {
            Push(new MainPanel());
        });
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnChange(BasePanel newPanel)
    {
        // StartPanel panel = newPanel as StartPanel;
    }
}
