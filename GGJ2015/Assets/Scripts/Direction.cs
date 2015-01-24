using UnityEngine;
using System.Collections;

public class DirectionClass {

	public enum DirectionEnum {
		None,
		Left,
		Right
	}

	public static DirectionEnum Opposite(DirectionEnum direction) {

		if (direction == DirectionEnum.Left) {
			return DirectionEnum.Right;
		}
		else if (direction == DirectionEnum.Right) {
			return DirectionEnum.Left;
		}
		return DirectionEnum.None;
	}
}
