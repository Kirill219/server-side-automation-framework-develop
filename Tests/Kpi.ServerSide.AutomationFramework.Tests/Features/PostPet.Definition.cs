﻿using System.Threading.Tasks;
using FluentAssertions;
using Kpi.ServerSide.AutomationFramework.Model.Domain.Pet;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.Pet;
using TechTalk.SpecFlow;

namespace Kpi.ServerSide.AutomationFramework.Tests.Features
{
    [Binding, Scope(Feature = "Post Pet")]
    public class PostDefinition
    {
        private readonly IPetContext _petContext;
        private PetResponse _createdPetResponse;
        private PetResponse _petResponse;

        public PostDefinition(
            IPetContext petContext)
        {
            _petContext = petContext;
        }

        [Given(@"I have free API with swagger")]
        public void GivenIHaveFreeApiWithSwagger()
        {
        }

        [When(@"I send pet creation request")]
        public async Task GivenISendPetCreationRequest()
        {
            _createdPetResponse = await _petContext.PostPetAsync(PetRequestStorage.PetRequest["PetRequest"]);
        }

        [Then(@"I send get request")]

        public async Task GivenISendGetRequest()
        {
            _petResponse = await _petContext.GetPetByIdAsync(_createdPetResponse.Id);   
        }

        [Then(@"I see created pet")]

        public void GivenISeeCreatedPet()
        {
            _petResponse.Id.Should().Be(_createdPetResponse.Id);
        }
    }
}
