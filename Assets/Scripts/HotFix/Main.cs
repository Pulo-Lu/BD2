using HotFix.GamePlay.Grid;
using UnityEngine;

public class Main : MonoBehaviour
{
   private GridManager gridManager;

   private void Awake()
   {
      gridManager = new GridManager(3, 4, 4);
   }
}
