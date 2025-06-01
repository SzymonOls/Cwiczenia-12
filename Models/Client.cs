using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFTest.Models;

public class Client
{
    [Key]
    public int IdClient { get; set; }

    [Required]
    [MaxLength(120)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(120)]
    public string LastName { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(30)]
    public string Telephone { get; set; }

    [Required]
    [MaxLength(11)]
    public string Pesel { get; set; }

    public ICollection<ClientTrip> ClientTrips { get; set; }
}
