using UnityEngine;

public static class InputServiceFactory
{
    public static IInputService Create()
    {
        return Application.isMobilePlatform
            ? (IInputService)new MobileInputService()
            : new DesktopInputService();
    }
}
