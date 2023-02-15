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
    public class TransactionsController : ControllerBase
    {
        private readonly BankContext _context;

        public TransactionsController(BankContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transactions>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transactions/5


        [HttpPost("Deposit")]
        public async Task<ActionResult<TransactionDTO>> DepositTransactions(string accId,TransactionDTO transactionDTO)
        {
            var transactions = await _context.Accounts.FindAsync(accId);
            var currenStatus = transactions.AccountStatusId;

            if (transactions == null || currenStatus == status.accStatus01.ToString() || transactionDTO.Amount <=0)
            {
                return NotFound($"Account not found or your Account Not {status.accStatus01} yet");
                
            }
            Guid id = Guid.NewGuid();
            transactions.Balance += transactionDTO.Amount;

            var tran = new Transactions
            {
                Id = id.ToString(),
                Amount = transactionDTO.Amount,
                Note = transactionDTO.Note,
                Date = DateTime.Now,
                AccountId = accId,
                TransactionTypeId = TranType.tranType02.ToString(),
            };

            _context.Transactions.Add(tran);

            await _context.SaveChangesAsync();
            return Ok("deposit scuessfully");
        }
        [HttpPost("Withdraw")]
        public async Task<ActionResult<TransactionDTO>> WitdrawTransactions(string accId, TransactionDTO transactionDTO)
        {
            var transactions = await _context.Accounts.FindAsync(accId);
            var currenStatus = transactions.AccountStatusId;

            if (transactions == null || currenStatus == status.accStatus01.ToString())
            {
                return NotFound($"Account not found or your Account Not {status.accStatus01} yet");

            }
            Guid id = Guid.NewGuid();
            transactions.Balance -= transactionDTO.Amount;

            var tran = new Transactions
            {
                Id = id.ToString(),
                Amount = transactionDTO.Amount,
                Note = transactionDTO.Note,
                Date = DateTime.Now,
                AccountId = accId,
                TransactionTypeId = TranType.tranType03.ToString(),
            };

            _context.Transactions.Add(tran);

            await _context.SaveChangesAsync();
            return Ok("withdraw scuessfully");
        }



        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactions(string id, Transactions transactions)
        {
            if (id != transactions.Id)
            {
                return BadRequest();
            }

            _context.Entry(transactions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transactions>> PostTransactions(Transactions transactions)
        {
            _context.Transactions.Add(transactions);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TransactionsExists(transactions.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTransactions", new { id = transactions.Id }, transactions);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactions(string id)
        {
            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transactions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionsExists(string id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
//_context.Entry(account).State = EntityState.Modified;
