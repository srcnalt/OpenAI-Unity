## OpenAI Unity Package
An unofficial OpenAI Unity Package that aims to help you use OpenAI API directly in Unity Game engine.

## How To Use

### Importing the Package
In Unity 2019+ you can go to `Window > Package Manager > + > Add package from git URL` and paste the repository URL `https://github.com/srcnalt/OpenAI-Unity.git` then click on `Add` button.

### Creating OpenAI Account
To be able to use OpenAI API you will need an OpenAI account. Go to https://openai.com/api and sign up for an account.

Once you created an account you can go to https://beta.openai.com/account/api-keys page and create a new secret key save it.

### Saving Your Credentials
To be able to make requests to OpenAI endpoints you will need to use your API Key and optionally organization name.

To avoid keeping your API Key in your Unity project you can save it in your device's local. 
For this purpose this package uses a json file in User directory of your device.
To create the file to save your credentials follow the steps below.
- Create a folder called `.openai` in your user's home directory `C:User\UserName\` for Windows and `~\` for Linux and Mac.
- Create a file called `auth.json` in `.openai` folder.
- Add `api_key` and optionally `organization` fields, fill their values to your json object and save it.

Finally you should have `C:User\UserName\.openai\auth.json` file and the file content should look as the following.

```json
{
    "api_key": "sk-...W6yi",
    "organization": "org-...L7W"
}
```

Remember that your API key is a secret! Do not share it with others or expose it in any client-side code (browsers, apps). 
Production requests must be routed through your own backend server where your API key can be securely loaded from an environment variable or key management service.
If you are going to use OpenAI for production make sure to run it on the server side.

### Making Requests to OpenAPI
You can create a async method to make a call to the `OpenAI` endpoints. 
All methods are asynchronous and can be directly accessed from the `OpenAIApi` instance.

```csharp
private async void SendRequest()
{
    var openai = new OpenAIApi();
    var request = new CreateCompletionRequest{
        Model="text-davinci-003",
        Prompt="Say this is a test",
    }
    var response = await openai.CreateCompletion(request);
}
```

### Sample Projects
With this package you can find 2 sample scenes that you can import through Package Manager.
- **ChatGPT sample:** A simple ChatGPT like chat example.
- **DallE sample:** A DALL.E text to image generation example.

### Further Reading
For further details on how to use all the request parameters please refer to OpenAI documentation.
https://beta.openai.com/docs/api-reference/introduction