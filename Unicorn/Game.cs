using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn
{
    internal class GameItem
    {
        internal GameItem(int id, string position, string move)
        {
            this.id = id;
            this.position = position;
            this.move = move;
        }

        internal int Id { get { return id; } }
        internal string Position { get { return position; } }
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
