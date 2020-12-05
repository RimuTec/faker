#!/bin/sh

chmod +x /app/dev/setversion.ps1

# Change ownership of all directories and files in the mounted volume, i.e.
# what has been mapped from the host:
chown -R faker:faker /app

#cp /app/dev/test-explorer-patch/testDiscovery.js /home/piranha/.vscode-server/extensions/formulahendry.dotnet-test-explorer-0.7.4/out/src/testDiscovery.js

# su -c "cp /app/dev/test-explorer-patch/testDiscovery.js /home/piranha/.vscode-server/extensions/formulahendry.dotnet-test-explorer-0.7.4/out/src/testDiscovery.js" piranha

# Finally invoke what has been specified as CMD in Dockerfile or command in docker-compose:
"$@"
