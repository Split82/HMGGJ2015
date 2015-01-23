using UnityEngine;
using System.Collections;

public class GameControlsManager : Singleton<GameControlsManager> {
	
	public bool JumpButtonIsActive {
		get {
			return Input.GetKey(KeyCode.UpArrow);
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
}
