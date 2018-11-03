using UnityEngine;
using System.Collections;

public class _2_CowboyAnim : MonoBehaviour {

	Animator AnimatorCowboy;
	public bool BoolRun, BoolJump, BoolJumpLeft, BoolJumpRight, BoolOver;
	public float RunAnimSpeed, JumpLeftAnimSpeed, JumpRightAnimSpeed;

	// Use this for initialization
	void Start () {
		AnimatorCowboy = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorCowboy.SetBool ("BoolRun", BoolRun);
		AnimatorCowboy.SetBool ("BoolJump", BoolJump);
		AnimatorCowboy.SetBool ("BoolJumpLeft", BoolJumpLeft);
		AnimatorCowboy.SetBool ("BoolJumpRight", BoolJumpRight);
		AnimatorCowboy.SetBool ("BoolOver", BoolOver);

		AnimatorCowboy.SetFloat ("RunAnimSpeed", RunAnimSpeed);
		AnimatorCowboy.SetFloat ("JumpLeftAnimSpeed", JumpLeftAnimSpeed);
		AnimatorCowboy.SetFloat ("JumpRightAnimSpeed", JumpRightAnimSpeed);
	}

	void JumpLREnd(){
		BoolJumpLeft = false;
		BoolJumpRight = false;
	}
}
