using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    private string baseUrl = "http://localhost:8080/";

    public TMP_Text txtLogger;
    public TMP_InputField txtUser, txtPass;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer.color = Color.gray;
    }

    public void tryLogin ()
    {
        string username = txtUser.text;
        string password = txtPass.text;

        if (username != "" && password != "")
            StartCoroutine(testPost(username, password));
        else
            spriteRenderer.color = Color.red;
    }

    IEnumerator testPost(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        //form.AddBinaryData("fileUpload", bytes, "screenShot.png", "image/png");
        var www = UnityWebRequest.Post(baseUrl + "login", form);
        spriteRenderer.color = Color.cyan;


        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("Send to the server");
            Debug.Log(www.downloadHandler.text);
            txtLogger.text = "server: " + www.downloadHandler.text;
            //Debug.Log(www.result);
        }
    }
}
