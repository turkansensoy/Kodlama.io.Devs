using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommand : IRequest<CreateTechnologyDto>
    {
        public string Name { get; set; }
        public string ProgrammingLanguageName { get; set; }
        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreateTechnologyDto>
        {
            private readonly IMapper _mapper;
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public CreateTechnologyCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository, IProgrammingLanguageRepository programmingLanguageRepository, TechnologyBusinessRules technologyBusinessRules)
            {
                _mapper = mapper;
                _technologyRepository = technologyRepository;
                _programmingLanguageRepository = programmingLanguageRepository;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<CreateTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechnologyNameCanBeDuplicatedWhenInserted(request.Name);
                ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(p => p.Name == request.ProgrammingLanguageName);
                _technologyBusinessRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);

                Technology createdtechnology = await _technologyRepository.AddAsync(new()
                {
                    ProgrammingLanguageId = programmingLanguage.Id,
                    Name = request.Name
                });
                CreateTechnologyDto technologyListDto = _mapper.Map<CreateTechnologyDto>(createdtechnology);
                return technologyListDto;
            }
        }
    }
}
