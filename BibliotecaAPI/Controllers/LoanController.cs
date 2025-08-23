using BibliotecaAPI.Interface;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ServiceLoan _serviceLoan;

        public LoanController(ServiceLoan serviceLoan)
        {
            _serviceLoan = serviceLoan;
        }

        [HttpPost] 
        public async Task<ActionResult<Loan>> PostLoanAsync(Loan loan)
        {
            try
            {
                Log.Information("Post Loan request Initialized.");
                await _serviceLoan.AddLoanAsync(loan);
                Log.Information("Post Loan request completed successfully");
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"Post Loan request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Post Loan request completed.");
            }
        }

        [HttpPut("return/{id}")]       // /api/loan/<id>
        public async Task<ActionResult<Loan>> UpdateReturnAsync(string id, Loan loan)
        {
            try
            {
                Log.Information("Update Loan request Initialized.");
                await _serviceLoan.UpdateReturnAsync(id, loan);
                Log.Information("Update Loan request completed successfully.");
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"Update Loan request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Update Loan request completed.");
            }
        }

        [HttpGet("report/book/{id}")]       // /api/loan/<id>
        public async Task<ActionResult<Loan>> GetLateByBookIdAsync(string id)
        {
            try
            {
                Log.Information("Get Loan By Id request Initialized.");
                var book = await _serviceLoan.GetLateByBookIdAsync(id);
                Log.Information("Get Loan By Id request completed successfully");
                return Ok(book);
            }
            catch (Exception e)
            {
                Log.Error($"Get Loan By Id request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Get Loan By Id request completed.");
            }
        }

        [HttpGet("report/client{id}")]       // /api/loan/<id>
        public async Task<ActionResult<Loan>> GetLateByClientIdAsync(string id)
        {
            try
            {
                Log.Information("Get Loan By Id request Initialized.");
                var client = await _serviceLoan.GetLateByClientId(id);
                Log.Information("Get Loan By Id request completed successfully");
                return Ok(client);
            }
            catch (Exception e)
            {
                Log.Error($"Get Loan By Id request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("Get Loan By Id request completed.");
            }
        }

        [HttpGet("report/")]       // /api/loan/
        public async Task<ActionResult<List<Loan>>> GetCatalogsAsync()
        {
            try
            {
                Log.Information("Get Loan request Initialized.");
                List<Loan> loan = await _serviceLoan.GetsLoansAsync();
                Log.Information("Get Loan request completed successfully.");
                return Ok(loan);
            }
            catch (Exception e)
            {
                Log.Error($"Get Loan request error: {e.Message}");
                return BadRequest(e.Message);
            }
            finally
            {
                Log.Information("ReGet Loan requestquest completed.");
            }
        }

        // [HttpDelete("{id}")]        // /api/loan/<id>
        // public async Task<ActionResult<Loan>> DeleteCatalogAsync(string id)
        // {
        //     try
        //     {
        //         Log.Information("Delete Loan request Initialized.");
        //         await _serviceLoan.(id);
        //         Log.Information("Delete Loan request completed successfully.");
        //         return Ok();
        //     }
        //     catch (Exception e)
        //     {
        //         Log.Error($"Delete Loan request error: {e.Message}");
        //         return BadRequest(e.Message);
        //     }
        //     finally
        //     {
        //         Log.Information("Delete Loan request completed.");
        //     }
        // }
    }
}