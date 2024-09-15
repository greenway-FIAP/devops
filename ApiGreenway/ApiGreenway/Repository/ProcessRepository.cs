using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class ProcessRepository : IProcessRepository
{
    private readonly dbContext _dbContext;

    public ProcessRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<Process>> GetProcesses()
    {
        return await _dbContext.Processes.Where(p => p.dt_finished_at == null).ToListAsync();
    }

    public async Task<Process> GetProcessById(int ProcessId)
    {
        return await _dbContext.Processes.FirstOrDefaultAsync(p => p.id_process == ProcessId && p.dt_finished_at == null);
    }

    public async Task<Process> AddProcess(Process process)
    {
        var processDb = await _dbContext.Processes.AddAsync(process);
        await _dbContext.SaveChangesAsync();
        return processDb.Entity;
    }

    public async Task<Process> UpdateProcess(Process process)
    {
        var processDb = await _dbContext.Processes.FirstOrDefaultAsync(p => p.id_process == process.id_process);
        if (processDb == null)
        {
            return null; // Retorna null se o Process não for encontrado
        }

        processDb.ds_name = process.ds_name;
        processDb.st_process = process.st_process;
        processDb.nr_priority = process.nr_priority;
        processDb.dt_start = process.dt_start;
        processDb.dt_end = process.dt_end;
        processDb.tx_description = process.tx_description;
        processDb.tx_comments = process.tx_comments;
        processDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        processDb.id_company = process.id_company;
        processDb.id_company_representative = process.id_company_representative;

        await _dbContext.SaveChangesAsync();
        return processDb;
    }
    public async void DeleteProcess(int ProcessId)
    {
        var processDb = await _dbContext.Processes.FirstOrDefaultAsync(p => p.id_process == ProcessId);
        if (processDb != null)
        {
            processDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
