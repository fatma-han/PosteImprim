using System;
using System.ComponentModel.DataAnnotations;

namespace PosteImprim.Models
{
    public class Position
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Coordonnée X obligatoire.")]
        public int X { get; set; }

        [Required(ErrorMessage = "Coordonnée Y obligatoire.")]
        public int Y { get; set; }

        [Required(ErrorMessage = "Le texte est obligatoire.")]
        public string Text { get; set; } = string.Empty; // Ensuring non-null

        public Guid ImprimerId { get; set; }
        public Imprimer Imprimer { get; set; } = default!; // Ensuring non-null
    }
}
