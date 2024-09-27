using CodeNest.DTO.Models;

namespace CodeNest.BLL.Service
{
    public interface IWorkspaceService
    {
        Task<bool> CreateWorkspace(WorkspacesDto workspacesDto);
    }
}
