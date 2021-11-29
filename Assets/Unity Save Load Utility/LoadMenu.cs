using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{

	public SaveLoadUtility slu;
	public bool showLoad = true;
	private string saveGameName;
	private int selectedSaveGameIndex = -99;
	public List<SaveGame> saveGames;
	private char[] newLine = "\n\r".ToCharArray();
	public Vector2 scrollPosition = Vector2.zero;

	private Regex regularExpression = new Regex("^[a-zA-Z0-9_\"  *\"]*$");
	/*Regular expression, contains only upper and lowercase letters, numbers, and underscores.
 
          * ^ : start of string
         [ : beginning of character group
         a-z : any lowercase letter
         A-Z : any uppercase letter
         0-9 : any digit
         _ : underscore
         ] : end of character group
         * : zero or more of the given characters
         $ : end of string
 
     */


	void Start()
	{
		if (slu == null)
		{
			slu = GetComponent<SaveLoadUtility>();
			if (slu == null)
			{
				Debug.Log("[SaveLoadMenu] Start(): Warning! SaveLoadUtility not assigned!");
			}
		}

		saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);

	}

	void OnGUI()
	{

		if(showLoad == true) {
			scrollPosition = GUI.BeginScrollView(new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 50, 320, 150), scrollPosition, new Rect(0, 0, 220, 500));
			GUILayout.BeginVertical(GUILayout.MinWidth(300)); 
			GUILayout.BeginArea(new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 50, 300, 500));
			foreach (SaveGame saveGame in saveGames) {
				if(GUILayout.Button(saveGame.savegameName + " (" + saveGame.saveDate + ")")) {
					slu.LoadGame(saveGame.savegameName);
					showLoad = false;
					GUI.EndScrollView();
					return;
				}
			}

			//GUILayout.BeginHorizontal();
			//GUILayout.FlexibleSpace();
			//GUILayout.EndHorizontal();

			GUILayout.FlexibleSpace();
			//GUILayout.EndArea();
			//GUILayout.EndVertical();
			//GUI.EndScrollView();

		}
		
	}
}