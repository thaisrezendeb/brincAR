using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesBrincAR.services.obj
{
    public class Game
    {

        public Game()
        {
            this.gameName = "";
            this.playerName = "";
            this.points = "";
            this.selectedObject = "";
            this.numberFoundObjects = 0;
            this.raffledLetters = new List<char>();
            this.foundObjects = new List<System.Drawing.Image>();
        }

        private String gameName;

        /// <summary>
        /// Seta ou retorna o nome do jogo(gameName).
        /// </summary>
        public String GameName
        {
            get { return gameName; }
            set { gameName = value; }
        }

        private String playerName;

        /// <summary>
        /// Seta ou retorna o nome do jogador(playerName).
        /// </summary>
        public String PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        private String points;

        /// <summary>
        /// Seta ou retorna o numero de pontos (points).
        /// </summary>
        public String Points
        {
            get { return points; }
            set { points = value; }
        }

        private String selectedObject;
        /// <summary>
        /// Seta ou retorna o objeto selecionado(selectedObject).
        /// </summary>
        public String SelectedObject
        {
            get { return selectedObject; }
            set { selectedObject = value; }
        }

        private List<char> raffledLetters;
        /// <summary>
        /// Seta ou retorna as letras sorteadas(faffleLetters) para o wordsBingo.
        /// </summary>
        public List<char> RaffleLetters
        {
            get { return raffledLetters; }
            set { raffledLetters = value; }
        }

        private int numberFoundObjects;
        /// <summary>
        /// Seta ou retorna o numero de objetos encontrados para o searchingWords.
        /// </summary>
        public int NumberFoundObjects
        {
            get { return numberFoundObjects; }
            set { numberFoundObjects = value; }
        }

        /// <summary>
        /// Lista que contém a imagem dos objetos encontrados no searchingWords.
        /// </summary>
        public List<System.Drawing.Image> foundObjects;
    }
}
