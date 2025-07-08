using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PosteImprim.Models { 
 public class Imprimer { 
  public Guid Id { get; set; } = Guid.NewGuid();
   public DateTime DateImpression { get; set; }
   public List<Position> Positions { get; set; } = new List<Position>();
[Required(ErrorMessage = "Le texte est obligatoire.")]
public List<string> Texts { get; set; } = new List<string>();
    
    } }
