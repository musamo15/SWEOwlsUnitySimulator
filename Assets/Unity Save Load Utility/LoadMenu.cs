using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{

	public SaveLoadUtility slu;
	public bool showMenu = false;
	public bool showSave = false;
	public bool showLoad = true;
	private string saveGameName;
	private int selectedSaveGameIndex = -99;
	public List<SaveGame> saveGames;
	private char[] newLine = "\n\r".ToCharArray();

	public Vector2 scrollPosition = Vector2.zero;
	public float originalWidth = 1920.0f;
	public float originalHeight = 1080.0f;
	public Texture btnTexture;

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
	}

	void OnGUI()
	{

		float resX = (float)(Screen.width) / originalWidth;
		float resY = (float)(Screen.height) / originalHeight;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(resX, resY, 1));

		if (showMenu == true)
		{
			GUILayout.BeginArea(new Rect(originalWidth / 2 - 200.0f / 2, originalHeight / 2 - 150.0f / 2, 200, 150));

			if (GUILayout.Button("Save"))
			{
				showMenu = false;
				showLoad = false;
				saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
				showSave = true;

				return;
			}

			if (GUILayout.Button("Load"))
			{
				showSave = false;
				showMenu = false;
				saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
				if (saveGames.Count >= 0)
				{
					showLoad = true;
				}
				else
				{
					showMenu = true;
				}
				return;
			}

			if (GUILayout.Button("Clear"))
			{
				//slu.LoadGame("zzz DONT TOUCH zzz.sav"); //Loads a nonexistent file to clear the level
				//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				slu.ClearScene();
				return;
			}

			if (GUILayout.Button("Close"))
			{
				showSave = false;
				showMenu = false;
				showLoad = false;
				return;
			}

			GUILayout.FlexibleSpace();
			GUILayout.EndArea();
		}
		if (showLoad == true)
		{
			saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
			scrollPosition = GUI.BeginScrollView(new Rect(originalWidth / 2 - 160.0f, originalHeight / 2 - 150.0f / 2, 350, 200), scrollPosition, new Rect(0, 0, 0, 525));
			foreach (SaveGame saveGame in saveGames)
			{
				if (GUILayout.Button(saveGame.savegameName + " (" + saveGame.saveDate + ")"))
				{
					slu.LoadGame(saveGame.savegameName);
					showLoad = false;
					return;
				}
			}
			GUI.EndScrollView();

		}
		if (showSave == true)
		{

			scrollPosition = GUI.BeginScrollView(new Rect(originalWidth / 2 - 160.0f, originalHeight / 2 - 150.0f / 2, 350, 200), scrollPosition, new Rect(0, 0, 300, 560));
			if (GUILayout.Button("Back", GUILayout.MaxWidth(100)))
			{
				if (selectedSaveGameIndex != -99)
				{
					selectedSaveGameIndex = -99;
				}
				else
				{
					showSave = false;
					showMenu = true;
				}
			}
			for (int i = -1; i < saveGames.Count; i++)
			{

				if (i == selectedSaveGameIndex)
				{

					GUILayout.BeginHorizontal(GUILayout.MinWidth(100));

					string str = GUILayout.TextField(saveGameName, 28, GUILayout.MinWidth(200));

					if (regularExpression.IsMatch(str))
					{
						if (str.IndexOfAny(newLine) != -1)
						{
							//New Line detected
							if (i >= 0)
							{
								SaveLoad.DeleteFile(slu.saveGamePath, saveGames[i].savegameName);
							}
							slu.SaveGame(saveGameName);
							selectedSaveGameIndex = -99;
							return;
						}
						else
						{
							saveGameName = str; //All OK, copy
						}
					}
					else
					{
						Debug.Log("Irregular expression detected");
					}

					GUILayout.FlexibleSpace();
					if (GUILayout.Button("Save", GUILayout.MaxWidth(100)))
					{
						if (i >= 0)
						{
							SaveLoad.DeleteFile(slu.saveGamePath, saveGames[i].savegameName);
						}
						slu.SaveGame(saveGameName);
						selectedSaveGameIndex = -99;
						saveGames = SaveLoad.GetSaveGames(slu.saveGamePath, slu.usePersistentDataPath);
						return;
					}

					if (GUILayout.Button("Cancel", GUILayout.MaxWidth(100)))
					{
						selectedSaveGameIndex = -99;
						return;
					}
					GUILayout.EndHorizontal();
				}
				else
				{
					if (i == -1)
					{
						if (GUILayout.Button("(New)"))
						{
							selectedSaveGameIndex = i;
							saveGameName = "";
							return;
						}
					}
					else
					{
						if (GUILayout.Button(saveGames[i].savegameName + " (" + saveGames[i].saveDate + ")"))
						{
							selectedSaveGameIndex = i;
							saveGameName = saveGames[i].savegameName;
							return;
						}
					}
				}
			}
			GUI.EndScrollView();


		}
	}
}
