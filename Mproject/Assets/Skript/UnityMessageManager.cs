using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UnityMessageManager : MonoBehaviour
{
    private static UnityMessageManager _instance;
    public static UnityMessageManager Instance 
    { 
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UnityMessageManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("UnityMessageManager");
                    _instance = go.AddComponent<UnityMessageManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
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
