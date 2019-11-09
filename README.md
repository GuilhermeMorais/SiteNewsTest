# Site News Test

## Return values from the console application

| Code | Description       |
| ---- | ----------------- |
| 0    | Success           |
| 1    | InvalidParameters |
| 2    | InternalError     |

## How to run

### Console application in Windows

It's necessary to have the SDK for [.netCore 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0) installed.

#### Steps to test the application

1. Open the solution named: HackerNews.sln
2. Build the application in Release mode
3. Open a command prompt: press win+r then type ```cmd```
4. Go to the folder: ```.\bin\Release\netcoreapp3.0\``` from the solution folder.
5. run the command: ```hackernews.exe --posts 20```
6. See the result in the screen

### Docker version

It's necessary to have [docker](https://docs.docker.com/install/) installed. I'm using Linux as default image, **not a Windows image**

#### Steps to build and test the application

1. Start docker
2. Open a terminal and go to the directory of the solution, where the file HackerNews.sln is located.
3. run ```docker build -t demo .```, this will create an image with a tag **demo**
4. Now, docker will download the necessary images and will build your container
5. After it finishes, run the command ```docker run demo --posts 10```
6. Check in the console the result from the application

## Configurable Variables

I created 3 variables which can be configured without rebuilding the application. In the appSettings.json file Or using Environment Variables (for docker version).

| Variable              | Description                                         | Default Value                                                       |
| --------------------- | --------------------------------------------------- | ------------------------------------------------------------------- |
| MaxParallelCallsToApi | Max number of calls in parallel to the external API | 10                                                                  |
| BestPostsUrl          | The URL to get the Best Posts                       | https://hacker-news.firebaseio.com/v0/beststories.json?print=pretty |
| StoryDetailUrl*       | The Url to get one Post                             | https://hacker-news.firebaseio.com/v0/item/{0}.json?print=pretty    |

**Attention**:  I do replace the **{0}** with the post Id, don't remove it.

## Libraries Used

### Microsoft.Extensions.Configuration

Used to dynamic build the necessary configurations (URLs and Parallel Level) to run the app. Getting the information from a JSON file or from the Environment. Combined with a few complementary nuggets to extend this functionality.

* Microsoft.Extensions.Configuration.FileExtensions
* Microsoft.Extensions.Configuration.Json
* Microsoft.Extensions.Configuration.EnvironmentVariables

### Microsoft.VisualStudio.Azure.Containers.Tools.Targets

This package is used by Visual Studio to integrate with docker containers
