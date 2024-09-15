using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IProcessRepository
{
    Task<IEnumerable<Process>> GetProcesses();
    Task<Process> GetProcessById(int ProcessId);
    Task<Process> AddProcess(Process process);
    Task<Process> UpdateProcess(Process process);
    void DeleteProcess(int ProcessId);
}
