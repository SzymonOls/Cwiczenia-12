using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTest.Models;

public class ClientTrip
{
    [Key]
    public int IdClientTrip { get; set; }

    [ForeignKey("Client")]
    public int IdClient { get; set; }

    public Client Client { get; set; }

    [ForeignKey("Trip")]
    public int IdTrip { get; set; }

    public Trip Trip { get; set; }

    public DateTime RegisteredAt { get; set; }

    public string? PaymentDate { get; set; }
}