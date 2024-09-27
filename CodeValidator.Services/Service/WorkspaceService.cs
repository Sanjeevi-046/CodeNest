using AutoMapper;
using CodeValidator.DAL.Context;
using CodeValidator.DAL.Models;
using CodeValidator.DTO.Models;
using MongoDB.Driver;
namespace CodeValidator.BLL.Service
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly MangoDbService _mongoService;
        private readonly IMapper _mapper;
        public WorkspaceService(MangoDbService mongoService , IMapper mapper) 
        { 
            _mongoService = mongoService;
            _mapper = mapper;
        }

        public async Task<bool> CreateWorkspace(WorkspacesDto workspacesDto) 
        {
            var result = await _mongoService.workSpaces.Find(x=>x.Name == workspacesDto.Name).FirstOrDefaultAsync();

            if (result != null) 
            { 
                return false;
            }
            try
            {
                await _mongoService.workSpaces.InsertOneAsync(_mapper.Map<Workspaces>(workspacesDto));
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
