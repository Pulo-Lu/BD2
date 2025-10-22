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
            var cells = GetCellsByTeam(camp);
            if (!IsValidCell(row, col, camp) || !cells[row, col].IsEmpty)
                return false;

            cells[row, col].Occupant = entity;
            entity.Row = row;
            entity.Col = col;
            entity.EntityTeam = camp;
            return true;
        }

        public bool ClearEntity(EntityTeamType camp, int row, int col)
        {
            var cells = GetCellsByTeam(camp);
            if (!IsValidCell(row, col, camp) || !cells[row, col].IsEmpty)
                return false;

            cells[row, col].Occupant = null;

            return true;
        }

        #endregion
        
        #region 移动实体

        /// <summary>
        /// 移动实体到目标格子
        /// </summary>
        public bool MoveEntity(EntityBase entity, int targetRow, int targetCol)
        {
            var cells = GetCellsByTeam(entity.EntityTeam);
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
            var cellsArray = GetCellsByTeam(entity.EntityTeam);

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

        public GridCell GetCell(EntityTeamType team, int row, int col)
        {
            if (!IsValidCell(row, col, team)) return null;
            return GetCellsByTeam(team)[row, col];
        }

        public List<EntityBase> GetCampEntities(EntityTeamType team)
        {
            var list = new List<EntityBase>();
            var cells = GetCellsByTeam(team);
            for (int r = 1; r <= rows; r++)
            {
                for (int c = 1; c <= (team == EntityTeamType.Self ? playerCols : enemyCols); c++)
                {
                    if (!cells[r, c].IsEmpty) list.Add(cells[r, c].Occupant);
                }
            }
            return list;
        }

        #endregion
        
        #region 工具方法

        private GridCell[,] GetCellsByTeam(EntityTeamType team)
        {
            return team == EntityTeamType.Self ? playerCells : enemyCells;
        }

        /// <summary>
        /// 检查一个格子坐标是否在当前阵营的有效范围内
        /// </summary>
        /// <param name="row">行号，从 1 开始</param>
        /// <param name="col">列号，从 1 开始</param>
        /// <param name="team">所属阵营（我方 / 敌方）</param>
        /// <returns>true = 有效格子，false = 越界</returns>
        private bool IsValidCell(int row, int col, EntityTeamType team)
        {
            if (row < 1 || row > rows) return false;
            if (team == EntityTeamType.Self) return col >= 1 && col <= playerCols;
            else return col >= 1 && col <= enemyCols;
        }

        #endregion
        
    }
}