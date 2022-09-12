using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
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

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand : IRequest<CreateTechnologyDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProgrammingLanguageName { get; set; }

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, CreateTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

            public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
                _programmingLanguageRepository = programmingLanguageRepository;
            }


            public async Task<CreateTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology? technology = await _technologyRepository.GetAsync(p => p.Id == request.Id);
                _technologyBusinessRules.TechnologyShouldExistWhenRequested(technology);
                await _technologyBusinessRules.TechnologyNameCanBeDuplicatedWhenInserted(request.Name);

                ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(p => p.Name == request.ProgrammingLanguageName);

                technology.Name = request.Name;
                technology.ProgrammingLanguageId = programmingLanguage.Id;
                Technology updatedTechnology = await _technologyRepository.UpdateAsync(technology);
                CreateTechnologyDto createTechnologyDto = _mapper.Map<CreateTechnologyDto>(updatedTechnology);
                return createTechnologyDto;
            }
        }
    }
}
