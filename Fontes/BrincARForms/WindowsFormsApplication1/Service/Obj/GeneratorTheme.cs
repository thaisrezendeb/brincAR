using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesBrincAR.services.obj
{
    class GeneratorTheme
    {
        private String themeName;
        /// <summary>
        /// Seta ou retorna o nome do tema(themeName)
        /// </summary>
        public String ThemeName
        {
            get { return themeName; }
            set { themeName = value; }
        }

        private List<String> words;
        /// <summary>
        /// Seta ou retorna a lista de palavras(words)
        /// </summary>
        public List<String> Words
        {
            get { return words; }
            set { words = value; }
        }

        /// <summary>
        /// Tema gerador, possui o nome do tema(themeName) e a lista de palavras(words)
        /// </summary>
        public GeneratorTheme()
        {
            this.themeName = "";
            this.words = new List<string>();
        }
    }
}
