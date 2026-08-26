using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UnityMessageManager : MonoBehaviour
{
    public static UnityMessageManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void RNUnitySendMessage(string message);
#endif

    public void SendMessageToRN(string message)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.azesmwayreactnativeunity.ReactNativeUnityViewManager"))
            {
                jc.CallStatic("sendMessageToMobileApp", message);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
#elif UNITY_IOS && !UNITY_EDITOR
        RNUnitySendMessage(message);
#else
        Debug.Log("SendMessageToRN called with: " + message + " (Ignored in Editor/Standalone)");
#endif
    }
}
