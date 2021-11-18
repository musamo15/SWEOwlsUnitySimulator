using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
	public GameObject[] characterPrefabs;
	public Transform spawnPoint;
	public TMP_Text label;
	public LevelEditorManager lem;

	void Start()
	{
		int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
		GameObject prefab = characterPrefabs[selectedCharacter];
		if(selectedCharacter == 0)
        {
			lem.ItemPrefabs[1].SetActive(true); // 1 is the index of SpikePrime in Level editor manager
			lem.characterButtons[0].SetActive(true);
        }
		if (selectedCharacter == 1)
		{
			lem.ItemPrefabs[13].SetActive(true); // 13 is the index of SpikePrimeSideColor in level editor manager
			lem.characterButtons[1].SetActive(true);
		}
		//else
		//{
		//	GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
		//}
		label.text = prefab.name; //will remove at the end when it is fully working.
	}
}
