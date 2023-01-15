using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;

    void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            nameInput.text = PlayerPrefs.GetString("username");
        }
        else
        {
            nameInput.text = "Player " + Random.Range(0, 1000).ToString("0000");
            onNameInputValueChanged();
        }
    }

    public void onNameInputValueChanged()
    {
        PhotonNetwork.NickName = nameInput.text;
        PlayerPrefs.SetString("username", nameInput.text);
    }
}
