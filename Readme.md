# Hacker News API Azure Function

This project contains an Azure Function that retrieves stories from the Hacker News API and provides them as a response. The function is triggered by an HTTP request and returns a list of stories in JSON format.

## Function Overview

The `StoriesFunction` Azure Function fetches stories from the Hacker News API and returns them in response to an HTTP request. The function takes a parameter `totalStories` to specify the number of stories to retrieve.

## Getting Started

### Prerequisites

- Azure Functions development environment set up.
- .NET Core SDK installed.
- HackerNewsAPI.Interfaces and HackerNewsAPI.Models packages installed.

### Installation

1. Clone this repository to your local machine.

2. Open the project in your preferred IDE or code editor.

3. Configure the `IStoriesService` implementation to retrieve stories from the Hacker News API. Implement the `GetStories` method in the `IStoriesService` interface.

4. Update the `StoriesFunction` class constructor to inject the appropriate `IStoriesService` implementation.

### Usage

1. Build and run the Azure Function locally or deploy it to your Azure account.

2. Access the function endpoint using an HTTP GET request in the following format:

```
    https://<your-function-app>.azurewebsites.net/api/stoires/{totalStories}
```

Replace `<your-function-app>` with your actual Azure Function App name and provide the desired value for `totalStories`.

3. The function will retrieve the specified number of stories from the Hacker News API and return them in JSON format.

### Configuration

You can configure the behavior of the function using the `appsettings.json` file or environment variables. Ensure that the necessary configuration settings are provided for the `IStoriesService` implementation to access the Hacker News API.

## Contributing

Contributions to this project are welcome. If you find any issues or improvements, please submit a pull request or create an issue.

## License

This project is licensed under the [MIT License](LICENSE).
