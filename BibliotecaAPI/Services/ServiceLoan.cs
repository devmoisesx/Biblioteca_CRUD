using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaAPI.Storages;
using Serilog;

namespace BibliotecaAPI.Services
{
    public class ServiceLoan
    {
        private readonly StorageLoan _storage;

        public ServiceLoan(StorageLoan storage)
        {
            try
            {
                Log.Information("Creating a Storage instance.");
                _storage = storage;     // Cria uma instancia do Storage
                Log.Information("Storage instance created successfully.");
            }
            catch (Exception e)
            {
                Log.Error("Error creating Storage instance.");
                throw new Exception(e.Message);
            }
        }

        // Creates new loan
        public async Task AddLoanAsync(Loan loan)
        {
            try
            {
                Log.Information("Method AddLoanAsync of ServiceLoan requested.");
                await _storage.AddLoanAsync(loan);
                Log.Information("Method AddLoanAsync of ServiceLoan completed sucessfully.");
            }
            catch (Exception e)
            {
                Log.Error($"Method AddLoanAsync of ServiceLoan request error: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        // Return loan
        public async Task UpdateReturnAsync(string id, Loan loan)
        {
            try
            {
                Log.Information("Method UpdateReturnAsync of ServiceLoan requested.");
                await _storage.UpdateReturnAsync(id, loan);
                Log.Information("Method UpdateReturnAsync of ServiceLoan completed sucessfully.");
            }
            catch (Exception e)
            {
                Log.Error($"Method UpdateReturnAsync of ServiceLoan request error: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        // Reports
        public async Task<List<Loan>> GetLateByBookIdAsync(string id)
        {
            try
            {
                Log.Information("Method GetLateByBookIdAsync of ServiceLoan requested.");
                var bookReport = await _storage.GetLateByBookIdAsync(id);
                Log.Information("Method GetLateByBookIdAsync of ServiceLoan completed sucessfully.");
                return bookReport;
            }
            catch (Exception e)
            {
                Log.Error($"Method GetLateByBookIdAsync of ServiceLoan request error: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Loan>> GetLateByClientId(string id)
        {
            try
            {
                Log.Information("Method GetLateByClientId of ServiceLoan requested.");
                var clientReport = await _storage.GetLateByClientId(id);
                Log.Information("Method GetLateByClientId of ServiceLoan completed sucessfully.");
                return clientReport;
            }
            catch (Exception e)
            {
                Log.Error($"Method GetLateByClientId of ServiceLoan request error: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Loan>> GetsLoansAsync()
        {
            try
            {
                Log.Information("Method GetsLoansAsync of ServiceLoan requested.");
                var loanReport = await _storage.GetsLoansAsync();
                Log.Information("Method GetsLoansAsync of ServiceLoan completed sucessfully.");
                return loanReport;            
            }
            catch (Exception e)
            {
                Log.Error($"Method GetsLoansAsync of ServiceLoan request error: {e.Message}");
                throw new Exception(e.Message);
            }
        }
    }
}