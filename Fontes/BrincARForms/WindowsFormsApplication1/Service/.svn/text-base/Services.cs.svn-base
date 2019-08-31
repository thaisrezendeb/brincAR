using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServicesBrincAR.services.obj;

namespace ServicesBrincAR {
    class Services {

        private Random rand = new Random();
        /// <summary>
        /// Retorna uma lista contendo todos os temas geradores e suas palavras.
        /// </summary>
        /// <returns></returns>
        public static List<GeneratorTheme> readAllThemes()
        {
            ThemesReader themesReader = new ThemesReader(BrincarConsts.getThemesPath());
            List<GeneratorTheme> themes = themesReader.readThemes();
            return themes;
        }

        /// <summary>
        /// Retorna um tema gerador aleatório do arquivo GeneratorTheme.bin.
        /// </summary>
        /// <returns></returns>
        public static GeneratorTheme readTheme()
        {
            ThemesReader themesReader = new ThemesReader(BrincarConsts.getThemesPath());
            List<GeneratorTheme> themes = themesReader.readThemes();
            Random generator = new Random();
            int pos = generator.Next(themes.Count);

            return themes[pos];
        }

        /// <summary>
        /// Inicia o jogo setando o playerName e sorteando uma palavra.
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        public static Game startGame(String playerName, String gameName)
        {
            GeneratorTheme genTheme = Services.readTheme();
            Game game = new Game();
            game.GameName = gameName;
            game.PlayerName = playerName;
            Random rand = new Random();
            int max = genTheme.Words.Count;
            game.SelectedObject = genTheme.Words[rand.Next(0, max)];
            return game;
        }
    }
}
