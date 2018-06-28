using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreation : MonoBehaviour 
{
    public GameObject[] characterPrefabs;
    public UIInput nameInput;
    private GameObject[] characterGameObjects;
    private int characterCounts;
    private int SelectedIndex = 0;

    void Start()
    {
        characterCounts = characterPrefabs.Length;
        characterGameObjects = new GameObject[characterCounts];
        for (int i = 0; i < characterCounts; i++)
        {
            characterGameObjects[i] = GameObject.Instantiate(characterPrefabs[i], transform.position, transform.rotation) as GameObject;
        }

        UpdateShowCharacter();
    }

    void UpdateShowCharacter()
    {
        characterGameObjects[SelectedIndex].SetActive(true);
        for (int i = 0; i < characterCounts; i++)
        {
            if (i != SelectedIndex)
            {
                characterGameObjects[i].SetActive(false);
            }
        }
    }

    public void OnNextClick()
    {
        SelectedIndex++;
        SelectedIndex %= characterCounts;
        UpdateShowCharacter();
    }

    public void OnPreClick()
    {
        SelectedIndex--;
        if (SelectedIndex < 0)
        {
            SelectedIndex = characterCounts - 1;
        }
        UpdateShowCharacter();    
    }

    public void OnOkClick()
    {
        PlayerPrefs.SetInt("SelectedCharacterIndex", SelectedIndex);
        PlayerPrefs.SetString("Name", nameInput.value);
    }
}
