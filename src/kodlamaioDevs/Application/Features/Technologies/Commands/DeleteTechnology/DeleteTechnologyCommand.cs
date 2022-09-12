using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyCommand: IRequest<DeleteTechnologyDto>
    {
        public int Id { get; set; }
        public class CreateTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeleteTechnologyDto>
        {
            private readonly IMapper _mapper;
            private readonly ITechnologyRepository _technologyRepository;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public CreateTechnologyCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository, TechnologyBusinessRules technologyBusinessRules)
            {
                _mapper = mapper;
                _technologyRepository = technologyRepository;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<DeleteTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology? technology = await  _technologyRepository.GetAsync(p => p.Id == request.Id);
                _technologyBusinessRules.TechnologyShouldExistWhenRequested(technology);

                Technology deletedTechnology = await _technologyRepository.DeleteAsync(technology);
                DeleteTechnologyDto deletedDeleteTechnologyDto = _mapper.Map<DeleteTechnologyDto>(deletedTechnology);

                return deletedDeleteTechnologyDto;
            }
        }
    }
}
