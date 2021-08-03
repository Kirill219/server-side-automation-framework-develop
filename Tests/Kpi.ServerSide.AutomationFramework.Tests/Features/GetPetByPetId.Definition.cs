﻿using System.Threading.Tasks;
using FluentAssertions;
using Kpi.ServerSide.AutomationFramework.Model.Domain;
using Kpi.ServerSide.AutomationFramework.Model.Domain.User;
using Kpi.ServerSide.AutomationFramework.TestsData.Storages.User;
using TechTalk.SpecFlow;

namespace Kpi.ServerSide.AutomationFramework.Tests.Features
{
    [Binding, Scope(Feature = "Get Pet by Id")]
    public class GetPetDefinition
    {
        private readonly IUserContext _petContext;
        private UserResponse _petResponse;
        private ResponseMessage _responseMessage;

        public GetPetDefinition(
            IUserContext petContext)
        {
            _petContext = petContext;
        }

        [Given(@"I have free API with swagger")]
        public void GivenIHaveFreeApiWithSwagger()
        {
        }

        [When(@"I receive get pet by id response")]
        public async Task GivenIReceiveGetPetByIdResponse()
        {
            _petResponse = await _petContext.GetPetByIdAsync(1);
        }

        [When(@"I receive get pet by id response with (.*) wrong id")]
        public async Task WhenIReceiveGetPetByIdResponseWithWrongId(string value)
        {
            _responseMessage = await _petContext.GetPetByIdResponseAsync(value);
        }

        [Then(@"I see (.*) response status code")]
        public void ThenISeeResponseStatusCode(string expectedStatusCode)
        {
            _responseMessage.StatusCode.Should().Be(expectedStatusCode);
        }

        [Then(@"I see (.*) response")]
        public void ThenISeeResponse(string expectedErrorResponse)
        {
            _responseMessage.Content.Should().Be(expectedErrorResponse);
        }

        [Then(@"I see returned pet details")]
        public void ThenISeeReturnedPetDetails()
        {
            var expectedResponse = UserResponsesStorage.UserResponses["Default"];
            _petResponse.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
