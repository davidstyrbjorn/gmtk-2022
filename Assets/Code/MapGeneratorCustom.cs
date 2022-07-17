// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;
// using System.Linq;

// public class EditingTools : EditorWindow
// {
//     static int width = 4;
//     static int height = 4;
//     static string level_name = "Cool map name";

//     [MenuItem("GMTK TOOLS/GENERATE MAP")]
//     static void Init()
//     {
//         EditorWindow window = GetWindow(typeof(EditingTools));
//         window.Show();
//     }

//     void OnGUI()
//     {
//         // Some input fields
//         width = EditorGUILayout.IntField("Width", width);
//         height = EditorGUILayout.IntField("Height", height);
//         EditorGUILayout.LabelField("LEVEL NAME");
//         level_name = EditorGUILayout.TextField(level_name);

//         if (GUILayout.Button("Generate map"))
//         {
//             GenerateMap();
//         }
//         if (GUILayout.Button("Set Correct Sorting Order"))
//         {
//             SetCorrectSortingOrder();
//         }
//     }

//     private bool OnEdge(int x, int y, int width, int height)
//     {
//         return (x == 0) || (y == 0) || (x == width - 1) || (y == height - 1);
//     }

//     private void SetCorrectSortingOrder()
//     {
//         var walls_and_tiles = GameObject.Find("walls_and_tiles").GetComponentsInChildren<Transform>();
//         for (int i = 1; i < walls_and_tiles.Length; i++)
//         {
//             if (walls_and_tiles[i].transform.name.Contains("Wall"))
//             {
//                 walls_and_tiles[i].GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-walls_and_tiles[i].transform.position.y);
//             }
//         }
//     }

//     private void GenerateMap()
//     {
//         var parent = new GameObject(level_name).transform; // Holder for the map
//         var container = new GameObject("walls_and_tiles").transform; // Holder for the objects
//         container.SetParent(parent);
//         var spawnPoints = new GameObject("spawn_points").transform;
//         spawnPoints.SetParent(parent);

//         // Grab the prefab object from our Assets
//         var groundPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Ground.prefab", typeof(GameObject));
//         var wallPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Wall.prefab", typeof(GameObject));

//         // Size of ground should be uniform, and wall of height should be 2 times height of ground
//         float groundSize = groundPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
//         float wallHeight = groundSize * 2;
//         float wallOffset = groundSize;

//         // Load all our sprites
//         Sprite bottom = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Graphics/Environment/wall_back.png", typeof(Sprite));
//         Sprite bottomLeft = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Graphics/Environment/wall_corner_down_left.png", typeof(Sprite));
//         Sprite bottomRight = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Graphics/Environment/wall_corner_down_right.png", typeof(Sprite));
//         Sprite topLeft = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Graphics/Environment/wall_corner_up_left.png", typeof(Sprite));
//         Sprite topRight = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Graphics/Environment/wall_corner_up_right.png", typeof(Sprite));
//         Sprite top = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Graphics/Environment/wall_front.png", typeof(Sprite));
//         Sprite left = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Graphics/Environment/wall_side_left.png", typeof(Sprite));
//         Sprite right = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Graphics/Environment/wall_side_right.png", typeof(Sprite));

//         // Generate all the ground and wall tiles
//         foreach (int x in Enumerable.Range(0, width))
//         {
//             foreach (int y in Enumerable.Range(0, height))
//             {
//                 if (OnEdge(x, y, width, height)) // Check if we're on an edge
//                 {
//                     Sprite sprite = top;
//                     if (x == 0)
//                     {
//                         if (y == 0)
//                         {
//                             // Bottom left
//                             sprite = bottomLeft;
//                         }
//                         else if (y == height - 1)
//                         {
//                             // Top left
//                             sprite = topLeft;
//                         }
//                         else
//                         {
//                             // Left side
//                             sprite = left;
//                         }
//                     }
//                     else if (x == width - 1)
//                     {
//                         if (y == 0)
//                         {
//                             // Bottom right
//                             sprite = bottomRight;
//                         }
//                         else if (y == height - 1)
//                         {
//                             // Top right
//                             sprite = topRight;
//                         }
//                         else
//                         {
//                             // Right side
//                             sprite = right;
//                         }

//                     }
//                     else if (y == 0)
//                     {
//                         // Bottom side
//                         sprite = bottom;
//                     }
//                     else if (y == height - 1)
//                     {
//                         // Top side
//                         sprite = top;
//                     }

//                     // Instantiate and place
//                     var wall = Instantiate(wallPrefab).transform;
//                     // Offset uppwards (y) with groundSize
//                     wall.position = (new Vector3(x, y, 0) * groundSize) + (Vector3.up * (groundSize / 2));
//                     wall.transform.SetParent(container);
//                     wall.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-wall.position.y);
//                     wall.GetComponent<SpriteRenderer>().sprite = sprite;
//                     continue;
//                 }

//                 // Instantiate and place
//                 var ground = Instantiate(groundPrefab).transform;
//                 ground.SetParent(container);
//                 ground.position = new Vector3(x, y, 0) * groundSize;
//                 ground.GetComponent<SpriteRenderer>().sortingLayerName = "ground";
//             }
//         }
//     }
// }