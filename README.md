## OpenAI Unity Package
An unofficial Unity package that allows you to use the OpenAI API directly in the Unity game engine.

## How To Use

### Importing the Package
To import the package, follow these steps:
- Open Unity 2019 or later
- Go to `Window > Package Manager`
- Click the `+` button and select `Add package from git URL`
- Paste the repository URL https://github.com/srcnalt/OpenAI-Unity.git and click `Add`

### Setting Up Your OpenAI Account
To use the OpenAI API, you need to have an OpenAI account. Follow these steps to create an account and generate an API key:

- Go to https://openai.com/api and sign up for an account
- Once you have created an account, go to https://beta.openai.com/account/api-keys
- Create a new secret key and save it

### Saving Your Credentials
To make requests to the OpenAI API, you need to use your API key and organization name (if applicable). To avoid exposing your API key in your Unity project, you can save it in your device's local storage.

To do this, follow these steps:

- Create a folder called .openai in your home directory (e.g. `C:User\UserName\` for Windows or `~\` for Linux or Mac)
- Create a file called `auth.json` in the `.openai` folder
- Add an api_key field and a organization field (if applicable) to the auth.json file and save it
- Here is an example of what your auth.json file should look like:

```json
{
    "api_key": "sk-...W6yi",
    "organization": "org-...L7W"
}
```
**IMPORTANT:** Your API key is a secret. 
Do not share it with others or expose it in any client-side code (e.g. browsers, apps). 
If you are using OpenAI for production, make sure to run it on the server side, where your API key can be securely loaded from an environment variable or key management service.

### Making Requests to OpenAPI
You can use the `OpenAIApi` class to make async requests to the OpenAI API.

All methods are asynchronous and can be accessed directly from an instance of the `OpenAIApi` class.

Here is an example of how to make a request:

```csharp
private async void SendRequest()
{
    var openai = new OpenAIApi();
    var request = new CreateCompletionRequest{
        Model="text-davinci-003",
        Prompt="Say this is a test",
    };
    var response = await openai.CreateCompletion(request);
}
```

### Sample Projects
This package includes two sample scenes that you can import via the Package Manager:

- **ChatGPT sample:** A simple ChatGPT like chat example.
- **DallE sample:** A DALL.E text to image generation example.

### Further Reading
For more information on how to use the various request parameters, 
please refer to the OpenAI documentation: https://beta.openai.com/docs/api-reference/introduction
