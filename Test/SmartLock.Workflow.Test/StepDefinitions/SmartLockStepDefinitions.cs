using Newtonsoft.Json;
using SmartLock.Workflow.Test.DSL;
using SmartLock.Workflow.Test.Model;
using System.Net;

namespace SmartLock.Workflow.Test.StepDefinitions
{
    [Binding]
    public class SmartLockStepDefinitions
    {
        private readonly SmartLockAdapter _smartLockAdapter;
        private string token;
        private string doorOpenResponse;
        private HttpResponseMessage httpResponseMessage;
        public SmartLockStepDefinitions()
        {
            _smartLockAdapter = new SmartLockAdapter();
            token = String.Empty;
            doorOpenResponse = String.Empty;
            httpResponseMessage = new HttpResponseMessage();
        }

        [Given(@"we have a valid user")]
        public void GivenWeHaveAValidUser()
        {
            //Seeded
        }

        [Given(@"a door created for an office")]
        public void GivenADoorCreatedForAnOffice()
        {
            //Seeded
        }

        [Given(@"the user is authorized to access the door")]
        public async Task GivenTheUserIsAuthorizedToAccessTheDoor()
        {
            httpResponseMessage = await _smartLockAdapter.LoginAsync("sheldon", "sheldon");
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            token = JsonConvert.DeserializeObject<AuthenticateResponse>(await httpResponseMessage.Content.ReadAsStringAsync())?.Token ?? string.Empty;
        }

        [When(@"the user tries to open the door")]
        public async Task WhenTheUserTriesToOpenTheDoor()
        {
            httpResponseMessage = await _smartLockAdapter.OpenDoorAsync("Opening door for ann", token);
            doorOpenResponse = await httpResponseMessage.Content.ReadAsStringAsync();
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int p0)
        {
            Assert.Equal(p0, (int)httpResponseMessage.StatusCode);
        }

        [Then(@"the door should be opened successfully")]
        public void ThenTheDoorShouldBeOpenedSuccessfully()
        {
            Assert.Equal("Door opened successfully", doorOpenResponse);
        }

        [Given(@"the user is not authorized to access the door")]
        public async Task GivenTheUserIsNotAuthorizedToAccessTheDoor()
        {
            httpResponseMessage = await _smartLockAdapter.LoginAsync("allie", "allie");
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            token = JsonConvert.DeserializeObject<AuthenticateResponse>(await httpResponseMessage.Content.ReadAsStringAsync())?.Token ?? string.Empty;
        }
    }
}
