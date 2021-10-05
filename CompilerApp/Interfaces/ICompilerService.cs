using CompilerApp.DTOs;
using System.Threading.Tasks;

namespace CompilerApp.Interfaces
{
    public interface ICompilerService
    {
        Task<CompilerResponseDTO> CompileAsync(CompilerRequestDTO compilerRequestDTO);
    }
}
