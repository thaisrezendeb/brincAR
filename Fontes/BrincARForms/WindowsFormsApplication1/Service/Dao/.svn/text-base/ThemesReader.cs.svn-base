using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ServicesBrincAR.services.obj;

namespace ServicesBrincAR
{
    class ThemesReader
    {
        /* File Format:
         * Theme#Word1#Word2
         * Theme#Word1#Word2
         */
        private StreamReader fileReader;

        public ThemesReader(String filePath) {
            this.fileReader = new StreamReader(filePath);
        }

        /// <summary>
        /// Retorna uma lista de temas geradores
        /// </summary>
        /// <returns></returns>
        public List<GeneratorTheme> readThemes()
        {
            List<GeneratorTheme> readThemes = new List<GeneratorTheme>();
            String line = this.fileReader.ReadLine();
            while(line != null) {
                String[] fields = line.Split('#');
                GeneratorTheme generatorTheme = new GeneratorTheme();
                generatorTheme.ThemeName = fields[0];
                List<String> words = new List<string>();
                for (int i = 1; i < fields.Length; i++ )
                {
                    words.Add(fields[i]);
                }
                generatorTheme.Words  = words;
                readThemes.Add(generatorTheme);
                line = this.fileReader.ReadLine();
            }
            return readThemes;
        }
    }
}
