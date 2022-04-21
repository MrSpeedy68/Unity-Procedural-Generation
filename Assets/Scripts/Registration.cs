using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    private string url = "https://elviveamten.000webhostapp.com/Register.php";
    
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public Button registerButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW(url, form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("user created successfully");
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User creation failed. Error #" + www.text);
        }
    }

    public void VerifyInputs()
    {
        registerButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
}
