using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void OnExiButton()
    {
        Debug.Log("123123123");
        
        // Check if UnityMessageManager exists (meaning we are likely inside React Native)
#if UNITY_ANDROID || UNITY_IOS
        try 
        {
            // Try to send a message to React Native
            if (UnityMessageManager.Instance != null) {
                UnityMessageManager.Instance.SendMessageToRN("QUIT_GAME");
                return;
            }
        } 
        catch (System.Exception e) 
        {
            Debug.Log("Not running in React Native or UnityMessageManager missing: " + e.Message);
        }
#endif
        // Fallback for standalone app or Unity Editor
        Application.Quit();
    } 
}
