# Site News Test

## Return values from the console application

| Code | Description       |
| ---- | ----------------- |
| 0    | Success           |
| 1    | InvalidParameters |
| 2    | InternalError     |

## How to run

### Console application in Windows

Steps to test the application:

1. Open the solution named: HackerNews.sln
2. Build the application in Release mode
3. Open a command prompt: press win+r then type ```cmd```
4. Go to the folder: ```.\bin\Release\netcoreapp3.0\``` from the solution folder.
5. run the command: ```hackernews.exe --posts 20```
6. See the result in the screen

## Libraries Used

### Microsoft.Extensions.Configuration

Used to dynamic build the necessary configurations (URLS and Parallel Level) to run the app. Getting the information from a JSON file or from the Environment. Combined with a few complementary nuggets to extend this functionality.

* Microsoft.Extensions.Configuration.FileExtensions
* Microsoft.Extensions.Configuration.Json
* Microsoft.Extensions.Configuration.EnvironmentVariables
