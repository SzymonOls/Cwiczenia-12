using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTest.Models;

public class CountryTrip
{
    [Key]
    public int IdCountryTrip { get; set; }

    [ForeignKey("Country")]
    public int IdCountry { get; set; }

    public Country Country { get; set; }

    [ForeignKey("Trip")]
    public int IdTrip { get; set; }

    public Trip Trip { get; set; }
}