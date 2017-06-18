using UnityEngine;
using System.Collections;

public class MenuRenderer : MonoBehaviour
{
	public static Vector2 SCALE = new Vector2 (800f / Screen.width, 480f / Screen.height);
	//
	public Game game;
	public GUISkin skin;
	//
	public Font numberFont;
	public Font textFont;
	//
	public Texture miniMap;
	public Texture enemyIndicator;
	public Texture playerIndicator;
	//
	public Texture winImage;
	public Texture loseImage;
	//
	public Texture topMenu;
	public Texture speedMeter;
	public Texture lapImage;
	public Texture checksImage;
	public Texture posImage;
	public Texture nitroBar;
	//
	public Texture nitroUp;
	public Texture nitroDown;
	public Texture brakeUp;
	public Texture brakeDown;
	public Texture pauseUp;
	public Texture pauseDown;
	public Texture respawnUp;
	public Texture respawnDown;
	//
	public Texture settingBackground;
	public Texture knob;
	public Texture activeSlider;
	public Texture onButton;
	public Texture offButton;
	//
	public Texture lowUp;
	public Texture lowDown;
	public Texture mediumUp;
	public Texture mediumDown;
	public Texture highUp;
	public Texture highDown;
	//
	public Texture mainMenuUp;
	public Texture mainMenuDown;
	public Texture continueUp;
	public Texture continueDown;
	public Texture restartUp;
	public Texture restartDown;
	//
	public Texture gameOverBackground;
	public Texture shareUp;
	public Texture shareDown;
	//
	PlayMenu playMenu;
	InfoRenderer infoRenderer;
	PauseMenu pauseMenu;
	GameOverMenu gameOverMenu;
	//		
	int i;
	//
	bool isWrongWay = false;

	public bool IsWrongWay {
		get {
			return isWrongWay;
		}
		set {
			isWrongWay = value;
		}
	}

	int playerOrder;

	public int PlayerOrder {
		get {
			return playerOrder;
		}
		set {
			playerOrder = value;
		}
	}

	void Start ()
	{
		this.useGUILayout = false;	
	}

	void OnGUI ()
	{
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, 
		                            new Vector3 (Screen.width / 800f, Screen.height / 480f, 1f));
	
		this.playMenu.render ();
		this.infoRenderer.render ();

		switch (Game.GameState) {
		case Game.GAME_STATE.WIN:
			this.gameOverMenu.render ();
			break;
			
		case Game.GAME_STATE.LOSE:
			this.gameOverMenu.render ();
			break;

		case Game.GAME_STATE.PAUSE:
			this.pauseMenu.render ();
			break;

		case Game.GAME_STATE.RUNNING:
			if (Input.GetKeyUp (KeyCode.Escape)) {
				this.game.pauseGame();
			}
			break;
			
		default:
			break;
		}
	}

	public void initMiniMap ()
	{
		this.playMenu = new PlayMenu (this);
		this.infoRenderer = new InfoRenderer (this);

		this.pauseMenu = new PauseMenu (this);
		this.gameOverMenu = new GameOverMenu (this);
	}
}	
