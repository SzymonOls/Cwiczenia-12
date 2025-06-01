using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFTest.Models;

public class Country
{
    [Key]
    public int IdCountry { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<Trip> Trips { get; set; }
}