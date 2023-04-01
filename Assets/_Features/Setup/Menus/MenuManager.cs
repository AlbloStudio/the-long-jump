using UnityEngine;
using UnityEngine.Rendering;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private Volume _blurVolume;

    public MenuHandler CurrentMenuHandler;
    public Volume BlurVolume => _blurVolume;
}
