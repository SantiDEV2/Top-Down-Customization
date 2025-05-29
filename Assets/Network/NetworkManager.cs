using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System;
using System.IO;

[Serializable]
public class Login
{
    public string status;
    public int id;
    public string name;
}

[Serializable]
public class GetProducts
{
}


public class NetworkManager : MonoBehaviour
{
    private string baseUrl = "http://localhost:8080/";

    public TMP_Text txtLogger;
    public TMP_InputField txtUser, txtPass;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer.color = Color.gray;
    }

    public void tryLogin()
    {

        string username = txtUser.text;
        string password = txtPass.text;


        if (username != "" && password != "")
            StartCoroutine(testPost(username, password));
        else
            spriteRenderer.color = Color.red;
    }

    public void getProducts()
    {
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
            Debug.Log(www.downloadHandler.text);
            txtLogger.text = "server: " + www.downloadHandler.text;
            string json = www.downloadHandler.text;

            Login myLogin = JsonUtility.FromJson<Login>(json);
            if (myLogin.status != "valid") yield return null ;
            PlayerPrefs.SetInt("idUser", myLogin.id);
        }
    }
}
