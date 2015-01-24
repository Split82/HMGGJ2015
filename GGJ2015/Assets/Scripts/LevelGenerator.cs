using UnityEngine;
using System.Collections;

public class Room
{
	public string[] tiles;

	private string[] LoadRes(string type, int max) {
		string index = Random.Range(1, max + 1).ToString("00");
		string name = "room_" + type + "_" + index;
		return (Resources.Load(name) as TextAsset).text.Split('\n');
	}

	public Room(char type) {
		switch (type) {
			case 'S': tiles = LoadRes("start"    , 1); break;
			case 'E': tiles = LoadRes("exit"     , 1); break;
			case '.': tiles = LoadRes("closed"   , 4); break;
			case '-': tiles = LoadRes("straight" , 4); break;
			case 'v': tiles = LoadRes("drop"     , 4); break;
			case '^': tiles = LoadRes("climb"    , 4); break;
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

		// generate rooms
		for (int j = 0; j < roomsy; j++) {
			for (int i = 0; i < roomsx; i++) {
				rooms[i, j] = '-';
			}
		}
		rooms[Random.Range(0, roomsx), 0] = 'S';
		rooms[Random.Range(0, roomsx), roomsy - 1] = 'E';
		for (int j = 0; j < roomsy - 1; j++) {
			for (;;) {
				int i = Random.Range(0, roomsx);
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
			l = Random.Range(0, l);
			r = Random.Range(r + 1, roomsx);
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
