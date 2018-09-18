﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsManager : MonoBehaviour {

	[SerializeField] private int wallsCant;
	[SerializeField] private float wallHeight=0.5f;
	[SerializeField] private GameObject wallPrefab;
	[SerializeField] private int _freeWallCount;
	private GameObject[] walls;

	void Start () {
		if (_freeWallCount > wallsCant) {
			_freeWallCount = wallsCant;
		}
		walls= new GameObject[wallsCant];
		for (int i = 0; i < wallsCant; i++) {
			walls [i] = Instantiate (wallPrefab);
			walls [i].transform.position = new Vector3 (0+ i * 2, wallHeight, 100);
			walls [i].GetComponent<WallClass> ().OriginalPos = walls [i].transform.position;
		}
	}

	void OnEnable()
	{
		GameManager.GetInstance ().AddListener (OnWaveEndEvent, GameManager.GameEvent.WaveEnd);
	}

	void OnDisable()
	{
		if (GameManager.GetInstance ()) {
			GameManager.GetInstance ().RemoveListener (OnWaveEndEvent, GameManager.GameEvent.WaveEnd);
		}
	}


	void OnWaveEndEvent(GameManager.GameEvent evt)
	{
		if (wallsCant > 0 && walls [walls.Length - 1].GetComponent<WallClass> ().Placed == false) {
			freeWallCount++;
		}
	}
	public GameObject FreeCheck(){
		for (int i = 0; i < wallsCant; i++) {
			if (walls [i].GetComponent<WallClass> ().Placed == false) {
				walls [i].GetComponent<WallClass> ().Placed = true;
				_freeWallCount--;
				return walls [i];
			}
		}
		return null;
	}
	public void ReturnWall (GameObject returned){
		for (int i = 0; i < wallsCant; i++) {
			if (walls [i] == returned) {
				walls [i].transform.position = walls [i].GetComponent<WallClass> ().OriginalPos;
				walls [i].GetComponent<WallClass> ().Placed = false;
				_freeWallCount++;
			}
		}
	}
	public int freeWallCount{
		get{ return _freeWallCount; }
		set{_freeWallCount += value;}
	}
}
