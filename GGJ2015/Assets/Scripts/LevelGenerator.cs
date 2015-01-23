using UnityEngine;
using System;
using System.Collections;

public class Room
{
	public string[] tiles;

	public Room(char type) {
		switch (type) {
			case '.':
				tiles = new string[] { "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX" };
				break;
			case '-':
				tiles = new string[] { "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "..........",
									   "..........",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX" };
				break;
			case 'v':
				tiles = new string[] { "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "..........",
									   "..........",
									   "XXXX..XXXX",
									   "XXXX..XXXX",
									   "XXXX..XXXX",
									   "XXXX..XXXX" };
				break;
			case '^':
				tiles = new string[] { "XXXX..XXXX",
									   "XXXX..XXXX",
									   "XXXX..XXXX",
									   "XXXX..XXXX",
									   "..........",
									   "..........",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX",
									   "XXXXXXXXXX" };
				break;
		}
	}
}

public class LevelGenerator : MonoBehaviour {

	public char[,] GenerateLevel() {

		const int roomsx = 12;
		const int roomsy = 12;

		const int tilesx = 10;
		const int tilesy = 10;

		char[,] rooms;
		char[,] tiles;

		rooms = new char[roomsx, roomsy];
		tiles = new char[roomsx * tilesx, roomsy * tilesy];
		Random rnd = new Random();

		// generate rooms
		for (int j = 0; j < roomsy; j++) {
			for (int i = 0; i < roomsx; i++) {
				rooms[i, j] = '-';
			}
		}
		for (int j = 0; j < roomsy - 1; j++) {
			for (;;) {
				int i = rnd.Next(0, roomsx);
				if (rooms[i, j] == '-') {
					rooms[i, j] = 'v';
					rooms[i, j + 1] = '^';
					break;
				}
			}
		}
		for (int j = 0; j < roomsy; j++) {
			int l = 0;
			while (rooms[l, j] == '-' && l < roomsx) l++;
			int r = roomsx - 1;
			while (rooms[r, j] == '-' && r >= 0) r--;
			l = rnd.Next(0, l);
			r = rnd.Next(r + 1, roomsx);
			for (int i = 0; i < l; i++) rooms[i, j] = '.';
			for (int i = r; i < roomsx; i++) rooms[i, j] = '.';
		}

		// generate tiles from rooms
		for (int j = 0; j < roomsy; j++) {
			for (int i = 0; i < roomsx; i++) {
				Room rm = new Room(rooms[i, j]);
				for (int l = 0; l < tilesy; l++) {
					for (int k = 0; k < tilesx; k++) {
						tiles[i * tilesx + k, j * tilesy + l] = rm.tiles[l][k];
					}
				}
			}
		}

		return tiles;

	}

}
