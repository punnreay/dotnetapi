using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bankCodeFirst.Data;
using bankCodeFirst.Models;

namespace bankCodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly BankContext _context;

        public AccountsController(BankContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult> GetAccounts()
        {
            return Ok(await Task.FromResult(
                _context.Accounts.Include(e => e.Transactions)
                .Select(e => new
                {
                    Id=e.Id,
                    AccNumber = e.Number,
                    UserName = e.Holder,
                    balance = e.Balance,
                    Status = e.AccountStatus.Code,
                    Transactions = e.Transactions.Select(
                        a => new
                        {
                            Amount = a.Amount,
                            Note = a.Note,
                            Date = a.Date,
                            transactionsType = a.TransactionTypes.Code,

                        })
                }).ToList())
                );
        }

        [HttpGet("accoount")]
        public async Task<ActionResult> Getaccount(string AccountNumber)
        {
            var acc = _context.Accounts.FirstOrDefault(e => e.Number == AccountNumber);




            if (acc == null) return NotFound($"Can Not find the Account {AccountNumber}");
            var accId = acc.Id.ToString();

           var account = await _context.Accounts.Include(
               q=>q.Transactions
               ).Select(e => new
               {
                   Id = e.Id,
                   AccNumber = e.Number,
                   UserName = e.Holder,
                   balance = e.Balance,
                   Status = e.AccountStatus.Code,
                   Transactions = e.Transactions.Select(
                        a => new
                        {
                            Amount = a.Amount,
                            Note = a.Note,
                            Date = a.Date,
                            transactionsType = a.TransactionTypes.Code,

                        })
               }).FirstOrDefaultAsync(e => e.Id == accId);


            return Ok(account);
        }


        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<AccountDTO>> PostAccount(AccountDTO accountDTO)
        {

            Guid id = Guid.NewGuid();

            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000000, 99999999);

            var account = new Account
            {
                Id = id.ToString(),
                Number = myRandomNo.ToString("D9"),
                Holder = accountDTO.Holder,
                Balance = accountDTO.Balance,
                AccountStatusId = status.accStatus01.ToString(),

            };
            var tran = new Transactions
            {
                Id = id.ToString(),
                Amount = accountDTO.Balance,
                Note = "Internal Balance",
                Date = DateTime.Now,
                AccountId = account.Id,
                TransactionTypeId = TranType.tranType01.ToString(),
            };

            _context.Transactions.Add(tran);
            _context.Accounts.Add(account);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountExists(account.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok("ACCOUNT CREATED");
        }

        [HttpPut("Activate")]
        public async Task<ActionResult> ActivateAccount(string accId)
        {
            var account = await _context.Accounts.FindAsync(accId);

            if (account == null) return NotFound();
            account.AccountStatusId = status.accStatus02.ToString();
            _context.SaveChangesAsync();

            return Ok("Account Activated");
        }
        [HttpPut("Inactive")]
        public async Task<ActionResult> InctiveAccount(string accId)
        {
            var account = await _context.Accounts.FindAsync(accId);

            if (account == null) return NotFound();
            account.AccountStatusId = status.accStatus03.ToString();
            _context.SaveChangesAsync();

            return Ok("Account Inactive");
        }

        [HttpPut("Closed")]
        public async Task<ActionResult> ClosedAccount(string accId)
        {
            var account = await _context.Accounts.FindAsync(accId);

            if (account == null) return NotFound();
            account.AccountStatusId = status.accStatus04.ToString();
            _context.SaveChangesAsync();

            return Ok("Account Closed");
        }



        private bool AccountExists(string id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    


    }
}
