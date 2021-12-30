#!/bin/sh

chmod +x /app/.devcontainer/setversion.ps1

# Change ownership of all directories and files in the mounted volume, i.e.
# what has been mapped from the host:
chown -R faker:faker /app

# Finally invoke what has been specified as CMD in Dockerfile or command in docker-compose:
"$@"
