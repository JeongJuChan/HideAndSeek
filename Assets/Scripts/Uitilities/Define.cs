using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UserAuth
    {
        None = -1,
        UserName,
    }

    public enum SceneName
    {
        None = -1,
        LoginScene,
        LobbyScene,
        MainScene,
    }

    public enum ColliderType
    {
        BoxCollider,
        CapsuleCollider,
        MeshCollider
    }
}
