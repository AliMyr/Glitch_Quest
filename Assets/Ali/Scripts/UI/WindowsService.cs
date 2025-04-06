using System;
using System.Collections.Generic;
using UnityEngine;

public class WindowsService : MonoBehaviour
{
    [SerializeField] private Window[] windows;
    private Dictionary<Type, Window> windowsDict;

    public void Initialize()
    {
        windowsDict = new Dictionary<Type, Window>();
        foreach (var window in windows)
        {
            windowsDict[window.GetType()] = window;
            window.Hide(true);
            window.Initialize();
        }
        ShowWindow<MainMenuWindow>(true);
    }

    public T GetWindow<T>() where T : Window => windowsDict[typeof(T)] as T;

    public void ShowWindow<T>(bool immediate) where T : Window =>
        GetWindow<T>()?.Show(immediate);

    public void HideWindow<T>(bool immediate) where T : Window =>
        GetWindow<T>()?.Hide(immediate);

    public void ShowWindow(Type windowType, bool immediate)
    {
        if (windowsDict.TryGetValue(windowType, out var window))
            window.Show(immediate);
        else
            Debug.LogError("Window of type " + windowType + " not found.");
    }
}
