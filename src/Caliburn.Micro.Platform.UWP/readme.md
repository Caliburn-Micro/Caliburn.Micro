# Why the separate folder?

Currently nuget v3 doesn't support multiple projects in the same folder because they all try to use project.json.
This is much like packages.config but it doesn't yet support the feature to prefix the file with the project name.

Until we'll keep the project in a separate folder.