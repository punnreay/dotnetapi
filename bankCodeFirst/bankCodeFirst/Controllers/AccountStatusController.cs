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
    public class AccountStatusController : ControllerBase
    {
        private readonly BankContext _context;

        public AccountStatusController(BankContext context)
        {
            _context = context;
        }

        // GET: api/AccountStatus
        [HttpGet]
        public async Task<ActionResult> GetAccountStatuses()
        {

            //return await _context.AccountStatuses.Include(e => e.Accounts).ToListAsync();
            return Ok(await Task.FromResult(
                _context.AccountStatuses.Include(e => e.Accounts)
                .Select(e => new
                {
                    Id = e.Id,
                    code = e.Code,
                    //text = e.Text,
                    Account = e.Accounts.Select(
                        a => new
                        {
                            Number = a.Number,
                            Holder = a.Holder,
                            Balance = a.Balance
                        })
                }).ToList())
                );

                
        }

        //// GET: api/AccountStatus/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<AccountStatus>> GetAccountStatus(string id)
        //{
        //    var accountStatus = await _context.AccountStatuses.FindAsync(id);

        //    if (accountStatus == null)
        //    {
        //        return NotFound();
        //    }


            
              


        //    return accountStatus;
        //}

        //// PUT: api/AccountStatus/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAccountStatus(string id, AccountStatus accountStatus)
        //{
        //    if (id != accountStatus.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(accountStatus).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AccountStatusExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/AccountStatus
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<AccountStatus>> PostAccountStatus(AccountStatus accountStatus)
        //{
        //    _context.AccountStatuses.Add(accountStatus);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (AccountStatusExists(accountStatus.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetAccountStatus", new { id = accountStatus.Id }, accountStatus);
        //}

        //// DELETE: api/AccountStatus/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAccountStatus(string id)
        //{
        //    var accountStatus = await _context.AccountStatuses.FindAsync(id);
        //    if (accountStatus == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.AccountStatuses.Remove(accountStatus);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool AccountStatusExists(string id)
        {
            return _context.AccountStatuses.Any(e => e.Id == id);
        }
    }
}
