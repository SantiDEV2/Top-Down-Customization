using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System;

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
    public int idProduct;
}

[Serializable]
public class ProductsData
{
    public GetProducts[] ownedProducts;
}


public class NetworkManager : MonoBehaviour
{
    private string baseUrl = "http://localhost:8080/";

    public TMP_InputField txtUser, txtPass;
    public SpriteRenderer spriteRenderer;

    public static int[] ids;
    void Start()
    {
        spriteRenderer.color = Color.gray;
    }

    public void Login()
    {
        string username = txtUser.text;
        string password = txtPass.text;

        if (username != "" && password != "")
            StartCoroutine(LoginRequest(username, password));
        else
            spriteRenderer.color = Color.red;
    }

    public void GetProducts()
    {
        StartCoroutine(GetProductsRequest(PlayerPrefs.GetInt("idUser")));
    }

    IEnumerator LoginRequest(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        //form.AddBinaryData("fileUpload", bytes, "screenShot.png", "image/png");
        var www = UnityWebRequest.Post(baseUrl + "login", form);

        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            string json = www.downloadHandler.text;

            Login myLogin = JsonUtility.FromJson<Login>(json);
            if (myLogin.status != "valid") yield return null ;
            PlayerPrefs.SetInt("idUser", myLogin.id);
            GetProducts();
        }
    }

    IEnumerator GetProductsRequest(int idUser)
    {
        WWWForm form = new WWWForm();
        form.AddField("idUser", idUser);
        var www = UnityWebRequest.Post(baseUrl + "getProducts", form);
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            string json = www.downloadHandler.text;
            ProductsData ProductsData = JsonUtility.FromJson<ProductsData>(json);

            for(int i = 0; i < ProductsData.ownedProducts.Length; i++)
            {
                Debug.Log(ProductsData.ownedProducts[i].idProduct);
            }
        }
    }
}
