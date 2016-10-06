using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn
{
    /// <summary>
    /// "Моментальный снимок" текущей игры.
    /// Снимки меняются при каждом ходе
    /// </summary>
    internal class GameItem
    {
        internal GameItem(int id, string position, string move)
        {
            this.id = id;
            this.position = position;
            this.move = move;
        }
        /// <summary>
        /// Id снимка
        /// </summary>
        internal int Id { get { return id; } }
        /// <summary>
        /// Позиция перед сделанным ходом
        /// Формат - строка FEN
        /// </summary>
        internal string Position { get { return position; } }
        /// <summary>
        /// Ход, сделанный в текущей позиции. 
        /// Формат - строка вида: ОТКУДА-КУДА:СКОЛЬКО_СБИЛИ:НОМЕР_ПОЛЯ_1;НОМЕР_ПОЛЯ_2;...;НОМЕР_ПОЛЯ_N
        /// для простого хода ОТКУДА-КУДА
        /// </summary>
        internal string Move { get { return move; } }

        private int id;
        private string position;
        private string move;
    }

    public class Game
    {
        public Game()
        {
            items = new List<GameItem>();
        }

        private List<GameItem> items;
    }
}
