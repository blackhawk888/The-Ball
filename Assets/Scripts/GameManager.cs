using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int StageNo; //ステージナンバー

	public bool isBallMoving; //ボール移動中か否か

	public GameObject ballPrefab; //ボールプレハブ
	public GameObject ball; //ボールオブジェクト

	public GameObject goButton; //ボタン:ゲーム開始
	public GameObject retrybutton; //ボタン:リトライ
	public GameObject clearText; //テキスト:クリア

	public AudioClip clearSE; //効果音:クリア
	private AudioSource audioSource; //オーディオソース 


	// Use this for initialization
	void Start () {
		retrybutton.SetActive (false); //リトライボタンを非表示
		isBallMoving = false; //ボールは移動中ではない

		//オーディオオースを取得
		audioSource = gameObject.GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Goボタンを押した
	public void PushGoButton(){
		//ボールの重力を有効化
		Rigidbody2D rd = ball.GetComponent<Rigidbody2D> ();
		rd.isKinematic = false;

		retrybutton.SetActive (true); //リトライボタンを表示
		goButton.SetActive(false); //Goボタンを非表示
		isBallMoving = true; //ボールは移動中
	}

	//リトライボタンを押した
	public void PushRetryButton(){
		Destroy (ball); //ボールオブジェクトを削除

		//プレハブより新しいボールオブジェクトを作成
		ball = (GameObject)Instantiate(ballPrefab);

		retrybutton.SetActive (false); //リトライボタンを非表示
		goButton.SetActive(true); //Goボタンを幼児
		isBallMoving = false; //ボールは移動中ではない
	}

	//バックボタンを押した
	public void PushBackButton(){
		GobackStageSelect ();
	}

	//ステージクリア処理
	public void StageClear () {
		audioSource.PlayOneShot (clearSE); //クリア音再生
		//セーブデータ表示
		if(PlayerPrefs.GetInt("CLEAR", 0) < StageNo){
			//セーブされているステージNoより今のステージNoが大きければ
			PlayerPrefs.SetInt("CLEAR", StageNo); //ステージナンバーを記録
		}

		clearText.SetActive (true); //クリア表示
		retrybutton.SetActive(false); //リトライボタン非表示

		//3病後に自動的にステージセレクト画面へ
		Invoke("GobackStageSelect", 3.0f);
	}

	//移動処理
	void GobackStageSelect(){
		SceneManager.LoadScene ("StageSelectScene");
	}

}
