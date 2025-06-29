using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MarkFramework;

public class InGamePanel : BasePanel {

	private Slider player1Slider;
	private Slider player2Slider;
	public PlayerEnergy player1EnergyBar;
	public PlayerEnergy player2EnergyBar;

	protected override void Awake()
	{
		//一定不能少 因为需要执行父类的awake来初始化一些信息 比如找控件 加事件监听
		base.Awake();
		//在下面处理自己的一些初始化逻辑
	}

	// Use this for initialization
	void Start () {
		player1Slider = GetControl<Slider>("Player1HpSlider");
		player2Slider = GetControl<Slider>("Player2HpSlider");
	}

	private void Drag(BaseEventData data)
	{
		//拖拽逻辑
	}

	private void PointerDown(BaseEventData data)
	{
		//PointerDown逻辑
	}

	// Update is called once per frame
	void Update () {
		//TODO:跟新player slider的hp比值；显示结束游戏UI
		player1Slider.value = GameState.Instance.Player1HP/GameState.Instance.GetPlayerMaxHP();
		player2Slider.value = GameState.Instance.Player2HP/GameState.Instance.GetPlayerMaxHP();
		if(GameState.Instance.Player1HP <= 0 || GameState.Instance.Player2HP <= 0)
		{
			Debug.Log("Game End");
		    UIManager.Instance.HidePanel("InGamePanel");
			UIManager.Instance.ShowPanel<ResultPanel>("ResultPanel");
			GameState.Instance.currentGameType = E_GameStateType.E_GameEnd;
		}

		player1EnergyBar.SetPlayerEnergyImg(GameState.Instance.Player1Energy);
		player2EnergyBar.SetPlayerEnergyImg(GameState.Instance.Player2Energy);
	}

	public override void ShowMe()
	{
		base.ShowMe();
		//显示面板时 想要执行的逻辑 这个函数 在UI管理器中 会自动帮我们调用
		//只要重写了它  就会执行里面的逻辑
	}

	protected override void OnClick(string btnName)
	{
		switch(btnName)
		{
			case "btnEnd":
				Debug.Log("btnEnd被点击");
				UIManager.Instance.HidePanel("InGamePanel");
				UIManager.Instance.ShowPanel<ResultPanel>("ResultPanel");
				break;
			case "btnIncreaseEng":
				Debug.Log("btnIncreaseEng被点击");
				if(GameState.Instance.Player1Energy < 5)GameState.Instance.Player1Energy++;
				break;
			case "btnReduceEng":
				Debug.Log("btnReduceEng被点击");
				if(GameState.Instance.Player1Energy > 0) GameState.Instance.Player1Energy--;
				break;
		}
	}

	protected override void OnValueChanged(string toggleName, bool value)
	{
		//在这来根据名字判断 到底是那一个单选框或者多选框状态变化了 当前状态就是传入的value
	}


	public void InitInfo()
	{
		Debug.Log("初始化数据");
	}

	//点击开始按钮的处理(可以放到switch里)
	public void ClickStart()
	{
	}

	//点击开始按钮的处理
	public void ClickQuit()
	{
		Debug.Log("Quit Game");
	}
}
