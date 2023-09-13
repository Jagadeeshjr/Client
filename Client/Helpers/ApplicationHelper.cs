using AutoMapper;
using ClientNamespace.Model;

namespace ClientNamespace.Helpers
{
    public class ApplicationHelper : Profile
    {
        public ApplicationHelper()
        {
            CreateMap<ClientModel, ClientModel>().ReverseMap();
        }
    }
}
