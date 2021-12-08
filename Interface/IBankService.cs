using System.Collections.Generic;
using backend.Models;

namespace backend.Interface
{
   public interface IBankService
    {
        IEnumerable<Bank> GetAllBank();

        Bank GetBankByID();
       void AddBank(Bank bank);

       void DelBank(string bankcode);

       void UpdateBank(Bank bank);
     
    }
}