using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Etapa4.Data.BankDbModels;

public partial class Client
{
    public int Id { get; set; }

    [MaxLength(200, ErrorMessage = "El nombre debe ser menor de 200 caracteres.")]
    public string Name { get; set; } = null!;

    [MaxLength(40, ErrorMessage = "El número de teléfono debe ser menor de 40 caracteres.")]
    public string PhoneNumber { get; set; } = null!;

    [MaxLength(50, ErrorMessage = "El email debe ser menor de 50 caracteres.")]
    [EmailAddress]
    public string? Email { get; set; }

    public DateTime RegDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
