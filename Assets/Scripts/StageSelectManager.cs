using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour {

	public GameObject[] stageButtons; //ステージ選択ボタン配列

	// Use this for initialization
	void Start () {
		//どのステージまでクリアしているのかをロード(セーブ前なら「0」)
		int clearStageNo = PlayerPrefs.GetInt("CLEAR", 0 );
		//ステージおたんの有効化
		for (int i = 0; i <= stageButtons.GetUpperBound (0); i++) {
			bool b;
			if (clearStageNo < i) {
				b = false; //前ステージをクリアしていなければ無効
			} else {
				b = true; //前ステージをクリアしていれば有効
			}

			//ボタンの有効/無効お設定
			stageButtons [i].GetComponent<Button> ().interactable = b;
		}

		//PlayerPrefsに記録したデータをすべて消して初期状態に戻す
		//PlayerPrefs.DeleteAll ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//スタージ選択ボタンを押した
	public void PushStageSelectButton(int stageNo){
		//ゲームシーンへ
		SceneManager.LoadScene ("PuzzleScene" + stageNo);
	}
}
