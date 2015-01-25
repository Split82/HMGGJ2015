using UnityEngine;
using System.Collections;

public class GameControlsManager : Singleton<GameControlsManager> {
	
	public bool JumpButtonIsActive {
		get {
			return Input.GetKey(KeyCode.UpArrow);
		}
	}

	public bool JumpButtonWasPressed {
		get {
			return Input.GetKeyDown(KeyCode.UpArrow);
		}
	}

	public bool DownButtonIsActive {
		get {
			return Input.GetKey(KeyCode.DownArrow);
		}
	}

	public bool LeftButtonIsActive {
		get {
			return Input.GetKey(KeyCode.LeftArrow);
		}
	}

	public bool RightButtonIsActive {
		get {
			return Input.GetKey(KeyCode.RightArrow);
		}
	}

	public bool FireButonIsActive {
		get {
			return Input.GetKey(KeyCode.X);
		}
	}

	public bool LeftButtonPressed {
		get {
			return Input.GetKeyDown(KeyCode.LeftArrow);
		}
	}
	
	public bool RightButtonPressed {
		get {
			return Input.GetKeyDown(KeyCode.RightArrow);
		}
	}
}
