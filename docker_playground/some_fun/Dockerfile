FROM mono
COPY proj.csproj ./
COPY kod.cs ./
RUN mono --version
RUN msbuild ./ proj.csproj
CMD [ "mono", "kod.cs" ]