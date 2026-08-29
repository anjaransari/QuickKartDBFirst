using System;
using System.Collections.Generic;

namespace DBFirst.DataAccessLayer.Models;

public partial class CardDetail
{
    public decimal CardNumber { get; set; }

    public string NameOnCard { get; set; }

    public string CardType { get; set; }

    public decimal Cvvnumber { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public decimal? Balance { get; set; }
}
