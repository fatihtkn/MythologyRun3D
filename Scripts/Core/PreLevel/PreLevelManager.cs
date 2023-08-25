using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLevelManager : MonoSingleton<PreLevelManager>
{
	public GameObject playButton;
	//private int potionClickCount;

	//public int PotionClickCount
	//{
	//	get { return potionClickCount; }
	//	set 
	//	{ 
	//		potionClickCount = value; 
	//		if (potionClickCount == 2)
	//		{
	//			playButton.SetActive(true);
	//		}
	//	}
	//}


	public void LoadScene()
	{
		SceneManager.LoadScene("MainScene");
	}
	


}
