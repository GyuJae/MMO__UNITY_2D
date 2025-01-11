using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        Managers.Map.LoadMap(1);
    }
    
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
