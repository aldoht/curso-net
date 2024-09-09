using System;
using System.Collections.Generic;

namespace Etapa4.Data.BankDbModels;

public partial class TransactionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime RegDate { get; set; }

    public virtual ICollection<BankTransaction> BankTransactions { get; set; } = new List<BankTransaction>();
}
