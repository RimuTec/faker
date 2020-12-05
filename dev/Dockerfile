FROM mcr.microsoft.com/dotnet/sdk:5.0

RUN apt-get update && \
    apt-get install -y procps

# Create non-root user
RUN groupadd -g 1000 -r faker && \
    useradd -u 1000 -r -g faker -m -s $(which bash) faker
# Option '-m' to create home directory (see https://askubuntu.com/a/393470)
# Option '-s' to set  shell for this user (see comment in https://askubuntu.com/a/393470)
# Option '-r' creates a system user which does not expire (see https://linuxize.com/post/how-to-create-users-in-linux-using-the-useradd-command/)

# Create working directory. Ownership will be changed in entrypoint.sh which
# executes *after* the volume has been mounted.
RUN mkdir /app
RUN mkdir /app/dev

# Copy entrypoint script into container, make it executable, then execute it:
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh
ENTRYPOINT ["/entrypoint.sh"]

# There is an issue with dotnet-test-explorer not finding tests when targeting .NET 5.0
# For more details see https://github.com/formulahendry/vscode-dotnet-test-explorer/issues/288
# COPY test-explorer-patch/testDiscovery.js /home/faker/.vscode-server/extensions/formulahendry.dotnet-test-explorer-0.7.4/out/src/testDiscovery.js
# RUN chown -R faker:faker /home/faker/.vscode-server/extensions/formulahendry.dotnet-test-explorer-0.7.4/out/src/testDiscovery.js

# Install dotnet-outdated (see https://github.com/dotnet-outdated/dotnet-outdated)
RUN runuser -l faker -c "dotnet tool install --global dotnet-outdated-tool"
# runuser installs it as if the non-root user was installing it. This makes it available to that non-root user
ENV PATH "$PATH:/home/faker/.dotnet/tools"
# If the previous doesn't work, consider https://stackoverflow.com/a/59293118/411428 [Manfred, 13nov2020]
