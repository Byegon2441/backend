using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Interface;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {

        private readonly IBankService _bankService;
        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        public ActionResult GetBank()
        {

            // return await _context.PaymentDetails.ToListAsync();  

            try
            {
                var bank = _bankService.GetAllBank();


                // return Ok(new
                // {
                //     status = true,
                //     msg = "Successfully",
                //     data = bank,
                // });
                return Ok(bank);
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, msg = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult InsertBank(Bank bank)
        {
            try
            {
                _bankService.AddBank(bank);
                // return Ok(new
                // {
                //     status = true,
                //     msg = "Insert Successfully",
                //     data = bank,
                // });
                return Ok(bank);
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, msg = ex.Message });
            }
        }

        [HttpDelete("{bankcode}")]
        public ActionResult DeleteBank(string bankcode)
        {
            try
            {
                _bankService.DelBank(bankcode);
                // return Ok(new
                // {
                //     status = true,
                //     msg = "Delete Successfully",
                //     data = bankcode,
                // });
                return Ok(bankcode);
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, msg = ex.Message });
            }
        }

        [HttpPut]
        public ActionResult EditBank(Bank bank)
        {
            try
            {
                _bankService.UpdateBank(bank);
                // return Ok(new
                // {
                //     status = true,
                //     msg = "Edit Successfully",
                //     data = bank,
                // });
                return Ok(bank);
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, msg = ex.Message });
            }
        }


    }
}