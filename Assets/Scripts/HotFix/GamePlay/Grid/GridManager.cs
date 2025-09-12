using System.Collections.Generic;
using UnityEngine;

namespace HotFix.GamePlay.Grid
{
    public class GridCell
    {
        public int Row;
        public int Col;
        public EntityBase Occupant; // 当前格子有谁
        
        public bool IsEmpty => Occupant == null;
        
        public GridCell(int row, int col)
        {
            Row = row;
            Col = col;
            Occupant = null;
        }
    }
    
    public enum PushDirection
    {
        Forward,   // 向前
        Backward,  // 向后
        Left,      // 向左
        Right,     // 向右
        ForwardLeft,  // 左前方
        ForwardRight  // 右前方
    }


    public class GridManager
    {
        private int rows;
        private int playerCols;
        private int enemyCols;
        
        private GridCell[,] playerCells; // 我方格子
        private GridCell[,] enemyCells;  // 敌方格子
        
        public GridManager(int rows, int playerCols, int enemyCols)
        {
            this.rows = rows;
            this.playerCols = playerCols;
            this.enemyCols = enemyCols;
            
            InitPlayerCells();
            InitEnemyCells();
        }
        
        private void InitPlayerCells()
        {
            playerCells = new GridCell[rows + 1, playerCols + 1];
            for (int r = 1; r <= rows; r++)
            {
                for (int c = 1; c <= playerCols; c++)
                {
                    playerCells[r, c] = new GridCell(r, c);
                }
            }
        }
        
        private void InitEnemyCells()
        {
            enemyCells = new GridCell[rows + 1, enemyCols + 1];
            for (int r = 1; r <= rows; r++)
            {
                for (int c = 1; c <= enemyCols; c++)
                {
                    enemyCells[r, c] = new GridCell(r, c);
                }
            }
        }
        
        
        #region 放置实体

        public bool PlaceEntity(EntityBase entity, EntityTeamType camp, int row, int col)
        {
            var cells = GetCellsByCamp(camp);
            if (!IsValidCell(row, col, camp) || !cells[row, col].IsEmpty)
                return false;

            cells[row, col].Occupant = entity;
            entity.Row = row;
            entity.Col = col;
            entity.EntityTeam = camp;
            return true;
        }

        #endregion
        
        #region 移动实体

        /// <summary>
        /// 移动实体到目标格子
        /// </summary>
        public bool MoveEntity(EntityBase entity, int targetRow, int targetCol)
        {
            var cells = GetCellsByCamp(entity.EntityTeam);
            if (!IsValidCell(targetRow, targetCol, entity.EntityTeam) || !cells[targetRow, targetCol].IsEmpty)
                return false;

            // 清空旧格子
            cells[entity.Row, entity.Col].Occupant = null;

            // 设置新格子
            cells[targetRow, targetCol].Occupant = entity;
            entity.Row = targetRow;
            entity.Col = targetCol;

            return true;
        }
        
        /// <summary>
        /// 推动单位到目标格子（假设技能/攻击效果）
        /// </summary>
        public bool PushEntity(EntityBase entity, int targetRow, int targetCol)
        {
            var cellsArray = GetCellsByCamp(entity.EntityTeam);

            if (!IsValidCell(targetRow, targetCol, entity.EntityTeam) ||
                !cellsArray[targetRow, targetCol].IsEmpty)
                return false;

            // 清空原位置
            cellsArray[entity.Row, entity.Col].Occupant = null;

            // 移动到目标格子
            cellsArray[targetRow, targetCol].Occupant = entity;
            entity.Row = targetRow;
            entity.Col = targetCol;

            return true;
        }

        #endregion
        
        #region 查询

        public GridCell GetCell(EntityTeamType camp, int row, int col)
        {
            if (!IsValidCell(row, col, camp)) return null;
            return GetCellsByCamp(camp)[row, col];
        }

        public List<EntityBase> GetCampEntities(EntityTeamType camp)
        {
            var list = new List<EntityBase>();
            var cells = GetCellsByCamp(camp);
            for (int r = 1; r <= rows; r++)
            {
                for (int c = 1; c <= (camp == EntityTeamType.Self ? playerCols : enemyCols); c++)
                {
                    if (!cells[r, c].IsEmpty) list.Add(cells[r, c].Occupant);
                }
            }
            return list;
        }

        #endregion
        
        #region 工具方法

        private GridCell[,] GetCellsByCamp(EntityTeamType camp)
        {
            return camp == EntityTeamType.Self ? playerCells : enemyCells;
        }

        private bool IsValidCell(int row, int col, EntityTeamType camp)
        {
            if (row < 1 || row > rows) return false;
            if (camp == EntityTeamType.Self) return col >= 1 && col <= playerCols;
            else return col >= 1 && col <= enemyCols;
        }
        
        private (int row, int col) GetNextCell(int row, int col, EntityTeamType team, PushDirection direction)
        {
            bool isEnemy = team == EntityTeamType.Enemy;

            switch (direction)
            {
                case PushDirection.Forward:
                    col += isEnemy ? -1 : 1;
                    break;
                case PushDirection.Backward:
                    col += isEnemy ? 1 : -1;
                    break;
                case PushDirection.Left:
                    row -= 1;
                    break;
                case PushDirection.Right:
                    row += 1;
                    break;
                case PushDirection.ForwardLeft:
                    col += isEnemy ? -1 : 1;
                    row -= 1;
                    break;
                case PushDirection.ForwardRight:
                    col += isEnemy ? -1 : 1;
                    row += 1;
                    break;
            }

            return (row, col);
        }


        #endregion
        
    }
}