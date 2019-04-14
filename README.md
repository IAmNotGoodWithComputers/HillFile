# HillFile

Because Nextcloud is too slow and it makes me angry

This is very much a work in progress

# Dev environment

- Check out repo
- install .NET Core SDK
- dotnet run

# What is this supposed to be

Someday, this will be a free/libre and open source cloud-storage management system. Much like ownCloud / NextCloud,
but with much less features and a stronger focus on performance. 

A few measures to increase the performance on the long run will be:

- Not targeting outdated browsers at all. Current stable Firefox, Chrome and Safari will be supported.
- Heavy use of websockets and asynchronous workflows
- Not using PHP in the first place lol
- Not implementing WebDAV

# What works?

- File Listing
- File Download

# What doesn't work yet?

- Authentication & Authorization
- File Upload
- Performance is not where I want it to be, yet

# Contributing

I talk about this project on a few Discord servers. If you want to contribute, either grab a 
piece of code marked with `//@todo` or open an issue with your idea. 

The biggest pain point where help is required, is the frontend. I really have no clue of what I'm doing there.