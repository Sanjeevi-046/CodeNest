using CodeValidator.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValidator.BLL.Service
{
    public interface IWorkspaceService
    {
        Task<bool> CreateWorkspace(WorkspacesDto workspacesDto);
    }
}
