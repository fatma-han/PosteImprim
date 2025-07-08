using System;
using System.Collections.Generic;
using System.Text;

namespace PosteImprim
{
    public class PrintCOM
    {
        private readonly int _paperWidth;
        private readonly int _paperHeight;

        public PrintCOM(int paperWidth = 80, int paperHeight = 297)
        {
            _paperWidth = paperWidth;
            _paperHeight = paperHeight;
        }

        public string GeneratePrintCommand(List<(int X, int Y, string text)> dataList)
        {
            StringBuilder commandBuilder = new StringBuilder();

            // Commandes d'initialisation
            commandBuilder.Append("\x1B@");       // Réinitialisation de l'imprimante
            commandBuilder.Append("\x1BM0");     // Mode standard
            commandBuilder.Append("\x1D!9");     // Format du texte par défaut

            foreach (var (X, Y, text) in dataList)
            {
                // Formatage des coordonnées
                string formattedX = X < 10 ? "0" + X : X.ToString();
                string formattedY = Y < 10 ? "0" + Y : Y.ToString();

                // Génération de la commande d'impression
                commandBuilder.Append($"\x1BL0{formattedX}\x1BJ0{formattedY}{text}");
            }

            // Vérification de la hauteur du papier
            if (dataList[^1].Y > _paperHeight)
            {
                commandBuilder.Append("\x1Bd1");  // Commande de découpe du papier (si nécessaire)
            }

            // Fin de la commande
            commandBuilder.Append("\x1BO");  // Fin d'impression

            return commandBuilder.ToString();
        }
    }
}
